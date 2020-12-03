using System.Configuration;

namespace ConsoleApp3
{
    public class SectionElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsKey = true)]
        public string Key
        {
            get { return (string)base["key"]; }

            set { base["key"] = value; }
        }

        [ConfigurationProperty("value")]
        public string Value
        {
            get { return (string)base["value"]; } 

            set { base["value"] = value; }
        }
    }
}