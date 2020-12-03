using System;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class FileSystemLogger
    {
        private readonly List<FIleSystemListener> _filesystemListeners;
        public FileSystemLogger()
        {
            _filesystemListeners = new List<FIleSystemListener>();
        }
        public void AddListen(string path)
        {
            try
            {
                var listener = new FIleSystemListener(path);

                _filesystemListeners.Add(listener);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
