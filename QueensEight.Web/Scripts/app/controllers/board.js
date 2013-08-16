queensEight.controller("BoardController", ['$scope',function($scope) {

  $scope.positions = [];
  $scope.rowIndicies = [0, 1, 2, 3, 4, 5, 6, 7];
  $scope.columnIndicies = [0, 1, 2, 3, 4, 5, 6, 7];
  
  $scope.toggleQueen = function (row, column) {
    if (!$scope.isInteractive) return;
    var position = { row: row, column: column };
    if ($scope.hasQueen(row, column)) {
      $scope.$apply(function () {
        var existingPosition = _($scope.positions).find(function(p) {
          return p.row === position.row && p.column === position.column;
        });
        $scope.positions.splice($scope.positions.indexOf(existingPosition), 1);
      });
    } else {
      $scope.$apply(function () { $scope.positions.push(position); });
    }
  };

  $scope.hasQueen = function (row, column) {
    return _($scope.positions).any(function (position) {
      return position.row === row && position.column === column;
    });
  };
  
}]);


