queensEight.boardTemplate = '''
<div ng-repeat="rowIndex in board.rowIndicies" class="cell-row">
  <div ng-repeat="columnIndex in board.columnIndicies" class="cell" board="board" row-index="rowIndex" column-index="columnIndex">
      <div ng-show="board.hasQueen(rowIndex,columnIndex)" class="queen">
        <img src="/img/crown.png"/>
      </div>
  </div>
</div>
'''

