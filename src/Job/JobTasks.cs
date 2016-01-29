using System;
using Microsoft.Azure.WebJobs;
using QueensEight.Processor;

namespace QueensEight.Job {
    public class JobTasks {
        public static void ProcessRequestedSolutions([ServiceBusTrigger("%requestqueuename%")] Solution theRequestedSolution)
        {
            Console.WriteLine(theRequestedSolution);
        }
    }
}