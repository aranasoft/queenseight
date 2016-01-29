using System;
using System.Configuration;
using Microsoft.Azure.WebJobs;

namespace QueensEight.Job.Configuration {
    public class QueueNameResolver : INameResolver {
        public string Resolve(string name) {
            if( name.Equals("requestqueuename",StringComparison.InvariantCultureIgnoreCase))
                return WebJobConfiguration.QueueName;

            return ConfigurationManager.AppSettings[name];
        }
    }
}