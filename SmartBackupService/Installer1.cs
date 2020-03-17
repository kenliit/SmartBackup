using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBackupService
{
    [RunInstaller(true)]
    public partial class Installer1 : System.Configuration.Install.Installer
    {
        public Installer1()
        {
            InitializeComponent();
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);

            var p = new System.Diagnostics.Process();
            p.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(this.Context.Parameters["AssemblyPath"]);
            p.StartInfo.FileName =  "SmartBackupService.exe";
            p.StartInfo.Arguments = "install start";
            p.Start();
        }

        public override void Uninstall(IDictionary savedState)
        {
            var p = new System.Diagnostics.Process();
            p.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(this.Context.Parameters["AssemblyPath"]);
            p.StartInfo.FileName = "SmartBackupService.exe";
            p.StartInfo.Arguments = "uninstall";
            p.Start();
            p.WaitForExit(10000);

            base.Uninstall(savedState);
        }
    }
}
