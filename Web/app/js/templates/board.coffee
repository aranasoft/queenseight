queensEight.boardTemplate = '''
<div ng-repeat="rowIndex in rowIndicies" class="cell-row">
    <div ng-repeat="columnIndex in columnIndicies" class="cell">
      <div ng-show="hasQueen(rowIndex,columnIndex)" class="queen">
        <i class="fa fa-star"></i>
        <img src="/img/crown-alt.png"/>
      </div>
    </div>
</div>
'''

