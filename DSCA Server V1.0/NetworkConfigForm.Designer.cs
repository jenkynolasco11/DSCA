namespace PruebaDLL
{
    partial class NetworkConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetworkConfigForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sipServerPortTextBox = new System.Windows.Forms.TextBox();
            this.serverIpLabel = new System.Windows.Forms.Label();
            this.sipServerIpTextBox = new System.Windows.Forms.TextBox();
            this.serverPortTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.interfacesComboBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.interfacesComboBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 186);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Interfaces";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.sipServerPortTextBox);
            this.groupBox4.Controls.Add(this.serverIpLabel);
            this.groupBox4.Controls.Add(this.sipServerIpTextBox);
            this.groupBox4.Controls.Add(this.serverPortTextBox);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(9, 55);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(251, 122);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Parametros de la red";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Direccion IP:";
            // 
            // sipServerPortTextBox
            // 
            this.sipServerPortTextBox.Location = new System.Drawing.Point(141, 93);
            this.sipServerPortTextBox.Name = "sipServerPortTextBox";
            this.sipServerPortTextBox.Size = new System.Drawing.Size(100, 20);
            this.sipServerPortTextBox.TabIndex = 9;
            this.sipServerPortTextBox.Text = "5060";
            // 
            // serverIpLabel
            // 
            this.serverIpLabel.AutoSize = true;
            this.serverIpLabel.Location = new System.Drawing.Point(138, 19);
            this.serverIpLabel.Name = "serverIpLabel";
            this.serverIpLabel.Size = new System.Drawing.Size(35, 13);
            this.serverIpLabel.TabIndex = 4;
            this.serverIpLabel.Text = "label3";
            // 
            // sipServerIpTextBox
            // 
            this.sipServerIpTextBox.Location = new System.Drawing.Point(141, 67);
            this.sipServerIpTextBox.Name = "sipServerIpTextBox";
            this.sipServerIpTextBox.Size = new System.Drawing.Size(100, 20);
            this.sipServerIpTextBox.TabIndex = 8;
            this.sipServerIpTextBox.Text = "10.0.0.23";
            // 
            // serverPortTextBox
            // 
            this.serverPortTextBox.Location = new System.Drawing.Point(141, 42);
            this.serverPortTextBox.Name = "serverPortTextBox";
            this.serverPortTextBox.Size = new System.Drawing.Size(51, 20);
            this.serverPortTextBox.TabIndex = 2;
            this.serverPortTextBox.Text = "4899";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Puerto Central Asterisk:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Puerto Servidor:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "IP Central Asterisk:";
            // 
            // interfacesComboBox
            // 
            this.interfacesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.interfacesComboBox.FormattingEnabled = true;
            this.interfacesComboBox.Location = new System.Drawing.Point(9, 19);
            this.interfacesComboBox.Name = "interfacesComboBox";
            this.interfacesComboBox.Size = new System.Drawing.Size(251, 21);
            this.interfacesComboBox.TabIndex = 14;
            this.interfacesComboBox.SelectedIndexChanged += new System.EventHandler(this.ShowIntIPAdd_OnIndexChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(166, 215);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Cancelar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(52, 215);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Ok";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OkButton_OnClick);
            // 
            // NetworkConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 250);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NetworkConfigForm";
            this.Text = "Administrador de Red";
            this.Load += new System.EventHandler(this.NetworkConfigForm_OnLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox sipServerPortTextBox;
        private System.Windows.Forms.Label serverIpLabel;
        private System.Windows.Forms.TextBox sipServerIpTextBox;
        private System.Windows.Forms.TextBox serverPortTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox interfacesComboBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;

    }
}