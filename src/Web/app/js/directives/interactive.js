queensEight.directive("interactive", function() {
  return {
    restrict: "A",
    link: function(scope, element, attrs) {
      var board;
      if( attrs.interactiveBoard ){
        console.log('has a board');
        board = scope.$eval(attrs.interactiveBoard);
        board.isInteractive = true;
      } else {
        console.log('does not have a board');
        console.log('attrs: ', attrs);
      }
    }
  };
});
