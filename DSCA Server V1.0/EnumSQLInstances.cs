using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;

namespace PruebaDLL
{
    public partial class EnumSQLInstances : Form
    {
        #region Variables
        private SqlDataSourceEnumerator enumDB;
        private DataTable table = null;
        private int lateSelect = 0;
        private string connectionString = "";
        private string createTableString = "create table Usuarios" +
                                    "(" +
	                                    "ADUser varchar (20) primary key," + 
	                                    "FullName varchar(20) not null, " +
	                                    "userId varchar(20), " +
	                                    "pass varchar (20), " +
	                                    "displayId varchar (20)" + 
                                    ")" ;
        private string connString = "";
        #endregion
        #region Properties
        public string ConnString
        {
            get { return connString; }
        }
        #endregion
        #region Constructor
        public EnumSQLInstances()
        {
            InitializeComponent();
        }
        #endregion
        #region CallBacks
        private void Form_OnLoad(object sender, EventArgs e)
        {
            enumDB = SqlDataSourceEnumerator.Instance;
            table = enumDB.GetDataSources();
            foreach (DataRow row in table.Rows)
            {
                if (row["InstanceName"].ToString().Equals("SQLEXPRESS") ||
                    row["InstanceName"].ToString().Equals("MSSQLSERVER") &&
                     row["InstanceName"].ToString() != null)
                {
                    instancesComboBox.Items.Add((String)row["ServerName"] + "\\" + (String)row["InstanceName"]);
                }
            }
            instancesComboBox.Items.Add("(local)");
            instancesComboBox.SelectedIndex = 0;
            lateSelect = instancesComboBox.SelectedIndex;
        }

        private void SelectedIndexChanged_OnChange(object sender, EventArgs e)
        {
            serversComboBox.Items.Clear();
            string server = (String)instancesComboBox.Items[instancesComboBox.SelectedIndex];

            connectionString = "Data Source =" + server + ";Integrated Security=True";//Initial Catalog=master";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("select name from sys.databases;", con);
                    SqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        serversComboBox.Items.Add(dr[0].ToString());
                    }

                    con.Close();
                    serversComboBox.SelectedIndex = 0;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Could Not connect to Database Instance");
                    instancesComboBox.SelectedIndex = lateSelect;
                }
                    
            }
        }

        private void CheckedChanged_OnCheck(object sender, EventArgs e)
        {
            if (newDBCheckBox.Checked)
            {
                serversComboBox.Enabled = false;
                nameDBTextBox.Enabled = true;
                createDBButton.Enabled = true;
            }
            else
            {
                serversComboBox.Enabled = true;
                nameDBTextBox.Enabled = false;
                createDBButton.Enabled = false;
            }
        }

        private void createDBButton_Click(object sender, EventArgs e)
        {
            if (nameDBTextBox.Text != "")
            {
                if (!Directory.Exists("C:\\Program Files\\DSCA Solutions"))
                {
                    Directory.CreateDirectory("C:\\Program Files\\DSCA Solutions");
                }

                string conString = "Data Source =" + (String)instancesComboBox.Items[instancesComboBox.SelectedIndex] 
                                + ";Integrated Security=True";
                string sqlCreateDBQuery = "CREATE DATABASE " + nameDBTextBox.Text
                                        + " ON PRIMARY "
                                        + "(NAME = " + nameDBTextBox.Text + "_Data, "
                                        + "FILENAME = 'C:\\Program Files\\DSCA Solutions\\" + nameDBTextBox.Text + "Data.mdf', "
                                        + "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " + "LOG ON (NAME = " + nameDBTextBox.Text + "_Log, "
                                        + "FILENAME = 'C:\\Program Files\\DSCA Solutions\\" + nameDBTextBox.Text + "Data.ldf', "
                                        + "SIZE = 1MB, MAXSIZE = 5MB, FILEGROWTH = 10%)";
                try
                {
                    SqlConnection myConn = new SqlConnection(conString);
                    SqlCommand myCommand = new SqlCommand(sqlCreateDBQuery, myConn);
                    try
                    {
                        myConn.Open();
                        myCommand.ExecuteNonQuery();
                        myConn.Close();
                        
                        conString += ";Database=" + nameDBTextBox.Text;
                        connString = conString;

                        CreateTable(conString);
                        
                        MessageBox.Show("Se ha creado la base de datos exitosamente!", "DSCA Solutions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "DSCA Solutions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                }
            }
        }

        private void CreateTable(string Conn)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Conn);
                SqlCommand myCommand = new SqlCommand(createTableString, conn);

                if (!ExistsTable(Conn))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    myCommand.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch
            {
            }
        }

        private bool ExistsTable(string Conn)
        {
            SqlConnection conn = new SqlConnection(Conn);
            using (conn)
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string toCheck = "SELECT 1 as IsExists FROM dbo.sysobjects where id = object_id('[dbo].[" + "Usuarios" +
                    "]')";
                SqlCommand cmd = new SqlCommand(toCheck, conn);

                try
                {
                    int check = (Int32)cmd.ExecuteScalar();
                    if (check > 0)
                    {
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        conn.Close();
                        return false;
                    }
                }
                catch
                {
                    conn.Close();
                    return false;
                }
            }
        }

        private void OkButton_OnClick(object sender, EventArgs e)
        {
            connString = "Data Source =" + (String)instancesComboBox.Items[instancesComboBox.SelectedIndex] 
                                + ";Integrated Security=True;Database=" + (String)serversComboBox.Items[serversComboBox.SelectedIndex];
            CreateTable(connString);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void CloseButton_OnClick(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion        
    }
}
