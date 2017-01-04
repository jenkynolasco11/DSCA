namespace PruebaDLL
{
    partial class ShowUsersPerGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowUsersPerGroup));
            this.usersPerGroupTreeView = new System.Windows.Forms.TreeView();
            this.treeViewImageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // usersPerGroupTreeView
            // 
            this.usersPerGroupTreeView.ImageIndex = 1;
            this.usersPerGroupTreeView.ImageList = this.treeViewImageList;
            this.usersPerGroupTreeView.Location = new System.Drawing.Point(6, 19);
            this.usersPerGroupTreeView.Name = "usersPerGroupTreeView";
            this.usersPerGroupTreeView.SelectedImageIndex = 1;
            this.usersPerGroupTreeView.ShowRootLines = false;
            this.usersPerGroupTreeView.Size = new System.Drawing.Size(256, 153);
            this.usersPerGroupTreeView.TabIndex = 0;
            this.usersPerGroupTreeView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ShowUsersPerGroup_MouseUp);
            // 
            // treeViewImageList
            // 
            this.treeViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeViewImageList.ImageStream")));
            this.treeViewImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.treeViewImageList.Images.SetKeyName(0, "groupIcon.png");
            this.treeViewImageList.Images.SetKeyName(1, "userIcon.png");
            this.treeViewImageList.Images.SetKeyName(2, "lock.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.usersPerGroupTreeView);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 178);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Usuarios";
            // 
            // removeButton
            // 
            this.removeButton.Enabled = false;
            this.removeButton.Location = new System.Drawing.Point(18, 204);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 2;
            this.removeButton.Text = "Remover";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.exitButton.Location = new System.Drawing.Point(199, 204);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 3;
            this.exitButton.Text = "Salir";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ShowUsersPerGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 239);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowUsersPerGroup";
            this.Text = "ShowUsersPerGroup";
            this.Shown += new System.EventHandler(this.ShowUsersPerGroup_OnShown);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView usersPerGroupTreeView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.ImageList treeViewImageList;
    }
}