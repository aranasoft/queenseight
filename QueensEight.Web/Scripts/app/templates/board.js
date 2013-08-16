(function() {
  queensEight.boardTemplate = '<div ng-repeat="rowIndex in rowIndicies">\n    <div ng-repeat="columnIndex in columnIndicies" class="cell">\n      <img ng-show="hasQueen(rowIndex,columnIndex)" src="/Content/images/crown.png"/>\n    </div>\n</div>';

}).call(this);
