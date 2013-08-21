using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using QueensEight.Processor;
using QueensEight.Processor.ServiceBus;

namespace QueensEight.Worker
{
    public class WorkerRole : RoleEntryPoint
    {
        private bool _isStopping = false;
        private bool _hasRunCompleted = false;
        private List<Solution> _solutions = new List<Solution>();

        public override void Run()
        {
            Trace.TraceInformation("QueensEight.Worker entry point called", "Information");

            while (true)
            {
                if (_isStopping)
                {
                    _hasRunCompleted = true;
                    return;
                }

                var currentSolutionRequest = ServiceBusUtilities.RequestedSolutionsQueue.FetchSolutionRequest(TimeSpan.FromSeconds(30));

                if (currentSolutionRequest != null)
                {
                    Trace.TraceInformation("Processing Solution Request", "Information");
                    
                    var board = new Board();
                    board.PlaceQueensAtPositions(currentSolutionRequest.Positions);
                    var queens = board.Solve();
                    var positions = queens.Select(queen => queen.Position).ToList();

                    var solution = new Solution
                    {
                        Hash = Position.ListHash(positions),
                        Positions = positions
                    };

                    if (positions.Any())
                    {
                        if (_solutions.All(s => s.Hash != solution.Hash))
                        {
                            solution.RequestHash = currentSolutionRequest.RequestHash;
                            ServiceBusUtilities.SolutionAvailableTopic.PublishSolution(solution);
                            _solutions.Add(solution);
                        }
                    }
                }
                else
                {
                    Thread.Sleep(500);
                }
            }
        }

        public override bool OnStart()
        {
            ServicePointManager.DefaultConnectionLimit = 12;

            ServiceBusUtilities.Setup();

            return base.OnStart();
        }

        public override void OnStop()
        {
            _isStopping = true;
            while (!_hasRunCompleted)
            {
                Thread.Sleep(10000);
            }

            base.OnStop();
        }
    }
}
