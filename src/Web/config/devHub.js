(function ($, window, undefined) {
    var allSolutions = [];
    var newSolution = {"positions":[{"row":0,"column":5,"isValid":true},{"row":5,"column":6,"isValid":true},{"row":1,"column":3,"isValid":true},{"row":2,"column":1,"isValid":true},{"row":3,"column":7,"isValid":true},{"row":4,"column":4,"isValid":true},{"row":6,"column":0,"isValid":true},{"row":7,"column":2,isValid:true}],"$$hashKey":"005",hash:"513213744566072"};
    $.connection.hub.start = function(){
        return {
            done: function(fn){return fn();}
        };
    }
    $.connection.solutionsHub = {};
    $.connection.solutionsHub.client = {};
    $.connection.solutionsHub.server = {};

    // this is depricated in the new version
    // solutions are fetched via api
    $.connection.solutionsHub.server.fetchSolutions = function(){
        return {
            done: function(fn){
                return fn(JSON.stringify(allSolutions)); }
        };
    }
    // this is depricated in the new verstion
    // solutions are requested via api
    $.connection.solutionsHub.server.requestSolution = function(solution){
        $.extend(solution, newSolution);
        $.connection.solutionsHub.client.solutionAvailable(JSON.stringify(solution));
    }

}(window.jQuery, window));
