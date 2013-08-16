queensEight.directive("board", function () {
  return {
    restrict: "C",
    template: queensEight.boardTemplate,
    link: function(scope, element) {
      //element.addClass('board-large');
      console.log('scope: ' + scope);
      console.log('element: ' + element);
    }
  };
});

