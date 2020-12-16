using System;
using System.IO;
using System.Configuration;
using res = ConsoleApp3.Resourses.Messages;

namespace ConsoleApp3
{
    class FIleSystemListener
    {
        int count = 0;

        private readonly FileSystemWatcher _fileSystemWatcher;

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
                Console.WriteLine(res.File+ " " + e.Name + " - " + e.ChangeType);

                var settings = ConfigurationManager.AppSettings;
                
                file = new FileInfo(e.FullPath);

                foreach (var setting in settings)
                {
                    if (file.Extension == setting.ToString())
                    {
                        Console.WriteLine(res.File + " " + e.Name + " " + res.FileFound + " " + setting +" "+ DateTime.Now.ToString(res.CreationDate));

                        directory = new DirectoryInfo(settings.Get(setting.ToString()));

                        if (!directory.Exists)
                        {
                            directory.Create();
                        }

                        file.MoveTo(directory.FullName + @"\" + (count += 1) + file.Name);

                        Console.WriteLine(res.File + " " + e.Name + " " + res.FileMove + " " + settings.Get(setting.ToString()));
                    }
                    else
                    {
                        directory = new DirectoryInfo("По умолчанию");

                        if (!directory.Exists)
                        {
                            directory.Create();
                        }

                        file.MoveTo(directory.FullName + @"\" + (count += 1) + file.Name);

                        Console.WriteLine(res.File + " " + e.Name + " " + res.FileMove + "По умолчанию");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
