(function() {

  queensEight.boardTemplate = '<div ng-repeat="rowIndex in rowIndicies" class="cell-row">\n    <div ng-repeat="columnIndex in columnIndicies" class="cell">\n      <div ng-show="hasQueen(rowIndex,columnIndex)" class="queen">\n        <i class="icon-star"></i>\n        <img src="/Content/images/crown-alt.png"/>\n      </div>\n    </div>\n</div>';

}).call(this);
