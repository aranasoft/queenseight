using System;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using QueensEight.Job.Configuration;
using QueensEight.Processor;

namespace QueensEight.Job {
    public class JobTasks {
        public static void ProcessRequestedSolutions([ServiceBusTrigger("%requestqueuename%")] Solution theRequestedSolution) {
            var board = new Board();
            board.PlaceQueensAtPositions(theRequestedSolution.Positions);
            var queens = board.Solve();

            var positions = queens.Select(queen => queen.Position).ToList();
            var solution = new Solution {
                Hash = Position.ListHash(positions),
                Positions = positions,
                RequestHash = theRequestedSolution.RequestHash
            };

            PostSolutionNotification(solution);
        }

        private static void PostSolutionNotification(Solution solution) {
            using (var client = new WebClient()) {
                var notificationUrl = WebJobConfiguration.NotificationUrl;
                client.Headers.Add("Content-Type", @"application/json");
                var serializedSolution = JsonConvert.SerializeObject(solution);
                client.UploadString(notificationUrl, serializedSolution);
            }
        }
    }
}