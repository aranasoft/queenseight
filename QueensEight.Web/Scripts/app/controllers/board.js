queensEight.controller("BoardController", function($scope) {

  $scope.positions = [];
  $scope.rowIndicies = [0, 1, 2, 3, 4, 5, 6, 7];
  $scope.columnIndicies = [0, 1, 2, 3, 4, 5, 6, 7];

  $scope.toggleQueen = function (row, column) {
    var position = { row: row, column: column };
    $scope.positions.push(position);
    console.log('handle click in controller with row: ' + row + ' column: ' +column);
  };

  $scope.hasQueen = function (row, column) {

  };

});


