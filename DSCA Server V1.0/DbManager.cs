using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PruebaDLL
{
    public partial class DbManager
    {
        #region Variables
        private SqlConnection conn = null;
        private SqlCommand cmd = null;
        private SqlDataReader reader = null;
        private SqlDataAdapter adapter = null;
        private bool Value = false;
        private string ConnectionString = "";// "Data Source=(local); " 
            //+ "Integrated Security=True; " + "database=mydb";

        private string getAllUserTables = "select * from Usuarios";
        private string sql = null;
        private DataTable dataTable = null;
        private DataColumn dataColumn = new DataColumn();
        #endregion

        #region Constructor
        public DbManager(string connectionString)
        {
            this.ConnectionString = connectionString;
            conn = new SqlConnection(ConnectionString);
            OpenConnection();
        }      
        #endregion

        #region Functions

        private void OpenConnection()
        {
            try
            {
//                while (Value)
//                {
//                }
                conn.Open();

                Value = true;
            }
            catch (Exception ex)
            {
            }
        }

        private void CloseConnection()
        {
            try
            {
                conn.Close();
//                Value = false;
            }
            catch(Exception ex)
            {
            }
        }

        public bool ExistUser(string user, DataTable table)
        {
            OpenConnection();

            if (ReturnByUser(user, table) != null)
            {
                CloseConnection();
                return true;
            }

            CloseConnection();
            return false;
        }

        public bool ExistExtension(string extension)
        {
            OpenConnection();

            if (ReturnByExtension(extension, dataTable) != null)
            {
                CloseConnection();
                return true;
            }

            CloseConnection();
            return false;
        }

        public bool ExistDisplayId(string displayId)
        {
            OpenConnection();

            if (ReturnByDisplayId(displayId, dataTable) != null)
            {
                CloseConnection();
                return true;
            }

            CloseConnection();
            return false;
        }

        public DataRow ReturnByUser(string user, DataTable table)
        {
            OpenConnection();
            if(table == null)
                table = GetDataTable();
            try
            {
                foreach (DataRow dr in table.Rows)
                {
                    if (dr["ADUser"].Equals(user))
                    {
                        CloseConnection();
                        return dr;
                    }
                }
            }

            catch (SqlException ae)
            {
                Console.WriteLine(ae.Message.ToString());
            }

            CloseConnection();
            return null;
        }

        public DataRow ReturnByFullName(string user, DataTable table)
        {
            OpenConnection();

            if (table == null)
                table = GetDataTable();

            try
            {
                foreach (DataRow dr in table.Rows)
                {
                    if (dr["FullName"].Equals(user))
                    {
                        CloseConnection();
                        return dr;
                    }
                }
            }

            catch (SqlException ae)
            {
                Console.WriteLine(ae.Message.ToString());
            }
            CloseConnection();
            return null;
        }

        public DataRow ReturnByExtension(string extension, DataTable table)
        {
            OpenConnection();

            if (table == null)
                table = GetDataTable();

            try
            {
                foreach (DataRow dr in table.Rows)
                {
                    if (dr["userId"].Equals(extension))
                    {
                        CloseConnection();
                        return dr;
                    }
                }
            }

            catch (SqlException ae)
            {
                Console.WriteLine(ae.Message.ToString());
            }
            CloseConnection();
            return null;
        }

        public DataRow ReturnByDisplayId(string displayId, DataTable table)
        {
            OpenConnection();

            if (table == null)
                table = GetDataTable();

            try
            {
                foreach (DataRow dr in table.Rows)
                {
                    if (dr["displayId"].Equals(displayId))
                    {
                        CloseConnection();
                        return dr;
                    }
                }
            }

            catch (SqlException ae)
            {
                Console.WriteLine(ae.Message.ToString());
            }
            CloseConnection();
            return null;
        }

        public DataTable GetDataTable()
        {
            try
            {
                OpenConnection();
                cmd = new SqlCommand(getAllUserTables,conn);

                using (var da = new SqlDataAdapter(cmd))
                {
                    var table = new DataTable();
                    dataTable = table;
                    da.Fill(dataTable);
                    da.Dispose();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
            CloseConnection();
            return dataTable;
        }

        //Arreglar esta parte agregando DataTable
        #region Database Sentences
        public int Insert(string userId, string pass, string displayid, string ADUser, string fullname)
        {
            OpenConnection();
            string sql = @"INSERT INTO Usuarios (ADUser,userId,pass,displayid,FullName)
                             VALUES(@ADUser,@userId,@pass,@displayid,@FullName)";

            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("ADUser", ADUser);
            command.Parameters.AddWithValue("userId", userId);
            command.Parameters.AddWithValue("pass", pass);
            command.Parameters.AddWithValue("displayid", displayid);
            command.Parameters.AddWithValue("FullName", fullname);

            int filaffect = command.ExecuteNonQuery();

            CloseConnection();
            
            if (filaffect > 0)
                return filaffect;
            else return 0;
        }

        public int Update(string userId, string pass, string displayid, string ADUser)
        {
            OpenConnection();
            string sql = @"UPDATE Usuarios SET 
                                   [userId] = @userId
                                  ,[pass] = @pass
                                  ,[displayId] = @displayid
                             WHERE [ADUser] = @ADUser";

            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("ADUser", ADUser);
            command.Parameters.AddWithValue("userId", userId);
            command.Parameters.AddWithValue("pass", pass);
            command.Parameters.AddWithValue("displayid", displayid);

            int filaffect = command.ExecuteNonQuery();
            
            CloseConnection();

            if (filaffect > 0)
            {
                return filaffect;
            }

            else return 0;
        }

        public int Delete(string ADUser, string pass, string displayid)
        {
            OpenConnection();

            string sql = @"DELETE FROM Usuarios
                            WHERE [ADUser] = @ADUser 
                            AND [displayId] = @displayid
                            AND [pass] = @pass";

            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("ADUser", ADUser);
            command.Parameters.AddWithValue("pass", pass);
            command.Parameters.AddWithValue("displayid", displayid);

            int filaffect = command.ExecuteNonQuery();

            CloseConnection();

            if (filaffect > 0)
            {
                return filaffect;
            }

            else return 0;
        }
        #endregion
        #endregion
    }
}
