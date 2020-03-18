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
                x.EnablePauseAndContinue();
                x.Service<SmartBackup>(s =>
                {
                    s.ConstructUsing(smartBackup => new SmartBackup());
                    s.WhenStarted(smartBackup => smartBackup.Start());
                    s.WhenStopped(smartBackup => smartBackup.Stop());
                    s.WhenPaused(smartBackup => smartBackup.Pause());
                    s.WhenContinued(smartBackup => smartBackup.Resume());
                    s.WhenCustomCommandReceived((smartBackup, hostControl, command) => smartBackup.ExecuteCustomeCommand(command));
                });

                x.RunAsLocalSystem();

                x.SetServiceName("SmartBackupService");
                x.SetDisplayName("Smart Backup Service");
                x.SetDescription("This is the smart service for backing up your files to a local backup drive.");
                x.StartAutomatically();
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
