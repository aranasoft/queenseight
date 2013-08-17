(function() {
  queensEight.factory("SolutionService", function() {
    return {
      solutions: $.connection.solutionHub.server.fetchSolutions()
    };
  });

  queensEight.controller("GameController", [
    '$scope', function($scope) {
      return $.connection.hub.start().done(function() {
        return $.connection.solutionsHub.server.fetchSolutions().done(function(solutionsJson) {
          $scope.solutions = JSON.parse(solutionsJson);
          return $scope.$apply();
        });
      });
    }
  ]);

  queensEight.controller("SolutionsController", ['$scope', function($scope) {}]);

  queensEight.controller("BoardController", [
    '$scope', function($scope) {
      var _base;

      window.boardscope = $scope;
      $scope.solution || ($scope.solution = {});
      (_base = $scope.solution).positions || (_base.positions = []);
      $scope.rowIndicies = [0, 1, 2, 3, 4, 5, 6, 7];
      $scope.columnIndicies = [0, 1, 2, 3, 4, 5, 6, 7];
      $scope.toggleQueen = function(row, column) {
        var position;

        if (!$scope.isInteractive) {
          return;
        }
        position = {
          row: row,
          column: column
        };
        if ($scope.hasQueen(row, column)) {
          return $scope.$apply(function() {
            var existingPosition, positions;

            positions = $scope.solution.positions;
            existingPosition = _(positions).find(function(p) {
              return p.row === position.row && p.column === position.column;
            });
            return positions.splice(positions.indexOf(existingPosition), 1);
          });
        } else {
          return $scope.$apply(function() {
            return $scope.solution.positions.push(position);
          });
        }
      };
      return $scope.hasQueen = function(row, column) {
        return _($scope.solution.positions).any(function(position) {
          return position.row === row && position.column === column;
        });
      };
    }
  ]);

  /*
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
  */


}).call(this);
