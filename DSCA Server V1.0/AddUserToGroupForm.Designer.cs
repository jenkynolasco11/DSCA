namespace PruebaDLL
{
    partial class AddUserToGroupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddUserToGroupForm));
            this.usersGroupBox = new System.Windows.Forms.GroupBox();
            this.addUsersToGroupListBox = new System.Windows.Forms.ListBox();
            this.addUsersToGroupAddButton = new System.Windows.Forms.Button();
            this.addUsersToGroupCancelButton = new System.Windows.Forms.Button();
            this.treeViewImageList = new System.Windows.Forms.ImageList(this.components);
            this.usersGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // usersGroupBox
            // 
            this.usersGroupBox.Controls.Add(this.addUsersToGroupListBox);
            this.usersGroupBox.Location = new System.Drawing.Point(12, 12);
            this.usersGroupBox.Name = "usersGroupBox";
            this.usersGroupBox.Size = new System.Drawing.Size(268, 154);
            this.usersGroupBox.TabIndex = 0;
            this.usersGroupBox.TabStop = false;
            this.usersGroupBox.Text = "Usuarios";
            // 
            // addUsersToGroupListBox
            // 
            this.addUsersToGroupListBox.FormattingEnabled = true;
            this.addUsersToGroupListBox.Location = new System.Drawing.Point(6, 19);
            this.addUsersToGroupListBox.Name = "addUsersToGroupListBox";
            this.addUsersToGroupListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.addUsersToGroupListBox.Size = new System.Drawing.Size(256, 121);
            this.addUsersToGroupListBox.TabIndex = 0;
            // 
            // addUsersToGroupAddButton
            // 
            this.addUsersToGroupAddButton.Location = new System.Drawing.Point(18, 199);
            this.addUsersToGroupAddButton.Name = "addUsersToGroupAddButton";
            this.addUsersToGroupAddButton.Size = new System.Drawing.Size(75, 23);
            this.addUsersToGroupAddButton.TabIndex = 1;
            this.addUsersToGroupAddButton.Text = "Agregar";
            this.addUsersToGroupAddButton.UseVisualStyleBackColor = true;
            this.addUsersToGroupAddButton.Click += new System.EventHandler(this.addUsersToGroupAddButton_Click);
            // 
            // addUsersToGroupCancelButton
            // 
            this.addUsersToGroupCancelButton.Location = new System.Drawing.Point(199, 199);
            this.addUsersToGroupCancelButton.Name = "addUsersToGroupCancelButton";
            this.addUsersToGroupCancelButton.Size = new System.Drawing.Size(75, 23);
            this.addUsersToGroupCancelButton.TabIndex = 2;
            this.addUsersToGroupCancelButton.Text = "Cancelar";
            this.addUsersToGroupCancelButton.UseVisualStyleBackColor = true;
            this.addUsersToGroupCancelButton.Click += new System.EventHandler(this.addUsersToGroupCancelButton_Click);
            // 
            // treeViewImageList
            // 
            this.treeViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeViewImageList.ImageStream")));
            this.treeViewImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.treeViewImageList.Images.SetKeyName(0, "groupIcon.png");
            this.treeViewImageList.Images.SetKeyName(1, "userIcon.png");
            this.treeViewImageList.Images.SetKeyName(2, "lock.png");
            // 
            // AddUserToGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 237);
            this.Controls.Add(this.addUsersToGroupCancelButton);
            this.Controls.Add(this.addUsersToGroupAddButton);
            this.Controls.Add(this.usersGroupBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddUserToGroupForm";
            this.Text = "AddGroupForm";
            this.usersGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox usersGroupBox;
        private System.Windows.Forms.Button addUsersToGroupAddButton;
        private System.Windows.Forms.Button addUsersToGroupCancelButton;
        private System.Windows.Forms.ImageList treeViewImageList;
        private System.Windows.Forms.ListBox addUsersToGroupListBox;
    }
}