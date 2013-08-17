queensEight.factory "SolutionService", ->
  solutions: $.connection.solutionHub.server.fetchSolutions()

queensEight.controller "GameController", ['$scope', ($scope) ->
  $.connection.hub.start().done ->
    $.connection.solutionsHub.server.fetchSolutions().done (solutionsJson) ->
      $scope.solutions = JSON.parse solutionsJson
      $scope.$apply()
]

queensEight.controller "SolutionsController", ['$scope', ($scope) ->
]

queensEight.controller "BoardController", ['$scope', ($scope) ->
  window.boardscope = $scope
  $scope.solution ||= {}
  $scope.solution.positions ||= []

  $scope.rowIndicies = [0..7]
  $scope.columnIndicies = [0..7]
  
  $scope.toggleQueen = (row, column) ->
    return unless $scope.isInteractive
    position = { row: row, column: column }
    if ($scope.hasQueen(row, column)) 
      $scope.$apply ->
        positions = $scope.solution.positions
        existingPosition = _(positions).find (p)->
          p.row == position.row and p.column == position.column;
        positions.splice(positions.indexOf(existingPosition), 1);
    else
      $scope.$apply -> $scope.solution.positions.push(position)

  $scope.hasQueen = (row, column) ->
    return _($scope.solution.positions).any (position) ->
      position.row == row and position.column == column;
]


###
 $.connection.hub.start().done( function() {
    $.connection.questionHub.server.fetchQuestions().done(function(questionsJson){
      var questions = JSON.parse(questionsJson);
      _(questions).each( function(question){
        var questionViewModel = new App.ViewModels.Question(question);
        overflowViewModel.questions.push(questionViewModel);
      });
    });
  });
});


var ngMongo = angular.module("ngMongo", ['ngResource']);

ngMongo.factory("Mongo", function ($resource) {
  return {
    database: $resource('/mongo-api/dbs')
  }
});


ngMongo.controller("ListCtrl", function ($scope, Mongo) {
  $scope.items = Mongo.database.query({}, isArray = true);

  $scope.addDb = function () {
    var dbName = $scope.newDbName;
    if (dbName) {
      var newDb = new Mongo.database({ name: dbName });
      newDb.$save();
      $scope.items.push(newDb);
    };
  }

  $scope.removeDb = function (item) {
    if (confirm("Delete this database? There is no undo!")) {
      item.$delete({ name: item.name })
      $scope.items.splice($scope.items.indexOf(item), 1);
    }
  }

});


###