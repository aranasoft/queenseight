queensEight.controller("BoardSelectionController", function($scope) {
  $scope.positions = [];

  $scope.onClick = function (row, column) {
    var position = { row: row, column: column };
    $scope.positions.push(position);
    console.log('handle click in controller with row: ' + row + ' column: ' +column);
  };
  
});
