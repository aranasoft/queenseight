using System;
using Microsoft.Azure.WebJobs;

namespace QueensEight.Job {
    public class JobTasks {
        public static void ProcessRequestedSolutions([ServiceBusTrigger("requestedsolutions")] string theRequestedSolution)
        {
            Console.WriteLine(theRequestedSolution);
        }
    }
}