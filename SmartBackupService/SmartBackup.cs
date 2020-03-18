using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SmartBackupService
{
    public class SmartBackup
    {
        private readonly Timer _timer;

        public SmartBackup()
        {
            _timer = new Timer(5000) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            string[] lines = new string[] { DateTime.Now.ToString() };
            File.AppendAllLines(@"D:\IDG\Heartbeat.txt", lines);
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Pause()
        {

        }

        public void Resume()
        {

        }

        public void ExecuteCustomeCommand(int command)
        {
            switch (command)
            {
                case 1:

                default:
                    break;
            }
        }
    }
}
