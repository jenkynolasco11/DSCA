using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices;

namespace PruebaDLL
{
    public partial class DomainOUForm : Form
    {
        #region Variables
        private DirectoryEntry entry;
        private string ldapGroupPath = "";
        private string ldapUsersPath = "";
        #endregion
        #region Properties
        public string GroupPath
        {
            get { return ldapGroupPath; }
        }

        public string UsersPath
        {
            get { return ldapUsersPath; }
        }
        #endregion
        #region Constructor
        public DomainOUForm(string groupPath)
        {
            InitializeComponent();

            if(groupPath != "")
                ldapGroupPath = groupPath.Substring(7);

            this.groupFolderTextBox.Text = ldapGroupPath;
        }
        #endregion
        #region Callbacks
        private void DomainOUForm_OnLoad(object sender, EventArgs e)
        {
            try
            {
                entry = new DirectoryEntry();
                string friendlyName = entry.Name.Substring(3);
                string gPath = "LDAP://";
                try
                {
                    DirectoryContext context = new DirectoryContext(DirectoryContextType.Domain, friendlyName);
                    Domain objDomain = Domain.GetDomain(context);
                    domainLabel.Text = objDomain.Name;
                }
                catch { }
            }
            catch { }
        }
        #endregion

        private void OKButton_OnClick(object sender, EventArgs e)
        {
            if (groupFolderTextBox.Text != "")
            {
                char[] x = new char[] { '.' };
                ldapGroupPath = "LDAP://" + groupFolderTextBox.Text;
                ldapUsersPath += "LDAP://CN=Users,DC=" + domainLabel.Text.Split(x)[0] + ",DC="
                    + domainLabel.Text.Split(x)[1];
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Por favor, inserte un nombre valido");
            //Poner aqui buscar si el OU existe
        }

        private void CancelButton_OnClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
