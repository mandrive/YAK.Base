using System.Configuration;

namespace Yak.SearchEngine.Utils
{
    class ElasticConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("elasticServerConfiguration", IsRequired = true)]
        public ElasticServerConfigurationElement ElasticServer
        {
            get
            {
                return (ElasticServerConfigurationElement)this["elasticServerConfiguration"];
            }

            set
            {
                this["elasticServerConfiguration"] = value;
            }
        }
    }

    public class ElasticServerConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("serverAddress", IsRequired = true)]
        public string ServerAddress
        {
            get
            {
                return (string)this["serverAddress"];
            }

            set
            {
                this["serverAddress"] = value;
            }
        }

        [ConfigurationProperty("indexName", IsRequired = true)]
        public string IndexName
        {
            get
            {
                return (string)this["indexName"];
            }

            set
            {
                this["indexName"] = value;
            }
        }
    }
}
