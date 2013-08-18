queensEight.factory "SolutionService", ->
  solutions: $.connection.solutionHub.server.fetchSolutions()

queensEight.controller "GameController", ['$scope', ($scope) ->
  solutionsHub = $.connection.solutionsHub
  solutionsHub.client.solutionAvailable = (serializedSolution) ->
    console.log 'serialized solution: ',serializedSolution
    solution = JSON.parse(serializedSolution)
    $scope.solutions.push solution
    $scope.$apply()

  $.connection.hub.start().done ->
    $.connection.solutionsHub.server.fetchSolutions().done (solutionsJson) ->
      $scope.solutions = JSON.parse solutionsJson
      $scope.$apply()
]

queensEight.controller "SolutionsController", ['$scope', ($scope) ->
]

queensEight.controller "BoardController", ['$scope', ($scope) ->
  window.boardscope = $scope
  $scope.solution ||= {}
  $scope.solution.positions ||= []

  $scope.rowIndicies = [0..7]
  $scope.columnIndicies = [0..7]
  
  $scope.toggleQueen = (row, column) ->
    return unless $scope.isInteractive
    position = { row: row, column: column }
    if ($scope.hasQueen(row, column)) 
      $scope.$apply ->
        positions = $scope.solution.positions
        existingPosition = _(positions).find (p)->
          p.row == position.row and p.column == position.column;
        positions.splice(positions.indexOf(existingPosition), 1);
    else
      $scope.$apply -> $scope.solution.positions.push(position)

  $scope.hasQueen = (row, column) ->
    return _($scope.solution.positions).any (position) ->
      position.row == row and position.column == column;

  $scope.requestSolution = ->
    console.log 'request solution'
    $.connection.solutionsHub.server.requestSolution($scope.solution).done (solutionJson) ->
      console.log 'solution response: ', solutionJson
      solution = JSON.parse solutionJson
      $scope.solution = solution
      $scope.$apply()

  $scope.clearBoard = ->
    $scope.solution.hash = ''
    $scope.solution.positions = []
]

