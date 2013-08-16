(function() {
  queensEight.boardTemplate = '<div ng-repeat="rowIndex in rowIndicies">\n    <div ng-repeat="columnIndex in columnIndicies" \n         row="{{rowIndex}}" \n         column="{{columnIndex}}"\n         cell ng-class="{dark:isDark({{rowIndex}},{{columnIndex}})}">\n    </div>\n</div>';

}).call(this);
