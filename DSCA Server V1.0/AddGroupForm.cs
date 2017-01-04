using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.DirectoryServices.AccountManagement;

namespace PruebaDLL
{
    public partial class AddGroupForm : Form
    {
        private string groupOU;
        private bool groupType;
        private string domain;
        private GroupScope groupScope;
        private string groupName = "";

        public string GroupName
        {
            get { return this.groupName; }
            set { this.groupName = value; }
        }

        public AddGroupForm(string groupOU, string domain)
        {
            InitializeComponent();
            this.groupOU = groupOU;
            this.domain = domain;
            AddEventsToRadioButtons();
            globalRadioButton.Checked = true;
            securityRadioButton.Checked = true;
            OKButton.Enabled = false;
            this.groupOULabel.Text = groupOU;
        }

        private void AddEventsToRadioButtons()
        {
            domainLocalRadioButton.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
            securityRadioButton.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
            universalRadioButton.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
            distRadioButton.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
            globalRadioButton.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                if (((GroupBox)((RadioButton)sender).Parent).Name == "groupScopeGroupBox")
                {
                    switch (((RadioButton)sender).TabIndex)
                    {
                        case 0:
                            this.groupScope = GroupScope.Local;
                            break;
                        case 1:
                            this.groupScope = GroupScope.Global;
                            break;
                        case 2:
                            this.groupScope = GroupScope.Universal;
                            break;
                    }
                }

                if (((GroupBox)((RadioButton)sender).Parent).Name == "groupTypeGroupBox")
                {
                    switch (((RadioButton)sender).TabIndex)
                    {
                        case 0:
                            this.groupType = true;
                            break;
                        case 1:
                            this.groupType = false;
                            break;
                    }
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (groupNameTextBox.Text != "")
            {
                if (groupDescriptionTextBox.Text != "")
                {
                    this.GroupName = groupNameTextBox.Text;

                    try
                    {
                        DomainClassHelper.CreateGroup(
                            this.groupOU,
                            this.groupNameTextBox.Text,
                            this.groupScope,
                            this.groupType,
                            this.domain,
                            this.groupDescriptionTextBox.Text);
                    }
                    catch (Exception ex)
                    {
                    }

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Por favor, insertar una descripcion en Descripcion de grupo");
                }
            }
            else
            {
                MessageBox.Show("Por favor, insertar un nombre en Nombre de grupo");
            }
        }

        private void GroupName_TextChanged(object sender, EventArgs e)
        {
            if (groupNameTextBox.Text != "")
            {
                OKButton.Enabled = true;
            }

            else
            {
                OKButton.Enabled = false;
            }
        }
    }
}
