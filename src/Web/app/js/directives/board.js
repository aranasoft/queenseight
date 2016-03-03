queensEight.directive("board", function () {
  return {
    restrict: "C",
    template: queensEight.boardTemplate
  };
});

queensEight.directive("interactive", function() {
  return {
    restrict: "A",
    link: function(scope, element, attrs) {
      var board;
      if( attrs.thing ){
        console.log('has a board');
        board = scope.$eval(attrs.thing);
        board.isInteractive = true;
      } else {
        console.log('does not have a board');
        console.log('attrs: ', attrs);
      }
    }
  };
});

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
      //console.log('attrs: ', attrs)
      //var row = scope.$parent.$index;
      //var column = scope.$index;

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

queensEight.isDark = function (row, column) {
    var rowOffset = row % 2;
    var cellIndex = (row * 8 + column + rowOffset) % 2;
    var isDark = cellIndex === 1;
    return isDark;
};

queensEight.hashFromPositions = function(positions) {
  var hash = '';
  var cellHashes = _(positions).sortBy(function(position) {
    return position.row;
  }).map(function(position) {
    return "" + position.row + "" + position.column;
  });

  return cellHashes.join('');
};

