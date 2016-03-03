queensEight.factory "q8SolutionData", ->
  currentSolution: {positions:[]}
  requestedSolutions: []
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
queensEight.factory "q8SolutionHub", (q8SolutionData) ->
  initialize: ->
    $.connection.hub.start().done ->
      console.log 'done from hub factory'

    solutionsHub = $.connection.solutionsHub

    solutionsHub.client.solutionAvailable = (serializedSolution) ->
      console.log 'solution available client'
      solution = JSON.parse(serializedSolution)
      requestedSolutons = q8SolutionData.requestedSolutions

      if( solution.positions.length == 0 )
        if( requestedSolutions[0].requestHash == solution.requestHash )
          q8SolutionData.solutionUnavailable = true
          $scope.$apply()
      else
        q8SolutionData.validSolutions.push solution
        if( requestedSolutions[0].requestHash == solution.requestHash )
          q8SolutionData.solutionUnavailable = false
          requestedSolutions[0].positions = solution.positions
          requestedSolutions[0].hash = solution.hash
        $scope.$apply()

    solutionsHub.client.pendingRequestMade = (serializedSolution) ->
      console.log 'pending solution request client'
      solution = JSON.parse(serializedSolution)
      q8SolutionData.pendingSolutions.push solution

  requestSolution: (partialSolution) ->
    $.connection.solutionsHub.server.requestSolution partialSolution


queensEight.factory "q8SolutionService", (q8SolutionData, q8ValidSolutionsApi, q8PendingSolutionsApi, q8SolutionHub ) ->
  q8SolutionHub.initialize()

  requestSolution: (solution) ->
    q8PendingSolutionsApi.requestSolution solution
  requestValidSolutions: ->
    q8ValidSolutionsApi.query().$promise.then (updatedValidSolutions) ->
      console.log 'updatedValidSolutions: ', updatedValidSolutions
      angular.copy updatedValidSolutions, q8SolutionData.validSolutions
      return
  requestPendingSolutions: ->
    q8PendingSolutionsApi.query().$promise.then (updatedPendingSolutions) ->
      angular.copy updatedPendingSolutions, q8SolutionData.pendingSolutions
      return


queensEight.controller "q8GameController", ['q8SolutionService','q8SolutionData', (q8SolutionService,q8SolutionData) ->
  q8SolutionService.requestValidSolutions()
  q8SolutionService.requestPendingSolutions()

  @currentSolution = q8SolutionData.currentSolution
  @pendingSolutions = q8SolutionData.pendingSolutions
  @validSolutions = q8SolutionData.validSolutions
  @solutionUnavailable = q8SolutionData.solutionUnavailable
  @anotherDataItem = {test: "this thing"}

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
    $scope.$apply
    return

  @requestSolution = =>
    hash = queensEight.hashFromPositions @solution.positions
    @solution.requestHash = hash
    q8SolutionService.requestSolution @solution
    return

  @clearBoard = ->
    q8SolutionData.currentSolution.hash = ''
    q8SolutionData.currentSolution.positions = []
    q8SolutionData.solutionUnavailable = false

  return
]


queensEight.factory "SolutionService", ->
  solutions: $.connection.solutionHub.server.fetchSolutions()

queensEight.controller "GameController", ['$scope','q8SolutionService', ($scope,q8SolutionService) ->
  q8SolutionService.requestValidSolutions()
  q8SolutionService.requestPendingSolutions()



  $scope.solutionUnavailable = false
  solutionsHub = $.connection.solutionsHub
  solutionsHub.client.solutionAvailable = (serializedSolution) ->
    solution = JSON.parse(serializedSolution)
    if( solution.positions.length == 0 )
      if( $scope.activeSolutions[0].requestHash == solution.requestHash )
        $scope.solutionUnavailable = true
        $scope.$apply()
    else
      $scope.solutions.push solution
      if( $scope.activeSolutions[0].requestHash == solution.requestHash )
        $scope.solutionUnavailable = false
        $scope.activeSolutions[0].positions = solution.positions
        $scope.activeSolutions[0].hash = solution.hash
      $scope.$apply()

  $scope.clearErrors = ->
    $scope.solutionUnavailable = false

  $.connection.hub.start().done ->
    console.log 'done from game controller'
    $.connection.solutionsHub.server.fetchSolutions().done (solutionsJson) ->
      $scope.solutions = JSON.parse solutionsJson
      $scope.activeSolutions = []
      solution = {}
      solution.positions = []
      $scope.activeSolutions.push solution

      #$scope.$apply()
      return
  ###
  #TODO: convert this to controllerAs syntax
  solutionUnavailable
  activeSolutions
  ###
]

queensEight.controller "SolutionsController", ['$scope', ($scope) ->
]

queensEight.controller "BoardController", ['$scope', ($scope) ->
  #TODO migrate $scope.solution to q8Data.displayedSolution
  #TODO convert to controllerAs syntax
  $scope.solution ||= {}
  $scope.solution.positions ||= []

  $scope.rowIndicies = [0..7]
  $scope.columnIndicies = [0..7]

  $scope.toggleQueen = (row, column) ->
    return unless $scope.isInteractive
    position = { row: row, column: column }
    $scope.$parent.$parent.clearErrors()
    if ($scope.hasQueen(row, column))
      $scope.$apply ->
        positions = $scope.solution.positions
        existingPosition = _(positions).find (p)->
          p.row == position.row and p.column == position.column
        positions.splice(positions.indexOf(existingPosition), 1)
    else
      $scope.$apply -> $scope.solution.positions.push(position)

  $scope.hasQueen = (row, column) ->
    return _($scope.solution.positions).any (position) ->
      position.row == row and position.column == column

  $scope.requestSolution = ->
    hash = queensEight.hashFromPositions $scope.solution.positions
    $scope.solution.requestHash = hash
    $.connection.solutionsHub.server.requestSolution($scope.solution)

  $scope.clearBoard = ->
    $scope.solution.hash = ''
    $scope.solution.positions = []
    $scope.$parent.$parent.clearErrors()
]

