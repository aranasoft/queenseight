queensEight.factory "q8SolutionData", ->
  currentSolution: {positions:[]}
  requestedSolutions: []
  myRequestedSolutions: []
  solutionUnavailable: false
  pendingSolutions: []
  validSolutions: []

queensEight.provider "q8ValidSolutionsApi", ->
  @$get = ($resource) ->
    $resource '/api/v1/solutions/valid', {}, {}
  return

queensEight.provider "q8PendingSolutionsApi", ->
  @$get = ($resource) ->
    $resource '/api/v1/solutions/pending', null,
      requestSolution: {method: 'POST'}
  return

#TODO: Remove [0] references make matcher functions in q8SolutionsData
#TODO: convert solution unavailable to toastr notifictaion
queensEight.factory "q8SolutionHub", ['$rootScope','$log','q8SolutionData',($rootScope, $log, q8SolutionData) ->
  initialize: ->
    $.connection.hub.start().done ->
      $log.log 'signalR connection established'

    solutionsHub = $.connection.solutionsHub

    solutionsHub.client.solutionAvailable = (serializedSolution) ->
      solution = JSON.parse(serializedSolution)
      requestedSolutions = q8SolutionData.pendingSolutions

      myRequest = _(q8SolutionData.myRequestedSolutions).findWhere {requestHash: solution.requestHash}

      if( solution.positions.length == 0 )
        if( requestedSolutions[0].requestHash == solution.requestHash and myRequest? )
          #alert 'solution not available'
          q8SolutionData.solutionUnavailable = true
      else
        q8SolutionData.validSolutions.push solution
        if( requestedSolutions[0].requestHash == solution.requestHash and myRequest? )
          #alert 'found solution'
          q8SolutionData.solutionUnavailable = false
          requestedSolutions[0].positions = solution.positions
          requestedSolutions[0].hash = solution.hash

      if( myRequest? )
        angular.copy _(q8SolutionData.myRequestedSolutions).without( myRequest ), q8SolutionData.myRequestedSolutions


      existingRequest = _(requestedSolutions).findWhere( {requestHash: solution.requestHash} )
      updatedList = _(requestedSolutions).without(existingRequest)
      angular.copy updatedList, requestedSolutions
      $rootScope.$apply()


    solutionsHub.client.pendingRequestMade = (serializedSolution) ->
      solution = JSON.parse(serializedSolution)
      q8SolutionData.pendingSolutions.push solution
      $rootScope.$apply()
      return
]

queensEight.factory "q8SolutionService", ['$log', 'q8SolutionData', 'q8ValidSolutionsApi', 'q8PendingSolutionsApi', 'q8SolutionHub', ($log, q8SolutionData, q8ValidSolutionsApi, q8PendingSolutionsApi, q8SolutionHub ) ->
  q8SolutionHub.initialize()

  requestSolution: (solution) ->
    q8SolutionData.myRequestedSolutions.push solution
    q8PendingSolutionsApi.requestSolution(solution).$promise.then ->
      console.log 'requested solution completed'
    console.log 'myRequests: ', q8SolutionData.myRequestedSolutions
  requestValidSolutions: ->
    q8ValidSolutionsApi.query().$promise.then (updatedValidSolutions) ->
      console.log 'updatedValidSolutions: ', updatedValidSolutions
      angular.copy updatedValidSolutions, q8SolutionData.validSolutions
      return
  requestPendingSolutions: ->
    q8PendingSolutionsApi.query().$promise.then (updatedPendingSolutions) ->
      angular.copy updatedPendingSolutions, q8SolutionData.pendingSolutions
      return
]


queensEight.controller "q8GameController", ['q8SolutionService','q8SolutionData', (q8SolutionService,q8SolutionData) ->
  q8SolutionService.requestValidSolutions()
  q8SolutionService.requestPendingSolutions()

  @currentSolution = q8SolutionData.currentSolution
  @pendingSolutions = q8SolutionData.pendingSolutions
  @validSolutions = q8SolutionData.validSolutions
  @solutionUnavailable = q8SolutionData.solutionUnavailable

  @clearErrors = ->
    console.log 'should be able to remove this when moving to toastr for unavailable solutions'
    q8SolutionData.solutionUnavailable = false

  return
]

queensEight.controller "q8BoardController", ['$scope', 'q8SolutionData','q8SolutionService', ($scope,q8SolutionData,q8SolutionService) ->
  @solution ||= { }
  @solution.positions ||= []
  @rowIndicies = [0..7]
  @columnIndicies = [0..7]
  @isInteractive = false

  @hasQueen = (row, column) =>
    return _(@solution.positions).any (position) ->
      position.row == row and position.column == column

  @toggleQueen = (row, column) =>
    return unless @isInteractive
    position = { row: row, column: column }
    q8SolutionData.solutionUnavailable = false
    if (@hasQueen(row, column))
      $scope.$apply =>
        positions = @solution.positions
        existingPosition = _(positions).find (p)->
          p.row == position.row and p.column == position.column
        positions.splice(positions.indexOf(existingPosition), 1)
    else
      $scope.$apply =>
        @solution.positions.push(position)
    $scope.$apply()
    return

  @requestSolution = =>
    hash = queensEight.hashFromPositions @solution.positions
    @solution.requestHash = hash
    q8SolutionService.requestSolution @solution
    return

  @clearBoard = ->
    @solution.hash = ''
    angular.copy [], @solution.positions
    q8SolutionData.solutionUnavailable = false

  return
]
