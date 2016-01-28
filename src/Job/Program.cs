using System.Configuration;
using System.ServiceModel.Security;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.ServiceBus;

namespace QueensEight.Job
{
    class Program
    {

        static void Main(string[] args)
        {
            var serviceBusConfiguration = new ServiceBusConfiguration { ConnectionString = AzureConfiguration.ServiceBusConnectionString };
            var config = new JobHostConfiguration
            {
                DashboardConnectionString = AzureConfiguration.WebJobDashboardConnectionString,
                StorageConnectionString = AzureConfiguration.WebJobStorageConnectionString
            };
            config.UseServiceBus(serviceBusConfiguration);

            var host = new JobHost(config);
            host.RunAndBlock();
        }

    }
}
