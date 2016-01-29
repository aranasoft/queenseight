using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QueensEight.Processor;

namespace QueensEight.Api.Data
{
    public class InMemoryDataStore
    {
        public static List<Solution> Solutions = new List<Solution>();

        static InMemoryDataStore() {
            FakeDataGenerator.AddTestSolutions(Solutions);
        }
    }
}