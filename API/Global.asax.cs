using System.Threading;
using System.Threading.Tasks;
using System.Web.Routing;
using Microsoft.WindowsAzure.ServiceRuntime;
using QueensEight.Processor.ServiceBus;
using QueensEight.Web.App_Start;
using QueensEight.Web.Hubs;

namespace QueensEight.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SetupSubscriptionListener();
        }

        public void SetupSubscriptionListener()
        {
            var instanceId = RoleEnvironment.CurrentRoleInstance.Id;
            var instanceNumber = instanceId.Substring(instanceId.LastIndexOf("_"));
            var environment = RoleEnvironment.IsEmulated  ? "dev" : "prod";

            var subscriptionName = string.Format("solutionavailable_{0}_{1}",environment, instanceNumber);


            ServiceBusConfig.Setup();
            ServiceBusUtilities.SetupSubscription(subscriptionName);

            Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        var solution = ServiceBusUtilities.SolutionAvailableSubscription.Receive();
                        if (solution != null)
                        {
                            SolutionsHub.BroadcastSolution(solution);
                        }
                        Thread.Sleep(100);
                    }
                });
        }
    }
}