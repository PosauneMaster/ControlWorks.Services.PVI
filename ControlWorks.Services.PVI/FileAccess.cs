using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ControlWorks.Services.PVI
{
    public static class FileAccess
    {
        private static object _syncLock = new object();

        private static AutoResetEvent _waitHandle = new AutoResetEvent(false);

        public static string Read(string filepath)
        {
            _waitHandle.WaitOne(1000);
            string json = null;
            lock (_syncLock)
            {
                using (var reader = new StreamReader(filepath))
                {
                    json = reader.ReadToEnd();
                }
            }

            _waitHandle.Set();

            return json;
        }

        public static void Write(string filepath, string contents)
        {
            _waitHandle.WaitOne(1000);

            lock (_syncLock)
            {
                using (var writer = new StreamWriter(filepath, false))
                {
                    writer.WriteLine(contents);
                }
            }

            _waitHandle.Set();

        }
    }
}
