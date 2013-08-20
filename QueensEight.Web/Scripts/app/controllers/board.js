(function() {
  queensEight.factory("SolutionService", function() {
    return {
      solutions: $.connection.solutionHub.server.fetchSolutions()
    };
  });

  queensEight.controller("GameController", [
    '$scope', function($scope) {
      var solutionsHub;

      solutionsHub = $.connection.solutionsHub;
      solutionsHub.client.solutionAvailable = function(serializedSolution) {
        var solution;

        console.log('serialized solution: ', serializedSolution);
        solution = JSON.parse(serializedSolution);
        $scope.solutions.push(solution);
        if ($scope.activeSolutions[0].requestHash === solution.requestHash) {
          $scope.activeSolutions[0].positions = solution.positions;
          $scope.activeSolutions[0].hash = solution.hash;
        }
        return $scope.$apply();
      };
      return $.connection.hub.start().done(function() {
        return $.connection.solutionsHub.server.fetchSolutions().done(function(solutionsJson) {
          var solution;

          $scope.solutions = JSON.parse(solutionsJson);
          $scope.activeSolutions = [];
          solution = {};
          solution.positions = [];
          $scope.activeSolutions.push(solution);
          return $scope.$apply();
        });
      });
    }
  ]);

  queensEight.controller("SolutionsController", ['$scope', function($scope) {}]);

  queensEight.controller("BoardController", [
    '$scope', function($scope) {
      var _base;

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
      $scope.hasQueen = function(row, column) {
        return _($scope.solution.positions).any(function(position) {
          return position.row === row && position.column === column;
        });
      };
      $scope.requestSolution = function() {
        var hash;

        hash = queensEight.hashFromPositions($scope.solution.positions);
        $scope.solution.requestHash = hash;
        return $.connection.solutionsHub.server.requestSolution($scope.solution);
      };
      return $scope.clearBoard = function() {
        $scope.solution.hash = '';
        return $scope.solution.positions = [];
      };
    }
  ]);

}).call(this);
