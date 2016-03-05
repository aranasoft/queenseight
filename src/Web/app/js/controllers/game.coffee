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
