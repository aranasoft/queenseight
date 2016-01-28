using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus;

namespace QueensEight.Messaging.ServiceBus
{
    public class CustomConfiguration : ConfigurationSection {
        private const string SectionName = "QueensEightServiceBus";
        private static class Attributes
        {
            public const string Namespace = "serviceBusNamespace";
            public const string SasKey = "serviceBusSasKey";
            public const string RequestQueue = "serviceBusQueueName";
        }

        public static string QueueName => CustomConfiguration.Current.ServiceBusQueueName;

        public static string ConnectionString
        {
            get
            {
                const string webJobConfigKey = "AzureWebJobsServiceBus";
                var environmentConnectionString = ConfigurationManager.ConnectionStrings[webJobConfigKey].ConnectionString;
                if (!string.IsNullOrEmpty(environmentConnectionString)) { return environmentConnectionString; }


                var serviceBusNamespace = CustomConfiguration.Current.ServiceBusNamespace;
                var serviceBusSasKey = CustomConfiguration.Current.ServiceBusSasKey;

                var connectionString = $"Endpoint=sb://{serviceBusNamespace}.servicebus.windows.net;" +
                                       "SharedAccessKeyName=RootManageSharedAccessKey;" +
                                       $"SharedAccessKey={serviceBusSasKey}";

                return connectionString;
            }
        }

        [ConfigurationProperty(Attributes.Namespace)]
        public string ServiceBusNamespace
        {
            get { return (string) this[Attributes.Namespace] ?? GetAppSetting(Attributes.Namespace); }
            set { this[Attributes.Namespace] = value; }
        }

        [ConfigurationProperty(Attributes.SasKey)]
        public string ServiceBusSasKey
        {
            get { return (string) this[Attributes.SasKey] ?? GetAppSetting(Attributes.SasKey); }
            set { this[Attributes.SasKey] = value; }
        }

        [ConfigurationProperty(Attributes.RequestQueue)]
        public string ServiceBusQueueName
        {
            get { return (string) this[Attributes.RequestQueue] ?? GetAppSetting(Attributes.RequestQueue); }
            set { this[Attributes.RequestQueue] = value; }
        }

        public static CustomConfiguration Current
        {
            get {
                try {
                    return (CustomConfiguration) ConfigurationManager.GetSection(SectionName);
                }
                catch (ConfigurationErrorsException) {
                    return new CustomConfiguration();
                }
            }
        }
        public string GetAppSetting(string key)
        {
            var setting = $"{SectionName}.{key}";
            return ConfigurationManager.AppSettings[setting];
        }
   }
}