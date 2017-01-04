namespace PruebaDLL
{
    partial class AddGroupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddGroupForm));
            this.cancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.groupNameTextBox = new System.Windows.Forms.TextBox();
            this.groupDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.groupNameLabel = new System.Windows.Forms.Label();
            this.groupDescriptionLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupOULabel = new System.Windows.Forms.Label();
            this.groupScopeGroupBox = new System.Windows.Forms.GroupBox();
            this.universalRadioButton = new System.Windows.Forms.RadioButton();
            this.globalRadioButton = new System.Windows.Forms.RadioButton();
            this.domainLocalRadioButton = new System.Windows.Forms.RadioButton();
            this.groupTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.distRadioButton = new System.Windows.Forms.RadioButton();
            this.securityRadioButton = new System.Windows.Forms.RadioButton();
            this.groupImagePictureBox = new System.Windows.Forms.PictureBox();
            this.groupScopeGroupBox.SuspendLayout();
            this.groupTypeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupImagePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(261, 337);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancelar";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(159, 337);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "Ok";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // groupNameTextBox
            // 
            this.groupNameTextBox.Location = new System.Drawing.Point(12, 105);
            this.groupNameTextBox.Name = "groupNameTextBox";
            this.groupNameTextBox.Size = new System.Drawing.Size(324, 20);
            this.groupNameTextBox.TabIndex = 3;
            this.groupNameTextBox.TextChanged += new System.EventHandler(this.GroupName_TextChanged);
            // 
            // groupDescriptionTextBox
            // 
            this.groupDescriptionTextBox.Location = new System.Drawing.Point(12, 148);
            this.groupDescriptionTextBox.Name = "groupDescriptionTextBox";
            this.groupDescriptionTextBox.Size = new System.Drawing.Size(324, 20);
            this.groupDescriptionTextBox.TabIndex = 4;
            // 
            // groupNameLabel
            // 
            this.groupNameLabel.AutoSize = true;
            this.groupNameLabel.Location = new System.Drawing.Point(12, 89);
            this.groupNameLabel.Name = "groupNameLabel";
            this.groupNameLabel.Size = new System.Drawing.Size(96, 13);
            this.groupNameLabel.TabIndex = 5;
            this.groupNameLabel.Text = "Nombre del Grupo:";
            // 
            // groupDescriptionLabel
            // 
            this.groupDescriptionLabel.AutoSize = true;
            this.groupDescriptionLabel.Location = new System.Drawing.Point(12, 132);
            this.groupDescriptionLabel.Name = "groupDescriptionLabel";
            this.groupDescriptionLabel.Size = new System.Drawing.Size(115, 13);
            this.groupDescriptionLabel.TabIndex = 6;
            this.groupDescriptionLabel.Text = "Descripcion del Grupo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Crear en:";
            // 
            // groupOULabel
            // 
            this.groupOULabel.AutoSize = true;
            this.groupOULabel.Location = new System.Drawing.Point(148, 31);
            this.groupOULabel.Name = "groupOULabel";
            this.groupOULabel.Size = new System.Drawing.Size(35, 13);
            this.groupOULabel.TabIndex = 8;
            this.groupOULabel.Text = "label4";
            // 
            // groupScopeGroupBox
            // 
            this.groupScopeGroupBox.Controls.Add(this.universalRadioButton);
            this.groupScopeGroupBox.Controls.Add(this.globalRadioButton);
            this.groupScopeGroupBox.Controls.Add(this.domainLocalRadioButton);
            this.groupScopeGroupBox.Location = new System.Drawing.Point(12, 182);
            this.groupScopeGroupBox.Name = "groupScopeGroupBox";
            this.groupScopeGroupBox.Size = new System.Drawing.Size(160, 117);
            this.groupScopeGroupBox.TabIndex = 9;
            this.groupScopeGroupBox.TabStop = false;
            this.groupScopeGroupBox.Text = "Alcance del grupo";
            // 
            // universalRadioButton
            // 
            this.universalRadioButton.AutoSize = true;
            this.universalRadioButton.Location = new System.Drawing.Point(20, 73);
            this.universalRadioButton.Name = "universalRadioButton";
            this.universalRadioButton.Size = new System.Drawing.Size(69, 17);
            this.universalRadioButton.TabIndex = 2;
            this.universalRadioButton.TabStop = true;
            this.universalRadioButton.Text = "Universal";
            this.universalRadioButton.UseVisualStyleBackColor = true;
            // 
            // globalRadioButton
            // 
            this.globalRadioButton.AutoSize = true;
            this.globalRadioButton.Location = new System.Drawing.Point(20, 50);
            this.globalRadioButton.Name = "globalRadioButton";
            this.globalRadioButton.Size = new System.Drawing.Size(55, 17);
            this.globalRadioButton.TabIndex = 1;
            this.globalRadioButton.TabStop = true;
            this.globalRadioButton.Text = "Global";
            this.globalRadioButton.UseVisualStyleBackColor = true;
            // 
            // domainLocalRadioButton
            // 
            this.domainLocalRadioButton.AutoSize = true;
            this.domainLocalRadioButton.Location = new System.Drawing.Point(20, 27);
            this.domainLocalRadioButton.Name = "domainLocalRadioButton";
            this.domainLocalRadioButton.Size = new System.Drawing.Size(92, 17);
            this.domainLocalRadioButton.TabIndex = 0;
            this.domainLocalRadioButton.TabStop = true;
            this.domainLocalRadioButton.Text = "Dominio Local";
            this.domainLocalRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupTypeGroupBox
            // 
            this.groupTypeGroupBox.Controls.Add(this.distRadioButton);
            this.groupTypeGroupBox.Controls.Add(this.securityRadioButton);
            this.groupTypeGroupBox.Location = new System.Drawing.Point(178, 182);
            this.groupTypeGroupBox.Name = "groupTypeGroupBox";
            this.groupTypeGroupBox.Size = new System.Drawing.Size(158, 117);
            this.groupTypeGroupBox.TabIndex = 10;
            this.groupTypeGroupBox.TabStop = false;
            this.groupTypeGroupBox.Text = "Tipo de grupo";
            // 
            // distRadioButton
            // 
            this.distRadioButton.AutoSize = true;
            this.distRadioButton.Location = new System.Drawing.Point(18, 50);
            this.distRadioButton.Name = "distRadioButton";
            this.distRadioButton.Size = new System.Drawing.Size(80, 17);
            this.distRadioButton.TabIndex = 1;
            this.distRadioButton.TabStop = true;
            this.distRadioButton.Text = "Distribucion";
            this.distRadioButton.UseVisualStyleBackColor = true;
            // 
            // securityRadioButton
            // 
            this.securityRadioButton.AutoSize = true;
            this.securityRadioButton.Location = new System.Drawing.Point(18, 27);
            this.securityRadioButton.Name = "securityRadioButton";
            this.securityRadioButton.Size = new System.Drawing.Size(73, 17);
            this.securityRadioButton.TabIndex = 0;
            this.securityRadioButton.TabStop = true;
            this.securityRadioButton.Text = "Seguridad";
            this.securityRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupImagePictureBox
            // 
            this.groupImagePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupImagePictureBox.Image = global::PruebaDLL.Properties.Resources.user_group;
            this.groupImagePictureBox.InitialImage = null;
            this.groupImagePictureBox.Location = new System.Drawing.Point(12, 12);
            this.groupImagePictureBox.Name = "groupImagePictureBox";
            this.groupImagePictureBox.Size = new System.Drawing.Size(61, 61);
            this.groupImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.groupImagePictureBox.TabIndex = 0;
            this.groupImagePictureBox.TabStop = false;
            // 
            // AddGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 372);
            this.Controls.Add(this.groupTypeGroupBox);
            this.Controls.Add(this.groupScopeGroupBox);
            this.Controls.Add(this.groupOULabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupDescriptionLabel);
            this.Controls.Add(this.groupNameLabel);
            this.Controls.Add(this.groupDescriptionTextBox);
            this.Controls.Add(this.groupNameTextBox);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.groupImagePictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddGroupForm";
            this.Text = "Agregar Grupo";
            this.groupScopeGroupBox.ResumeLayout(false);
            this.groupScopeGroupBox.PerformLayout();
            this.groupTypeGroupBox.ResumeLayout(false);
            this.groupTypeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupImagePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox groupImagePictureBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.TextBox groupNameTextBox;
        private System.Windows.Forms.TextBox groupDescriptionTextBox;
        private System.Windows.Forms.Label groupNameLabel;
        private System.Windows.Forms.Label groupDescriptionLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label groupOULabel;
        private System.Windows.Forms.GroupBox groupScopeGroupBox;
        private System.Windows.Forms.RadioButton universalRadioButton;
        private System.Windows.Forms.RadioButton globalRadioButton;
        private System.Windows.Forms.RadioButton domainLocalRadioButton;
        private System.Windows.Forms.GroupBox groupTypeGroupBox;
        private System.Windows.Forms.RadioButton distRadioButton;
        private System.Windows.Forms.RadioButton securityRadioButton;
    }
}