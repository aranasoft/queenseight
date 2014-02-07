queensEight.boardTemplate = '''
<div ng-repeat="rowIndex in rowIndicies" class="cell-row">
  <div ng-repeat="columnIndex in columnIndicies" class="cell">
      <div ng-show="hasQueen(rowIndex,columnIndex)" class="queen">
        <img src="/img/crown.png"/>
      </div>
  </div>
</div>
'''

