using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;

namespace ConsoleApp3
{
    class Program: ConfigurationSection
    {
        public static async Task Main(string[] args)
        {
            try
            {
                #region ConfigSectionAndCulture
                Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                StartupCustomSection s = (StartupCustomSection)cfg.GetSection("CustomSection");

                if(s != null)
                {
                    System.Diagnostics.Debug.WriteLine(s.SectionItems[0].Key);
                    System.Diagnostics.Debug.WriteLine(s.SectionItems[0].Value);
                    s.SectionItems[0].Value = "en-EN";
                    
                    System.Diagnostics.Debug.WriteLine(s.SectionItems[1].Key);
                    System.Diagnostics.Debug.WriteLine(s.SectionItems[1].Value);
                    s.SectionItems[1].Value = @"C:\";
                }

                string culture = s.SectionItems[0].Value;
                string path = s.SectionItems[1].Value;
                string counter = s.SectionItems[2].Value;

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
                #endregion

                var logger = new FileSystemLogger();
                logger.AddListen(path);

                cfg.Save();

                while (true)
                {
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
