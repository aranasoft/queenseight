using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using QueensEight.Processor;

namespace QueensEight.Api.Hubs {
    public class SolutionsHub : Hub {
        public static JsonSerializerSettings serializerSettings = new JsonSerializerSettings {
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
        };

        public static void BroadcastSolution(Solution solution) {
            var serializedSolution = JsonConvert.SerializeObject(solution, serializerSettings);
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SolutionsHub>();
            context.Clients.All.SolutionAvailable(serializedSolution);

            //TODO: old version stores solution here
            // that seems like mixing responsibilities
            // make sure that 
        }

        public static void BroadcastPendingRequest(Solution solution) {
            var serializedSolution = JsonConvert.SerializeObject(solution, serializerSettings);
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<SolutionsHub>();
            context.Clients.All.PendingRequestMade(serializedSolution);
        }

    }
}