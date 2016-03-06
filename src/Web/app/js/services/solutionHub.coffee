#TODO: Remove [0] references make matcher functions in q8SolutionsData
#TODO: convert solution unavailable to toastr notifictaion
queensEight.factory "q8SolutionHub", ['$rootScope','$log','q8SolutionData','orbMessageService',($rootScope, $log, q8SolutionData, orbMessageService) ->
  initialize: ->
    $.connection.hub.start()
    solutionsHub = $.connection.solutionsHub

    solutionsHub.client.solutionAvailable = (serializedSolution) ->
      solution = JSON.parse(serializedSolution)
      requestedSolutions = q8SolutionData.pendingSolutions

      myRequest = _(q8SolutionData.myRequestedSolutions).findWhere {requestHash: solution.requestHash}

      if( solution.positions.length == 0 )
        if( requestedSolutions[0].requestHash == solution.requestHash and myRequest? )
          orbMessageService.error('No solution available for this pattern.')
      else
        q8SolutionData.validSolutions.push solution
        if( requestedSolutions[0].requestHash == solution.requestHash and myRequest? )
          orbMessageService.success('Found solution for this pattern.')
          requestedSolutions[0].positions = solution.positions
          requestedSolutions[0].hash = solution.hash
          angular.copy solution, q8SolutionData.currentSolution

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
