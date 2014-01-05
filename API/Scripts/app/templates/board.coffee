queensEight.boardTemplate = '''
<div ng-repeat="rowIndex in rowIndicies" class="cell-row">
    <div ng-repeat="columnIndex in columnIndicies" class="cell">
      <div ng-show="hasQueen(rowIndex,columnIndex)" class="queen">
        <i class="icon-star"></i>
        <img src="/Content/images/crown-alt.png"/>
      </div>
    </div>
</div>
'''

