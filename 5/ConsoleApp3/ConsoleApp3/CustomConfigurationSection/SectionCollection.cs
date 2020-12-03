using System.Configuration;

namespace ConsoleApp3
{
    [ConfigurationCollection(typeof(SectionElement))]
    public class SectionCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SectionElement)element).Key;
        }

        public SectionElement this[int i]
        {
            get {return (SectionElement)BaseGet(i); }
        }
    }
}
