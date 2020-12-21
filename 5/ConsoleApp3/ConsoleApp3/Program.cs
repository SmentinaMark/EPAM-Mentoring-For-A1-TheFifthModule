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
                Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                StartupCustomSection s = (StartupCustomSection)cfg.GetSection("CustomSection");

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(s.SectionItems[0].Value);

                var logger = new FileSystemLogger();

                logger.AddListen(s.SectionItems[3].Value);
                logger.AddListen(s.SectionItems[4].Value);

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
