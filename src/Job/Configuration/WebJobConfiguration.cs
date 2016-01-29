using System.Configuration;

namespace QueensEight.Job.Configuration
{
    public class WebJobConfiguration : ConfigurationSection {
        private const string SectionName = "QueensEightWebJob";
        private static class Attributes
        {
            public const string Namespace = "serviceBusNamespace";
            public const string SasKey = "serviceBusSasKey";
            public const string QueueName = "serviceBusQueueName";
            public const string StorageAccountName = "storageAccountName";
            public const string StorageAccessKey = "storageAccessKey";
        }
        public static string ServiceBusConnectionString
        {
            get
            {
                const string webJobConfigKey = "AzureWebJobsServiceBus";
                var environmentConnectionString = ConfigurationManager.ConnectionStrings[webJobConfigKey].ConnectionString;
                if (!string.IsNullOrEmpty(environmentConnectionString)) { return environmentConnectionString; }

                var serviceBusNamespace = WebJobConfiguration.Current.ServiceBusNamespace;
                var serviceBusSasKey = WebJobConfiguration.Current.ServiceBusSasKey;

                var connectionString = $"Endpoint=sb://{serviceBusNamespace}.servicebus.windows.net;" +
                                       "SharedAccessKeyName=RootManageSharedAccessKey;" +
                                       $"SharedAccessKey={serviceBusSasKey}";

                return connectionString;
            }
        }

        public static string WebJobDashboardConnectionString => FetchWebJobStorageConnectionString("AzureWebJobsDashboard");
        public static string WebJobStorageConnectionString => FetchWebJobStorageConnectionString("AzureWebJobsStorage");
        private static string FetchWebJobStorageConnectionString(string keyName)
        {
            var environmentConnectionString = ConfigurationManager.ConnectionStrings[keyName].ConnectionString;
            if (!string.IsNullOrEmpty(environmentConnectionString)) { return environmentConnectionString; }

            var storageAccountName = Current.StorageAccountName;
            var storageAccountKey = Current.StorageAccessKey;
            var connectionString = "DefaultEndpointsProtocol=https;" +
                                   $"AccountName={storageAccountName};" +
                                   $"AccountKey={storageAccountKey};";
            return connectionString;
        }

        public static WebJobConfiguration Current
        {
            get {
                try {
                    return (WebJobConfiguration) ConfigurationManager.GetSection(SectionName);
                }
                catch (ConfigurationErrorsException) {
                    return new WebJobConfiguration();
                }
            }
        }

        [ConfigurationProperty(Attributes.Namespace)]
        public string ServiceBusNamespace
        {
            get {
                return (string) this[Attributes.Namespace] ?? GetAppSetting(Attributes.Namespace);
            }
            set { this[Attributes.Namespace] = value; }
        }

        [ConfigurationProperty(Attributes.SasKey)]
        public string ServiceBusSasKey
        {
            get { return (string) this[Attributes.SasKey] ?? GetAppSetting(Attributes.SasKey); }
            set { this[Attributes.SasKey] = value; }
        }

        [ConfigurationProperty(Attributes.QueueName)]
        public string ServiceBusQueueName
        {
            get { return (string) this[Attributes.QueueName] ?? GetAppSetting(Attributes.QueueName); }
            set { this[Attributes.QueueName] = value; }
        }

        [ConfigurationProperty(Attributes.StorageAccountName)]
        public string StorageAccountName
        {
            get {
                return (string) this[Attributes.StorageAccountName] ?? GetAppSetting(Attributes.StorageAccountName);
            }
            set { this[Attributes.StorageAccountName] = value; }
        }

        [ConfigurationProperty(Attributes.StorageAccessKey)]
        public string StorageAccessKey
        {
            get {
                return (string) this[Attributes.StorageAccessKey] ?? GetAppSetting(Attributes.StorageAccessKey);
            }
            set { this[Attributes.StorageAccessKey] = value; }
        }

        public string GetAppSetting(string key)
        {
            var setting = $"{SectionName}.{key}";
            return ConfigurationManager.AppSettings[setting];
        }

    }
}