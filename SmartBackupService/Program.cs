using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SmartBackupService
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<SmartBackup>(s =>
                {
                    s.ConstructUsing(smartBackup => new SmartBackup());
                    s.WhenStarted(smartBackup => smartBackup.Start());
                    s.WhenStopped(smartBackup => smartBackup.Stop());
                });

                x.RunAsLocalSystem();

                x.SetServiceName("SmartBackupService");
                x.SetDisplayName("Smart Backup Service");
                x.SetDescription("This is the smart service for backing up your files to a local backup drive.");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
