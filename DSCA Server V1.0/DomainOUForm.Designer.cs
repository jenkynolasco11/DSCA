namespace PruebaDLL
{
    partial class DomainOUForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DomainOUForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupFolderTextBox = new System.Windows.Forms.TextBox();
            this.groupFolderLabel = new System.Windows.Forms.Label();
            this.domainLabel = new System.Windows.Forms.Label();
            this.domainShLabel = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.groupFolderTextBoxToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupFolderTextBox);
            this.groupBox1.Controls.Add(this.groupFolderLabel);
            this.groupBox1.Controls.Add(this.domainLabel);
            this.groupBox1.Controls.Add(this.domainShLabel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 99);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parametros de Dominio";
            // 
            // groupFolderTextBox
            // 
            this.groupFolderTextBox.Location = new System.Drawing.Point(118, 57);
            this.groupFolderTextBox.Name = "groupFolderTextBox";
            this.groupFolderTextBox.Size = new System.Drawing.Size(138, 20);
            this.groupFolderTextBox.TabIndex = 3;
            this.groupFolderTextBoxToolTip.SetToolTip(this.groupFolderTextBox, "OU=Grupo,DC=Dominio,DC=Dominio");
            // 
            // groupFolderLabel
            // 
            this.groupFolderLabel.AutoSize = true;
            this.groupFolderLabel.Location = new System.Drawing.Point(13, 60);
            this.groupFolderLabel.Name = "groupFolderLabel";
            this.groupFolderLabel.Size = new System.Drawing.Size(99, 13);
            this.groupFolderLabel.TabIndex = 2;
            this.groupFolderLabel.Text = "Carpeta de Grupos:";
            // 
            // domainLabel
            // 
            this.domainLabel.AutoSize = true;
            this.domainLabel.Location = new System.Drawing.Point(118, 29);
            this.domainLabel.Name = "domainLabel";
            this.domainLabel.Size = new System.Drawing.Size(35, 13);
            this.domainLabel.TabIndex = 1;
            this.domainLabel.Text = "label2";
            // 
            // domainShLabel
            // 
            this.domainShLabel.AutoSize = true;
            this.domainShLabel.Location = new System.Drawing.Point(64, 28);
            this.domainShLabel.Name = "domainShLabel";
            this.domainShLabel.Size = new System.Drawing.Size(48, 13);
            this.domainShLabel.TabIndex = 0;
            this.domainShLabel.Text = "Dominio:";
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(41, 132);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "Aceptar";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_OnClick);
            // 
            // ExitButton
            // 
            this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ExitButton.Location = new System.Drawing.Point(177, 132);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.Text = "Salir";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.CancelButton_OnClick);
            // 
            // groupFolderTextBoxToolTip
            // 
            this.groupFolderTextBoxToolTip.IsBalloon = true;
            this.groupFolderTextBoxToolTip.ShowAlways = true;
            this.groupFolderTextBoxToolTip.ToolTipTitle = "Ejemplo";
            // 
            // DomainOUForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 167);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DomainOUForm";
            this.Text = "Configurar Dominio";
            this.Load += new System.EventHandler(this.DomainOUForm_OnLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox groupFolderTextBox;
        private System.Windows.Forms.Label groupFolderLabel;
        private System.Windows.Forms.Label domainLabel;
        private System.Windows.Forms.Label domainShLabel;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.ToolTip groupFolderTextBoxToolTip;
    }
}