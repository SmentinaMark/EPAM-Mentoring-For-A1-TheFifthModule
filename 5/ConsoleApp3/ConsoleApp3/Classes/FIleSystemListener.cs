using System;
using System.IO;
using System.Configuration;
using res = ConsoleApp3.Resourses.Messages;

namespace ConsoleApp3
{
    class FIleSystemListener
    {
        private readonly FileSystemWatcher _fileSystemWatcher;

        int count = 0;

        FileInfo file;
        DirectoryInfo directory;

        public FIleSystemListener(string path)
        {
            _fileSystemWatcher = new FileSystemWatcher(path)
            {
                EnableRaisingEvents = true
            };

            _fileSystemWatcher.Created += _fileSystemWatcher_Changed;
        }
        private void _fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                StartupCustomSection s = (StartupCustomSection)cfg.GetSection("CustomSection");

                Console.WriteLine(res.File + " " + e.Name + " - " + e.ChangeType);

                var settings = ConfigurationManager.AppSettings;

                file = new FileInfo(e.FullPath);

                foreach (var setting in settings)
                {
                    if (file.Extension == setting.ToString())
                    {

                        if(s.SectionItems[2].BoolValue==true)
                        {
                            Console.WriteLine(res.File + " " + e.Name + " " + res.FileFound + " " + setting + " " + DateTime.Now.ToString(res.CreationDate));
                        }
                        else
                        {
                            Console.WriteLine(res.File + " " + e.Name + " " + res.FileFound + " " + setting);
                        }
                       
                        directory = new DirectoryInfo(settings.Get(setting.ToString()));
                        if (!directory.Exists)
                        {
                            directory.Create();
                        }
                        Console.WriteLine(settings.Get(setting.ToString()));
                        

                        if(s.SectionItems[1].BoolValue == true)
                        {
                            file.MoveTo(directory.FullName + @"\" + (count += 1) + file.Name);
                        }
                        else
                        {
                            file.MoveTo(directory.FullName + @"\" + file.Name);
                        }

                        Console.WriteLine(res.File + " " + e.Name + " " + res.FileMove + " " + settings.Get(setting.ToString()));
                    }
                }

                if (File.Exists(e.FullPath))
                {
                    if(s.SectionItems[2].BoolValue == true)
                    {
                        Console.WriteLine(res.ExtentionNotFound + " " + DateTime.Now.ToString(res.CreationDate));
                    }
                    else
                    {
                        Console.WriteLine(res.ExtentionNotFound);
                    }

                    directory = new DirectoryInfo("По умолчанию");
                    if (!directory.Exists)
                    {
                        directory.Create();
                    }

                    if (s.SectionItems[1].BoolValue == true)
                    {
                        file.MoveTo(directory.FullName + @"\" + (count += 1) + file.Name);
                    }
                    else
                    {
                        file.MoveTo(directory.FullName + @"\" + file.Name);
                    }

                    Console.WriteLine(res.File + " " + e.Name + " " + res.FileMove + " По умолчанию");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
