queensEight.boardTemplate = '''
<div ng-repeat="rowIndex in rowIndicies">
    <div ng-repeat="columnIndex in columnIndicies" 
         row="{{rowIndex}}" 
         column="{{columnIndex}}"
         cell ng-class="{dark:isDark({{rowIndex}},{{columnIndex}})}">
    </div>
</div>
'''

