using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using QueensEight.Processor;
using QueensEight.Processor.ServiceBus;

namespace QueensEight.Web.Hubs
{
    public class SolutionsHub : Hub
    {
        public static JsonSerializerSettings serializerSettings =
            new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() };

        private static List<Solution> solutions = new List<Solution>();

       static SolutionsHub()
        {
           // AddTestSolutions();
        }

        private static void AddTestSolutions()
        {
            solutions.Add(new Solution
            {
                Hash = "12345",
                Positions = new List<Position>
                {
                    new Position {Row = 2, Column = 2},
                    new Position {Row = 3, Column = 0},
                    new Position {Row = 6, Column = 1},
                    new Position {Row = 7, Column = 3},
                    new Position {Row = 1, Column = 4},
                    new Position {Row = 4, Column = 5},
                    new Position {Row = 0, Column = 6},
                    new Position {Row = 5, Column = 7}
                }
            });
            solutions.Add(new Solution
            {
                Hash = "23456",
                Positions = new List<Position>
                {
                    new Position {Row = 3, Column = 2},
                    new Position {Row = 4, Column = 0},
                    new Position {Row = 7, Column = 1},
                    new Position {Row = 0, Column = 3},
                    new Position {Row = 2, Column = 4},
                    new Position {Row = 5, Column = 5},
                    new Position {Row = 1, Column = 6},
                    new Position {Row = 6, Column = 7}
                }
            });
            solutions.Add(new Solution
            {
                Hash = "34567",
                Positions = new List<Position>
                {
                    new Position {Row = 4, Column = 2},
                    new Position {Row = 5, Column = 0},
                    new Position {Row = 0, Column = 1},
                    new Position {Row = 1, Column = 3},
                    new Position {Row = 3, Column = 4},
                    new Position {Row = 6, Column = 5},
                    new Position {Row = 2, Column = 6},
                    new Position {Row = 7, Column = 7}
                }
            });
            solutions.Add(new Solution
            {
                Hash = "45678",
                Positions = new List<Position>
                {
                    new Position {Row = 5, Column = 2},
                    new Position {Row = 6, Column = 0},
                    new Position {Row = 1, Column = 1},
                    new Position {Row = 2, Column = 3},
                    new Position {Row = 4, Column = 4},
                    new Position {Row = 7, Column = 5},
                    new Position {Row = 3, Column = 6},
                    new Position {Row = 0, Column = 7}
                }
            });
        }

        public string FetchSolutions()
        {
            return JsonConvert.SerializeObject(solutions, serializerSettings);
        }

        public string RequestSolution(Solution partialSolution)
        {
            ServiceBusUtilities.RequestedSolutionsQueue.RequestSolution(partialSolution);

            return "submitted";
        }

        public static void BroadcastSolution(Solution solution)
        {
            var serializedSolution = JsonConvert.SerializeObject(solution, serializerSettings);
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SolutionsHub>();
            context.Clients.All.SolutionAvailable(serializedSolution);

            if (!solution.Positions.Any()) return;
            if (solutions.All(s => s.Hash != solution.Hash))
            {
                solutions.Add(solution);
            }
        }

    }
}