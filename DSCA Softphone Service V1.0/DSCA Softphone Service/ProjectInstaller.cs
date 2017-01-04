using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;

namespace DSCA_Softphone_Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            this.AfterInstall += new InstallEventHandler(Installer_AfterInstall);
        }

        void Installer_AfterInstall(object sender, InstallEventArgs e)
        {
            ServiceController sc = new ServiceController("DSCA Softphone Service");
            sc.Start();
        }
    }
}
