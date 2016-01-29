using System;
using System.Linq;
using Microsoft.Azure.WebJobs;
using QueensEight.Processor;

namespace QueensEight.Job {
    public class JobTasks {
        public static void ProcessRequestedSolutions([ServiceBusTrigger("%requestqueuename%")] Solution theRequestedSolution) {
            Console.WriteLine("Solution request for " + theRequestedSolution);
            var board = new Board();
            board.PlaceQueensAtPositions(theRequestedSolution.Positions);
            var queens = board.Solve();

            var positions = queens.Select(queen => queen.Position).ToList();
            var solution = new Solution {
                Hash = Position.ListHash(positions),
                Positions = positions,
                RequestHash = theRequestedSolution.RequestHash
            };

            //TODO: Post solution back to WebAPI
            Console.WriteLine("Solution: " + solution);
        }
    }
}