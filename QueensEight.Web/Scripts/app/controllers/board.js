queensEight.controller("BoardController", ['$scope',function($scope) {

  $scope.positions = [];
  $scope.rowIndicies = [0, 1, 2, 3, 4, 5, 6, 7];
  $scope.columnIndicies = [0, 1, 2, 3, 4, 5, 6, 7];
  
  $scope.toggleQueen = function (row, column) {
    if (!$scope.isInteractive) return;
    var position = { row: row, column: column };
    if ($scope.hasQueen(row, column)) {
      $scope.$apply(function () {
        $scope.positions.splice($scope.positions.indexOf(position), 1);
      });
    } else {
      $scope.$apply(function() { $scope.positions.push(position); });
    }
    console.log('handle click in controller with row: ' + row + ' column: ' + column);
    console.log('hasQueen now: ' + $scope.hasQueen(row, column));
  };

  $scope.hasQueen = function (row, column) {
    return _($scope.positions).any(function (position) {
      return position.row === row && position.column === column;
    });
  };
  
}]);


