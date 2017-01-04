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
    public partial class UpdateForm : Form
    {
        #region Variables
        private string user = "";
        private string extension = "";
        private string id = ""; 
        private string pass = "";
        private DbManager dbMan;// = new DbManager();
        #endregion

        #region Properties
        public string User
        {
            get { return user ;}
        }

        public string Extension
        {
            get { return extension;}
        }

        public string DisplayID
        {
            get { return id;}
        }

        public string Password
        {
            get { return pass;}
        }
        #endregion

        #region Constructor
        public UpdateForm(string user, string extension, string id, string pass, DbManager dBManager)
        {
            InitializeComponent();

            this.user = user;
            this.extension = extension;
            this.id = id;
            this.pass = pass;
            this.dbMan = dBManager;
        }
        #endregion

        #region Callbacks
        private void updateForm_Shown(object sender, EventArgs e)
        {
            this.userLabel.Text = this.user;
            this.extensionTextBox.Text = this.extension;
            this.displayidTextBox.Text = this.id;
            this.passTextBox.Text = this.pass;
        }
        #endregion

        #region Button Handlers
        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (this.extensionTextBox.Text == "" || this.displayidTextBox.Text == "" || this.passTextBox.Text == "")
            {
                goto Finish;
            }

            if (dbMan.ExistExtension(this.extensionTextBox.Text) && this.extensionTextBox.Text != this.extension)
            {
                MessageBox.Show("Esta extension ya existe.\nFavor inserte otra.");
                goto GoOut;
            }

            this.extension = this.extensionTextBox.Text;
            this.id = this.displayidTextBox.Text;
            this.pass = this.passTextBox.Text;


            if (dbMan.Update(this.extension, this.pass, this.id, this.user) != 0)
            {
                //if (this.extensionTextBox.Text != this.Extension)
                    this.DialogResult = DialogResult.OK;
                //else
                //    this.DialogResult = DialogResult.Ignore;

                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el usuario. \nIntente otros datos.");
                goto GoOut;
            }
        Finish:
            if (this.extensionTextBox.Text == "")
                MessageBox.Show("Favor inserte una extension.");
            else if (this.displayidTextBox.Text == "")
                MessageBox.Show("Favor inserte una id a mostrar.");
            else if (this.passTextBox.Text == "")
                MessageBox.Show("Favor inserte un password.");
        GoOut:
            return;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        #endregion
    }
}
