using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ControlWorks.Services.PVI
{
    public interface IFileWrapper
    {
        bool Exists(string filename);
        string Read(string filepath);
        void Write(string filepath, string contents);
        string CreateBackup(string path);
    }

    public class FileWrapper : IFileWrapper
    {
        private readonly object _syncLock = new object();
        private readonly AutoResetEvent _waitHandle = new AutoResetEvent(false);


        public bool Exists(string filename)
        {
            return File.Exists(filename);
        }

        public string Read(string filepath)
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

        public void Write(string filepath, string contents)
        {
            _waitHandle.WaitOne(1000);

            lock (_syncLock)
            {
                var fi = new FileInfo(filepath);
                if (!Directory.Exists(fi.DirectoryName) && !String.IsNullOrEmpty(fi.DirectoryName))
                {
                    Directory.CreateDirectory(fi.DirectoryName);
                }

                using (var writer = new StreamWriter(filepath, false))
                {
                    writer.WriteLine(contents);
                }
            }

            _waitHandle.Set();

        }

        public string CreateBackup(string path)
        {
            var fi = new FileInfo(path);
            if (fi.Exists)
            {
                var backupPath = $"{fi.FullName}.{DateTime.Now:yyyyMMddHHmmss}.bak";
                fi.CopyTo(backupPath);
                return backupPath;
            }
            return string.Empty;
        }
    }
}
