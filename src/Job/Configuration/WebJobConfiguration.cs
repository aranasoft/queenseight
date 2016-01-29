﻿using System.Configuration;

namespace QueensEight.Job.Configuration
{
    public class WebJobConfiguration : ConfigurationSection
    {
        private const string SectionName = "QueensEightWebJob";
        private static class Attributes
        {
            public const string Namespace = "serviceBusNamespace";
            public const string SasKey = "serviceBusSasKey";
            public const string RequestQueueName = "serviceBusQueueName";
            public const string StorageAccountName = "storageAccountName";
            public const string StorageAccessKey = "storageAccessKey";
            public const string ApiNotificationUrl = "notificationApiUrl";
        }
        public static string ServiceBusConnectionString
        {
            get
            {
                const string webJobConfigKey = "AzureWebJobsServiceBus";
                var environmentConnectionString = ConfigurationManager.ConnectionStrings[webJobConfigKey].ConnectionString;
                if (!string.IsNullOrEmpty(environmentConnectionString)) { return environmentConnectionString; }

                var serviceBusNamespace = Current.ServiceBusNamespace;
                var serviceBusSasKey = Current.ServiceBusSasKey;

                var connectionString = $"Endpoint=sb://{serviceBusNamespace}.servicebus.windows.net;" +
                                       "SharedAccessKeyName=RootManageSharedAccessKey;" +
                                       $"SharedAccessKey={serviceBusSasKey}";

                return connectionString;
            }
        }

        public static string QueueName => Current.ServiceBusQueueName;
        public static string NotificationUrl
        {
            get
            {
                const string notificationUrlConfigKey = "NotifcationApiUrl";
                var environmentNotificationApiUrl = ConfigurationManager.AppSettings[notificationUrlConfigKey];
                if (!string.IsNullOrEmpty(environmentNotificationApiUrl)) {
                    return environmentNotificationApiUrl;
                }

                return Current.ApiNotificationUrl;
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
            get
            {
                try
                {
                    return (WebJobConfiguration)ConfigurationManager.GetSection(SectionName);
                }
                catch (ConfigurationErrorsException)
                {
                    return new WebJobConfiguration();
                }
            }
        }

        [ConfigurationProperty(Attributes.Namespace)]
        public string ServiceBusNamespace
        {
            get
            {
                return (string)this[Attributes.Namespace] ?? GetAppSetting(Attributes.Namespace);
            }
            set { this[Attributes.Namespace] = value; }
        }

        [ConfigurationProperty(Attributes.SasKey)]
        public string ServiceBusSasKey
        {
            get { return (string)this[Attributes.SasKey] ?? GetAppSetting(Attributes.SasKey); }
            set { this[Attributes.SasKey] = value; }
        }

        [ConfigurationProperty(Attributes.RequestQueueName)]
        public string ServiceBusQueueName
        {
            get { return (string)this[Attributes.RequestQueueName] ?? GetAppSetting(Attributes.RequestQueueName); }
            set { this[Attributes.RequestQueueName] = value; }
        }

        [ConfigurationProperty(Attributes.StorageAccountName)]
        public string StorageAccountName
        {
            get
            {
                return (string)this[Attributes.StorageAccountName] ?? GetAppSetting(Attributes.StorageAccountName);
            }
            set { this[Attributes.StorageAccountName] = value; }
        }

        [ConfigurationProperty(Attributes.StorageAccessKey)]
        public string StorageAccessKey
        {
            get
            {
                return (string)this[Attributes.StorageAccessKey] ?? GetAppSetting(Attributes.StorageAccessKey);
            }
            set { this[Attributes.StorageAccessKey] = value; }
        }

        [ConfigurationProperty(Attributes.ApiNotificationUrl)]
        public string ApiNotificationUrl
        {
            get
            {
                return (string)this[Attributes.ApiNotificationUrl] ?? GetAppSetting(Attributes.ApiNotificationUrl);
            }
            set { this[Attributes.ApiNotificationUrl] = value; }
        }


        public string GetAppSetting(string key)
        {
            var setting = $"{SectionName}.{key}";
            return ConfigurationManager.AppSettings[setting];
        }

    }
}