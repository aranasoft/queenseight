queensEight.directive("interactive", function() {
  return {
    restrict: "A",
    link: function(scope, element, attrs) {
      var board;
      if( attrs.interactiveBoard ){
        board = scope.$eval(attrs.interactiveBoard);
        board.isInteractive = true;
      }
    }
  };
});
