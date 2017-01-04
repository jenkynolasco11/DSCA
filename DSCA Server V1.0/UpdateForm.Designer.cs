namespace PruebaDLL
{
    partial class UpdateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
            this.acceptButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.userShowLabel = new System.Windows.Forms.Label();
            this.extensionShowLabel = new System.Windows.Forms.Label();
            this.displayidShowLabel = new System.Windows.Forms.Label();
            this.passShowLabel = new System.Windows.Forms.Label();
            this.extensionTextBox = new System.Windows.Forms.TextBox();
            this.displayidTextBox = new System.Windows.Forms.TextBox();
            this.passTextBox = new System.Windows.Forms.TextBox();
            this.userLabel = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(26, 184);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 0;
            this.acceptButton.Text = "Aceptar";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(129, 184);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancelar";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // userShowLabel
            // 
            this.userShowLabel.AutoSize = true;
            this.userShowLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userShowLabel.Location = new System.Drawing.Point(33, 27);
            this.userShowLabel.Name = "userShowLabel";
            this.userShowLabel.Size = new System.Drawing.Size(50, 13);
            this.userShowLabel.TabIndex = 2;
            this.userShowLabel.Text = "Usuario";
            // 
            // extensionShowLabel
            // 
            this.extensionShowLabel.AutoSize = true;
            this.extensionShowLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.extensionShowLabel.Location = new System.Drawing.Point(23, 60);
            this.extensionShowLabel.Name = "extensionShowLabel";
            this.extensionShowLabel.Size = new System.Drawing.Size(62, 13);
            this.extensionShowLabel.TabIndex = 3;
            this.extensionShowLabel.Text = "Extension";
            // 
            // displayidShowLabel
            // 
            this.displayidShowLabel.AutoSize = true;
            this.displayidShowLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayidShowLabel.Location = new System.Drawing.Point(12, 97);
            this.displayidShowLabel.Name = "displayidShowLabel";
            this.displayidShowLabel.Size = new System.Drawing.Size(76, 13);
            this.displayidShowLabel.TabIndex = 4;
            this.displayidShowLabel.Text = "ID a mostrar";
            // 
            // passShowLabel
            // 
            this.passShowLabel.AutoSize = true;
            this.passShowLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passShowLabel.Location = new System.Drawing.Point(23, 134);
            this.passShowLabel.Name = "passShowLabel";
            this.passShowLabel.Size = new System.Drawing.Size(61, 13);
            this.passShowLabel.TabIndex = 5;
            this.passShowLabel.Text = "Password";
            // 
            // extensionTextBox
            // 
            this.extensionTextBox.Location = new System.Drawing.Point(95, 57);
            this.extensionTextBox.Name = "extensionTextBox";
            this.extensionTextBox.Size = new System.Drawing.Size(100, 20);
            this.extensionTextBox.TabIndex = 6;
            // 
            // displayidTextBox
            // 
            this.displayidTextBox.Location = new System.Drawing.Point(95, 94);
            this.displayidTextBox.Name = "displayidTextBox";
            this.displayidTextBox.Size = new System.Drawing.Size(100, 20);
            this.displayidTextBox.TabIndex = 7;
            // 
            // passTextBox
            // 
            this.passTextBox.Location = new System.Drawing.Point(95, 131);
            this.passTextBox.Name = "passTextBox";
            this.passTextBox.PasswordChar = '*';
            this.passTextBox.Size = new System.Drawing.Size(100, 20);
            this.passTextBox.TabIndex = 8;
            this.passTextBox.UseSystemPasswordChar = true;
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(102, 27);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(35, 13);
            this.userLabel.TabIndex = 9;
            this.userLabel.Text = "label5";
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.userShowLabel);
            this.groupBox.Controls.Add(this.userLabel);
            this.groupBox.Controls.Add(this.acceptButton);
            this.groupBox.Controls.Add(this.passTextBox);
            this.groupBox.Controls.Add(this.cancelButton);
            this.groupBox.Controls.Add(this.displayidTextBox);
            this.groupBox.Controls.Add(this.extensionShowLabel);
            this.groupBox.Controls.Add(this.extensionTextBox);
            this.groupBox.Controls.Add(this.displayidShowLabel);
            this.groupBox.Controls.Add(this.passShowLabel);
            this.groupBox.Location = new System.Drawing.Point(12, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(228, 228);
            this.groupBox.TabIndex = 10;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Parametros";
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 252);
            this.Controls.Add(this.groupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UpdateForm";
            this.Text = "Modificar";
            this.Shown += new System.EventHandler(this.updateForm_Shown);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label userShowLabel;
        private System.Windows.Forms.Label extensionShowLabel;
        private System.Windows.Forms.Label displayidShowLabel;
        private System.Windows.Forms.Label passShowLabel;
        private System.Windows.Forms.TextBox extensionTextBox;
        private System.Windows.Forms.TextBox displayidTextBox;
        private System.Windows.Forms.TextBox passTextBox;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.GroupBox groupBox;
    }
}