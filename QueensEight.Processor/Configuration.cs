using System;
using System.Configuration;

namespace QueensEight.Processor
{
    public class Configuration : ConfigurationSection
    {
        private const string SectionName = "QueensEight";

        /// <summary>
        ///     The current configuration from an application config file.
        /// </summary>
        public static Configuration Current
        {
            get
            {
                try
                {
                    return (Configuration) ConfigurationManager.GetSection(SectionName);
                }
                catch (ConfigurationErrorsException e)
                {
                    return new Configuration();
                }
            }
        }

        [ConfigurationProperty(Attributes.Namespace)]
        public string Namespace
        {
            get { return (string) this[Attributes.Namespace] ?? GetAppSetting(Attributes.Namespace); }
            set { this[Attributes.Namespace] = value; }
        }

        [ConfigurationProperty(Attributes.IssuerName)]
        public string IssuerName
        {
            get { return (string) this[Attributes.IssuerName] ?? GetAppSetting(Attributes.IssuerName); }
            set { this[Attributes.IssuerName] = value; }
        }

        [ConfigurationProperty(Attributes.IssuerKey)]
        public string IssuerKey
        {
            get { return (string) this[Attributes.IssuerKey] ?? GetAppSetting(Attributes.IssuerKey); }
            set { this[Attributes.IssuerKey] = value; }
        }

        public string GetAppSetting(string key)
        {
            string setting = string.Format("{0}.{1}", SectionName, key);
            return ConfigurationManager.AppSettings[setting];
        }

        private static class Attributes
        {
            public const string Namespace = "namespace";
            public const string IssuerName = "issuername";
            public const string IssuerKey = "issuerkey";
        }
    }
}