using System.Configuration;
using System.ServiceModel.Security;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.ServiceBus;
using QueensEight.Job.Configuration;

namespace QueensEight.Job
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceBusConfiguration = new ServiceBusConfiguration {
                ConnectionString = WebJobConfiguration.ServiceBusConnectionString
            };
            var config = new JobHostConfiguration
            {
                NameResolver = new QueueNameResolver(),
                DashboardConnectionString = WebJobConfiguration.WebJobDashboardConnectionString,
                StorageConnectionString = WebJobConfiguration.WebJobStorageConnectionString
            };
            config.UseServiceBus(serviceBusConfiguration);

            var host = new JobHost(config);
            host.RunAndBlock();
        }

    }
}
