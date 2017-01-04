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
    public partial class AddUserToGroupForm : Form
    {
        private string groupName;
        private string groupOU;
        private string UserOU;
        private string domain;
        private List<string> users = new List<string>();
        
        public List<string> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }

        public string GroupName
        {
            get { return this.groupName; }
            set { this.groupName = value; }
        }

        public AddUserToGroupForm(List<string> Users, string GroupName, string GroupOU, string UsersOU, string domain)
        {
            InitializeComponent();

            this.GroupName = GroupName;
            this.groupOU = GroupOU.Substring(GroupOU.IndexOf("LDAP://") + 7);
            this.UserOU = UsersOU.Substring(UsersOU.IndexOf("LDAP://") + 7);
            this.domain = domain;
            this.Text = GroupName;
            addUsersToListBox(Users);
        }

        private void addUsersToListBox(List<string> Users)
        {
            foreach (string user in Users)
            {
                addUsersToGroupListBox.Items.Add(user);
            }
        }

        private void addUsersToGroupAddButton_Click(object sender, EventArgs e)
        {
            List<string> usersAlreadyInGroup = new List<string>();

            foreach (object SelectedItem in addUsersToGroupListBox.SelectedItems)
            {
                try
                {
                    DomainClassHelper.AddUserToGroup((string)SelectedItem, UserOU, domain, groupName, groupOU);
                    this.Users.Add((string)SelectedItem);
                }
                catch (Exception ex)
                {
                    usersAlreadyInGroup.Add((string)SelectedItem);
                }
            }

            if (usersAlreadyInGroup.Count > 0)
            {
                string users = "";
                foreach(string g in usersAlreadyInGroup)
                    users += g + "\n";
                MessageBox.Show(users);
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void addUsersToGroupCancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
