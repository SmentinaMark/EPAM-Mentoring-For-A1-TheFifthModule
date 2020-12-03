using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;

namespace ConsoleApp3
{
    class Program: ConfigurationSection
    {
        static async Task Main(string[] args)
        {
            try
            {
                #region ConfigSectionAndCulture
                Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                StartupCustomSection s = (StartupCustomSection)cfg.GetSection("CustomSection");

                s.SectionItems[0].Value = "en";

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(s.SectionItems[0].Value);
                #endregion

                var logger = new FileSystemLogger();
                logger.AddListen(@"C:\");

                while(true)
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
