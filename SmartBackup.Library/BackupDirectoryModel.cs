using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBackup.Library
{
    public class BackupDirectoryModel
    {
        /// <summary>
        /// Destination Folder of this folder
        /// </summary>
        public string Destination { get; set; }
        /// <summary>
        /// Watcher of this folder
        /// </summary>
        public FileSystemWatcher watcher { get; set; }
        /// <summary>
        /// The backup status of this folder:
        /// -1   Error (Path not exist, etc)
        /// 0    Backup Completed (synced)
        /// 1    Successfully Inited
        /// 2    Currently Processing
        /// </summary>
        public int Status { get; set; }

        public BackupDirectoryModel(string source, string destination)
        {
            if (CheckPath(source) != "ok" || CheckPath(destination) != "ok")
            {
                Status = -1;
                return;
            }

            Destination = destination;
            watcher.Path = source;
            if (EnableWatcher() != "ok")
            {
                Status = -1;
                return;
            }

            Status = 1;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        private string EnableWatcher()
        {
            try
            {
                watcher.Created += Watcher_Created;
                watcher.Renamed += Watcher_Renamed;
                watcher.Deleted += Watcher_Deleted;
                watcher.Changed += Watcher_Changed;
                watcher.EnableRaisingEvents = true;
                watcher.IncludeSubdirectories = true;
                Status = 1;

                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private string CheckPath(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    return "Not Found";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;

                throw;
            }

            return "ok";
        }
    }
}
