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
      scope.isInteractive = true;
    }
  };
});

queensEight.directive("cell", function () {
  return {
    restrict: "C",
    link: function (scope, element, attrs) {
      var row = scope.$parent.$index;
      var column = scope.$index;

      element.addClass('cell');
      if (queensEight.isDark(row, column)) element.addClass('dark');

      element.bind("click", function () {
        scope.toggleQueen(row, column);
      });
    }
  };
});

queensEight.isDark = function (row, column){
    var rowOffset = row % 2;
    var cellIndex = (row * 8 + column + rowOffset) % 2;
    var isDark = cellIndex == 1;
    return isDark;
};

