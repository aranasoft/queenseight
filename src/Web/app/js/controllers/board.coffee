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

  return
]
