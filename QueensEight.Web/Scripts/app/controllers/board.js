queensEight.controller("BoardSelectionController", function($scope) {

  $scope.positions = [];
  $scope.rowIndicies = [0, 1, 2, 3, 4, 5, 6, 7];
  $scope.columnIndicies = [0, 1, 2, 3, 4, 5, 6, 7];

  $scope.onClick = function (row, column) {
    var position = { row: row, column: column };
    $scope.positions.push(position);
    console.log('handle click in controller with row: ' + row + ' column: ' +column);
  };

  $scope.hasQueen = function (row, column) {

  };

  $scope.isDark = function (row, column) {
    var rowOffset = row % 2;
    var cellIndex = (row * 8 + column + rowOffset) % 2;
    var isDark = cellIndex == 1;
    return isDark;
  };

});
