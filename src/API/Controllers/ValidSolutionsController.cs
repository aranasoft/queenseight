using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QueensEight.Api.Data;
using QueensEight.Api.Hubs;
using QueensEight.Processor;

namespace QueensEight.Api.Controllers
{
    public class ValidSolutionsController : ApiController
    {
        // GET api/v1/solutions/valid
        public IEnumerable<Solution> Get() {
            return InMemoryDataStore.Solutions;
        }

        // POST: api/v1/solutions/valid
        public void Post([FromBody]Solution solution )
        {
            //TODO: check for existing
            InMemoryDataStore.Solutions.Add(solution);

            SolutionsHub.BroadcastSolution(solution);
        }
    }
}