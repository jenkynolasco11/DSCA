namespace DSCA_Softphone_Service
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DSCAServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.DSCAServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // DSCAServiceProcessInstaller
            // 
            this.DSCAServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.DSCAServiceProcessInstaller.Password = null;
            this.DSCAServiceProcessInstaller.Username = null;
            // 
            // DSCAServiceInstaller
            // 
            this.DSCAServiceInstaller.ServiceName = "DSCA Softphone Service";
            this.DSCAServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.DSCAServiceProcessInstaller,
            this.DSCAServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller DSCAServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller DSCAServiceInstaller;
    }
}