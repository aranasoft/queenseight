using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QueensEight.Api.Hubs;
using QueensEight.Messaging.ServiceBus;
using QueensEight.Processor;

namespace QueensEight.Api.Controllers
{
    public class PendingSolutionsController : ApiController {
        private RequestedSolutions _requestedSolutions;
        private RequestedSolutions RequestedSolutionsQueue {
            get {
                _requestedSolutions = _requestedSolutions ?? (_requestedSolutions = new RequestedSolutions());
                return _requestedSolutions;
            }
        }

        // GET: api/v1/solutions/pending
        public IEnumerable<Solution> Get() {
            var pendingSolutions = RequestedSolutionsQueue.All();

            return pendingSolutions;
        }

        // POST: api/v1/solutions/pending
        public void Post([FromBody]Solution partialSolution )
        {
            RequestedSolutionsQueue.Add(partialSolution);

            SolutionsHub.BroadcastPendingRequest(partialSolution);
        }
    }
}
