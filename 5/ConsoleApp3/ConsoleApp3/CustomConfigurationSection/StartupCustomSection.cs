using System.Configuration;

namespace ConsoleApp3
{
    public class StartupCustomSection : ConfigurationSection
    {
        [ConfigurationProperty("Name")]
        public string Name
        {
            get { return (string)base["Name"]; }
        }

        [ConfigurationProperty("CustomElement")]
        public SectionCollection SectionItems
        {
            get { return (SectionCollection)base["CustomElement"]; }
        }
    }
}
