queensEight.directive("board", function () {
  return {
    restrict: "C",
    template: queensEight.boardTemplate
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

