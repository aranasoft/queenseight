queensEight.boardTemplate = '''
<div ng-repeat="rowIndex in rowIndicies">
    <div ng-repeat="columnIndex in columnIndicies" class="cell">
      <img ng-show="hasQueen(rowIndex,columnIndex)" src="/Content/images/crown.png"/>
    </div>
</div>
'''

