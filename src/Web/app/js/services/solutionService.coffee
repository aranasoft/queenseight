queensEight.factory "q8SolutionService", ['$log', 'q8SolutionData', 'q8ValidSolutionsApi', 'q8PendingSolutionsApi', 'q8SolutionHub', ($log, q8SolutionData, q8ValidSolutionsApi, q8PendingSolutionsApi, q8SolutionHub ) ->
  q8SolutionHub.initialize()

  requestSolution: (solution) ->
    q8SolutionData.myRequestedSolutions.push solution
    q8PendingSolutionsApi.requestSolution(solution)
  requestValidSolutions: ->
    q8ValidSolutionsApi.query().$promise.then (updatedValidSolutions) ->
      angular.copy updatedValidSolutions, q8SolutionData.validSolutions
      return
  requestPendingSolutions: ->
    q8PendingSolutionsApi.query().$promise.then (updatedPendingSolutions) ->
      angular.copy updatedPendingSolutions, q8SolutionData.pendingSolutions
      return
]
