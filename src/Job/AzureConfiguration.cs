using System.Configuration;

namespace QueensEight.Job
{
    public class AzureConfiguration : ConfigurationSection {
        private const string SectionName = "QueensEight";
        private static class Attributes
        {
            public const string Namespace = "serviceBusNamespace";
            public const string SasKey = "serviceBusSasKey";
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

                var serviceBusNamespace = AzureConfiguration.Current.ServiceBusNamespace;
                var serviceBusSasKey = AzureConfiguration.Current.ServiceBusSasKey;

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

        public static AzureConfiguration Current
        {
            get {
                try {
                    return (AzureConfiguration) ConfigurationManager.GetSection(SectionName);
                }
                catch (ConfigurationErrorsException) {
                    return new AzureConfiguration();
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