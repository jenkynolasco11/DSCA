namespace PruebaDLL
{
    partial class EnumSQLInstances
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnumSQLInstances));
            this.okButton = new System.Windows.Forms.Button();
            this.instancesComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.newDBCheckBox = new System.Windows.Forms.CheckBox();
            this.serversComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nameDBTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.createDBButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(24, 217);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 26);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_OnClick);
            // 
            // instancesComboBox
            // 
            this.instancesComboBox.FormattingEnabled = true;
            this.instancesComboBox.Location = new System.Drawing.Point(9, 32);
            this.instancesComboBox.Name = "instancesComboBox";
            this.instancesComboBox.Size = new System.Drawing.Size(253, 21);
            this.instancesComboBox.TabIndex = 1;
            this.instancesComboBox.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChanged_OnChange);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.closeButton);
            this.groupBox1.Controls.Add(this.newDBCheckBox);
            this.groupBox1.Controls.Add(this.serversComboBox);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.instancesComboBox);
            this.groupBox1.Controls.Add(this.okButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 249);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Motor de la Base de Datos";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(146, 217);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(99, 26);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Salir";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseButton_OnClick);
            // 
            // newDBCheckBox
            // 
            this.newDBCheckBox.AutoSize = true;
            this.newDBCheckBox.Location = new System.Drawing.Point(162, 74);
            this.newDBCheckBox.Name = "newDBCheckBox";
            this.newDBCheckBox.Size = new System.Drawing.Size(86, 17);
            this.newDBCheckBox.TabIndex = 0;
            this.newDBCheckBox.Text = "Crear Nueva";
            this.newDBCheckBox.UseVisualStyleBackColor = true;
            this.newDBCheckBox.CheckedChanged += new System.EventHandler(this.CheckedChanged_OnCheck);
            // 
            // serversComboBox
            // 
            this.serversComboBox.FormattingEnabled = true;
            this.serversComboBox.Location = new System.Drawing.Point(6, 72);
            this.serversComboBox.Name = "serversComboBox";
            this.serversComboBox.Size = new System.Drawing.Size(146, 21);
            this.serversComboBox.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nameDBTextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.createDBButton);
            this.groupBox2.Location = new System.Drawing.Point(9, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(253, 112);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Crear Base de Datos";
            // 
            // nameDBTextBox
            // 
            this.nameDBTextBox.Enabled = false;
            this.nameDBTextBox.Location = new System.Drawing.Point(9, 32);
            this.nameDBTextBox.Name = "nameDBTextBox";
            this.nameDBTextBox.Size = new System.Drawing.Size(238, 20);
            this.nameDBTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nombre de la Base de Datos";
            // 
            // createDBButton
            // 
            this.createDBButton.Enabled = false;
            this.createDBButton.Location = new System.Drawing.Point(6, 83);
            this.createDBButton.Name = "createDBButton";
            this.createDBButton.Size = new System.Drawing.Size(75, 23);
            this.createDBButton.TabIndex = 0;
            this.createDBButton.Text = "Crear";
            this.createDBButton.UseVisualStyleBackColor = true;
            this.createDBButton.Click += new System.EventHandler(this.createDBButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Bases de Datos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Instancias de SQL Server";
            // 
            // EnumSQLInstances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EnumSQLInstances";
            this.Text = "Conecte con SQL Server";
            this.Load += new System.EventHandler(this.Form_OnLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ComboBox instancesComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox serversComboBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox newDBCheckBox;
        private System.Windows.Forms.TextBox nameDBTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button createDBButton;
        private System.Windows.Forms.Button closeButton;
    }
}