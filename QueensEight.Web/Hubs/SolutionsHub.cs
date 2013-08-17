using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace QueensEight.Web.Hubs
{
    public class Solution
    {
        public List<Position> Positions { get; set; }
        public string Hash { get; set; }
    }

    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }

    public class SolutionsHub : Hub
    {
        public static JsonSerializerSettings serializerSettings =
            new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() };

        public string FetchSolutions()
        {
            var solutions = new List<Solution>();

            var solution = new Solution {Hash = "12345"};
            var positions = new List<Position>
                {
                    new Position {Row = 2, Column = 2},
                    new Position {Row = 3, Column = 0},
                    new Position {Row = 6, Column = 1},
                    new Position {Row = 7, Column = 3},
                    new Position {Row = 1, Column = 4},
                    new Position {Row = 4, Column = 5},
                    new Position {Row = 0, Column = 6},
                    new Position {Row = 5, Column = 7}
                };
            solution.Positions = positions;
            solutions.Add(solution);

            return JsonConvert.SerializeObject(solutions, serializerSettings);
        }
    }
}