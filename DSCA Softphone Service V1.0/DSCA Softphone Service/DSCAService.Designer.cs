namespace DSCA_Softphone_Service
{
    partial class DSCAService
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
            this.serviceEventLog = new System.Diagnostics.EventLog();
            this.LogFileSystemWatcher = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.serviceEventLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogFileSystemWatcher)).BeginInit();
            // 
            // LogFileSystemWatcher
            // 
            this.LogFileSystemWatcher.NotifyFilter = System.IO.NotifyFilters.Size;
            // 
            // DSCAService
            // 
            this.CanHandleSessionChangeEvent = true;
            this.ServiceName = "DSCA Softphone Service";
            ((System.ComponentModel.ISupportInitialize)(this.serviceEventLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogFileSystemWatcher)).EndInit();

        }

        #endregion

        private System.Diagnostics.EventLog serviceEventLog;
        private System.IO.FileSystemWatcher LogFileSystemWatcher;
    }
}
