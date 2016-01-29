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
            HubContext.Clients.All.SolutionAvailable(serializedSolution);
        }

        public static void BroadcastPendingRequest(Solution solution) {
            var serializedSolution = JsonConvert.SerializeObject(solution, serializerSettings);
            HubContext.Clients.All.PendingRequestMade(serializedSolution);
        }

        public static IHubContext HubContext => GlobalHost.ConnectionManager.GetHubContext<SolutionsHub>();
    }
}