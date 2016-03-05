queensEight.directive("cell", function () {
  return {
    restrict: "C",
    link: function (scope, element, attrs) {
      var row = scope.$eval(attrs.rowIndex);
      var column = scope.$eval(attrs.columnIndex);
      var board;

      if( attrs.board ){
        board = scope.$eval(attrs.board);
      }

      element.addClass('cell');
      if (queensEight.isDark(row, column)) { element.addClass('cell-dark'); }

      element.bind("click", function () {
        if( board ){
          board.toggleQueen(row, column);
        }
      });
    }
  };
});


