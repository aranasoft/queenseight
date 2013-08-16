queensEight.directive("board", function () {
  return {
    restrict: "C",
    template: queensEight.boardTemplate,
    link: function (scope, element) {
      //console.log('scope: ' + scope);
      //console.log('element: ' + element);
    }
  };
});

queensEight.directive("cell", function() {
  return {
    restrict: "A",
    link: function (scope, element, attrs) {
      element.addClass('cell');

      element.bind("click", function (e) {
        scope.onClick(attrs.row, attrs.column);
      });
    }
  };
});

