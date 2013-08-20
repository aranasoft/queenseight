using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Newtonsoft.Json;
using QueensEight.Processor.ServiceBus;
using QueensEight.Web.Hubs;

namespace QueensEight.Web
{
    public class WebRole : RoleEntryPoint
    {
        private bool _isStopping = false;
        private bool _hasRunCompleted = false;

        public override bool OnStart()
        {
            return base.OnStart();
        }

        private IHubContext context = null;
        public override void Run()
        {
            while (true)
            {
                if (_isStopping)
                {
                    _hasRunCompleted = true;
                    return;
                }
                Thread.Sleep(10000);
            }
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
