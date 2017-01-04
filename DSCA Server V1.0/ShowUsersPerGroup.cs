using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PruebaDLL
{
    public partial class ShowUsersPerGroup : Form
    {
        private string groupName;
        private string groupOU;
        private string domain;
        private string usersOU;
        private List<string> users = new List<string>();
        
        public List<string> usersData
        {
            get { return users; }
            set { users = value; }
        }

        public string GroupName
        {
            get { return groupName; }
        }

        public ShowUsersPerGroup(string groupName, string GroupOU, string domain, string UsersOU)
        {
            InitializeComponent();
            this.groupName = groupName;
            this.domain = domain;            
            this.groupOU = GroupOU.Substring(GroupOU.IndexOf("LDAP://") + 7);
            this.usersOU = UsersOU.Substring(UsersOU.IndexOf("LDAP://") + 7);
            this.Text = groupName;
        }

        private void ShowUsersPerGroup_OnShown(object sender, EventArgs e)
        {
            List<string> users = new List<string>();
            users = DomainClassHelper.GetUsersByGroup(groupName, domain, "LDAP://" + groupOU);

            foreach (string user in users)
            {
                usersPerGroupTreeView.Nodes.Add(new TreeNode(user, 1, 1));
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void ShowUsersPerGroup_MouseUp(object sender, MouseEventArgs e)
        {
            Point clickPoint = new Point(e.X, e.Y);

            TreeNode selectedNode = (usersPerGroupTreeView.SelectedNode = 
                usersPerGroupTreeView.GetNodeAt(clickPoint));
                
            if ((selectedNode) == null)
            {
                this.removeButton.Enabled = false;
                this.usersPerGroupTreeView.SelectedNode = null;
            }

            else
            {
                this.removeButton.Enabled = true;
            }
            
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que quiere eliminar al usuario del grupo?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
            {
                string userToRemove = this.usersPerGroupTreeView.SelectedNode.Text;
                try
                {
                    DomainClassHelper.RemoveUserFromGroup(userToRemove, this.usersOU, this.domain, this.groupName, this.groupOU);
                    this.usersPerGroupTreeView.Nodes.Remove(this.usersPerGroupTreeView.SelectedNode);
                    this.usersPerGroupTreeView.Refresh();
                    usersData.Add(userToRemove);
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    this.usersPerGroupTreeView.SelectedNode = null;
                    this.removeButton.Enabled = false;
                }
            }
        }

    }
}
