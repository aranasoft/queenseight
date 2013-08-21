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