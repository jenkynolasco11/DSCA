using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworksApi.TCP.SERVER;
using NetworksApi.TCP;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using TextFile;
using System.Diagnostics;

namespace PruebaDLL
{
    public partial class DSCAServerForm : Form
    {
        #region Variables
        private Server server = null;
        private string interfaceIpAddress = "";
        private string listeningPort = "";
        private string serverport = "";
        private string serverip = "";
        private string sipip = "";
        private string sipport = "";
        private string connString = "";
        private string keysToFind = "";
        private List<object> interfaceList = new List<object>();
        private DbManager dbManager = null;

        //this one
        //private string usersPath = "LDAP://CN=Users,DC=dsca,DC=com";
        private string usersPath = "";
        //this one
        //private string groupsPath = "LDAP://OU=Groups,DC=dsca,DC=com";
        private string groupsPath = "";
        private string domain;

        private Thread receiveThread;
        private Thread reconnect;
        private Thread usersTV;
        private Thread groupTV;
        private Thread actualizeUsers;
        private Thread actualizeDaemon;
        private Thread checkDB;
        private Thread logsThread;
        //private Thread usersListViewThread;
        private System.Timers.Timer timer = new System.Timers.Timer();
        //private ThreadPool WorkerThreads;
        private static DaemonInterpreter daemon;
        private static DataTable dataTable;
        private static List<string> Groups = new List<string>();
        private static List<string> Users = new List<string>();
        private static List<string> ClientConnected = new List<string>();
        private bool threadControl = false;
        private bool quitThread = false;
        
        #region Delegates
        private delegate void AddUserNode(TreeNode node);
        private delegate void AddGroupNode(TreeNode node);
        private delegate void ClearGroupNode();
        private delegate void ClearUserNode();
        private delegate void UpdTable();
        private delegate void WriteTextBox(string Name, string IP, string Stmt);
        private delegate void AddUsersGroupNodes(TreeNode user, List<TreeNode> groups);
        private delegate void KeyboardChar(string key);
        private delegate void ChargeLogs(FileInfo[] logNames);
        private delegate void ChangeUserState(int index, string UserName);
        private delegate void Auxiliar();
        private delegate void Auxiliar_Obj(object obj);
        #endregion
        #endregion

        #region Properties
        public string ConnectionString
        {
            get { return Properties.Settings.Default.mydbConnectionString; }
            set
            {
                connString = value;
                Properties.Settings.Default.mydbConnectionString = value;
            }
        }

        public string ServerPort 
        {
            get { return /*this.serverport;*/ Properties.Settings.Default.ServerPort; }
            set 
            { 
                this.serverport = value;
                Properties.Settings.Default.ServerPort = value;
                Properties.Settings.Default.Save();
            } 
        }

        public string SipIP 
        {
            get { return /*this.sipip; */ Properties.Settings.Default.SIPIp; }
            set 
            {
                this.sipip = value;
                Properties.Settings.Default.SIPIp = value;
                Properties.Settings.Default.Save();
            } 
        }

        public string SipPort 
        {
            get { return /*this.sipport;*/ Properties.Settings.Default.SIPPort; }
            set 
            { 
                this.sipport = value;
                Properties.Settings.Default.SIPPort = value;
                Properties.Settings.Default.Save();
            } 
        }

        public string ServerIP 
        {
            get { return /*this.serverip;*/Properties.Settings.Default.ServerIp; }
            set 
            { 
                this.serverip = value;
                Properties.Settings.Default.ServerIp = value;
                Properties.Settings.Default.Save();
            } 
        }

        public DataTable Table
        {
            get { return dataTable; }
            set
            {
                dataTable = value;
                if (this.InvokeRequired)
                    this.BeginInvoke(new UpdTable(UpdateTable));
                else
                    this.usersDataGridView.DataSource = dataTable;
                PoblateTable();
            }
        }

        public string GroupPath
        {
            get { return /*groupsPath;*/Properties.Settings.Default.MyGroupPath; }
            set 
            { 
                groupsPath = value;
                Properties.Settings.Default.MyGroupPath = value;
                Properties.Settings.Default.Save();
            }
        }

        public string UsersPath
        {
            get { return Properties.Settings.Default.myUsersPath; }
            set
            {
                this.usersPath = value;
                Properties.Settings.Default.myUsersPath = value;
                Properties.Settings.Default.Save();

                string domValue = usersPath.Substring(usersPath.IndexOf("DC="));
                string [] dom = domValue.Split(new string [] {"DC=",","}, StringSplitOptions.None);
                string domainName = "";
                foreach (string v in dom)
                {
                    if (v != "")
                    {
                        domainName += v;
                        if (v == dom[dom.Length - 1])
                            continue;
                        domainName += ".";
                    }
                }

                Domain = domainName;
            }
        }
        
        public string Domain
        {
            get { return Properties.Settings.Default.myDomain; }
            set 
            { 
                this.domain = value;
                Properties.Settings.Default.myDomain = value;
                Properties.Settings.Default.Save();
            }
        }
        #endregion

        #region Constructor
        public DSCAServerForm()
        {
            //this one
            if (!Properties.Settings.Default.InitialConfiguration)
            {
                NetworkConfig_OnClick(null, EventArgs.Empty);
                ConfigDataBase_OnClick(null, EventArgs.Empty);
                DomainConfiguration_OnClick(null, EventArgs.Empty);

                Properties.Settings.Default.InitialConfiguration = true;
                Properties.Settings.Default.Save();
            }
            //UsersPath = "LDAP://CN=Users,DC=dsca,DC=com";
            try
            {
                dbManager = new DbManager(ConnectionString);
            }
            catch (Exception e)
            {
                MessageBox.Show("No se ha podido conectar a la base de datos.\nSe cerrara la aplicacion.");
                FormClosed_StopServer(this, null);
            }
            InitializeComponent();
            //ReturnInterfaces();

            reconnect = new Thread(new ThreadStart(Reconnect));
            actualizeUsers = new Thread(new ThreadStart(ActualizeUsers));
            reconnect.IsBackground = true;
            actualizeUsers.IsBackground = true;

            //Aqui dura muchisimo!!!
            GetUsersFromDomain();
            reconnect.Start();
            actualizeUsers.Start();

            
            InitializeServer();

            

            //DaemonThread();
        }
        #endregion

        #region Functions
        private void InitializeSystemWatcher()
        {
            try
            {
                logsFileSystemWatcher.Path = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\DSCA Solutions\\LOGS";
                logsFileSystemWatcher.Filter = "*.txt";
                logsFileSystemWatcher.IncludeSubdirectories = false;
                logsFileSystemWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.CreationTime | NotifyFilters.Size;
                logsFileSystemWatcher.Changed += new FileSystemEventHandler(logsFileSystemWatcher_WriteLog);
                logsFileSystemWatcher.Created += new FileSystemEventHandler(logsFileSystemWatcher_WriteLog);
                logsFileSystemWatcher.EnableRaisingEvents = true;
            }
            catch { }
        }

        private void UpdateTable()
        {
            try
            {
                this.usersDataGridView.DataSource = Table;
            }
            catch
            {
                throw new Exception();
            }
        }
        #region Thread Functions
        //Lo mismo que ActualizeDaemon
        private void DaemonThread()
        {
            threadControl = false;
            actualizeDaemon = new Thread(new ThreadStart(ActualizeDaemonDB));
            actualizeDaemon.IsBackground = true;
            actualizeDaemon.Priority = ThreadPriority.AboveNormal;
            actualizeDaemon.Start();
        }
        //Lo mismo que DaemonThread
        private void ActualizeDaemon()
        {
            new Thread(new ThreadStart(ActualizeDaemonDB)).Start();
        }

        private void GetDataTableThread()
        {
            checkDB = new Thread(new ThreadStart(GetDataTable));
            checkDB.IsBackground = true;
            checkDB.Start();
        }

        private void UsersTreeViewThread()
        {
            usersTV = new Thread(new ParameterizedThreadStart(PoblateUsersTree));
            usersTV.IsBackground = true;
            usersTV.Start(Users);
        }

        private void GroupsTreeViewThread()
        {
            groupTV = new Thread(new ThreadStart(PoblateGroupsTree));
            groupTV.IsBackground = true;
            groupTV.Start();
        }

        private void ActualizeUsers()
        { 
            while(true)
            {
                new Thread(new ThreadStart(UpdateUsersCatalog)).Start();
                Thread.Sleep(15000);
                if (quitThread)
                    break;
            }
        }
                
        #endregion
        private void CallDelegates()
        {
            server.OnClientConnected += new OnConnectedDelegate(server_OnClientConnected);
            server.OnServerError += new OnErrorDelegate(server_ErrorArguments);
            server.OnClientDisconnected += new OnDisconnectedDelegate(server_OnClientDisconnected);
            server.OnDataReceived += new OnReceivedDelegate(server_OnReceivedData);
        }

        private void PoblateTable()
        {
            try
            {
                usersDataGridView.Columns["userId"].HeaderText = "Extension";
                usersDataGridView.Columns["pass"].HeaderText = "Password";
                usersDataGridView.Columns["pass"].Visible = false;
                usersDataGridView.Columns["displayId"].HeaderText = "Nombre Usuario";
                usersDataGridView.Columns["ADUser"].HeaderText = "Cuenta Usuario";
                usersDataGridView.Columns["FullName"].HeaderText = "Nombre de Inicio de Sesion";
            }
            catch
            {
            }
        }

        private void GetDataTable()
        {
            Table = dbManager.GetDataTable();
        }

        private void InitializeServer()
        {
            try
            {
                server = new Server(ServerIP, ServerPort);
                server.Start();
                CallDelegates();
            }
            catch (NullReferenceException NRE) { }
        }

        //private void ReturnInterfaces()
        //{
        //    NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

        //    foreach (NetworkInterface netInterface in interfaces)
        //    {
        //        if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet || netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
        //            || netInterface.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet || netInterface.NetworkInterfaceType == NetworkInterfaceType.Loopback)
        //        {
        //            comboBox1.Items.Add(netInterface.Name);
        //            interfaceList.Add(netInterface);
        //        }
        //    }
        //    comboBox1.SelectedIndex = 0;
        //}

        private void GetObjectsFromDomain()
        {
            Users = DomainClassHelper.GetUsersFromDomain(UsersPath, Domain);
            Groups = DomainClassHelper.GetGroupsFromDomain(Domain, GroupPath);
        }

        private void GetUsersFromDomain()
        {
            GetObjectsFromDomain();
            InsertToDB(Users);
        }
        //------Segundo-----------------
        private void CheckInDatabase(object Users)
        {
            List<string> users = (List<string>)Users;

            do
            {
                try
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (!users.Contains(row["ADUser"]))
                        {
                            dbManager.Delete((String)row["ADUser"],
                                             (String)row["pass"],
                                             (String)row["displayId"]);
                        }
                    }
                }catch(Exception ex)
                {}
            } while (Table == null);
        }
        //------------------------------
        private void AddGroupsToUserNode(object User, object Node)
        {
            string user = (String)User;
            TreeNode node = (TreeNode)Node;
            // groups = DomainClassHelper.GetUsersByGroup("Call Center", domain, groupsPath);
            List<string> groups = DomainClassHelper.GetGroupsByUser((String)dbManager.ReturnByUser(user, dataTable)["FullName"], Domain, UsersPath);
            List<TreeNode> nodes = new List<TreeNode>();

            if (groups != null)
            {
                foreach (string group in groups)
                {
                    if (Groups.Contains(group))
                    {

                        nodes.Add(new TreeNode(group, 0, 0));
                        //java.SendData(new List<string> { "modifygroup", group, user });
                        //Thread.Sleep(150);
                    }
                    else
                    {
                        nodes.Add(new TreeNode(group, 2, 2));
                    }
                }
            }

            if (this.InvokeRequired)
                this.Invoke(new AddUsersGroupNodes(AddGroupsToUserTree), new object[] { node, nodes } );//, nodes.ToArray()) });
            else
                AddGroupsToUserTree(node, nodes);

        }

        private void AddGroupsToUserTree(TreeNode user, List<TreeNode> groups)
        {
            try
            {
                user.Nodes.AddRange(groups.ToArray());
            }
            catch { }
        }

        private void ChangeIconUser(int index, string UserName)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new ChangeUserState(ChangeStateToUserInTree), new object[] { index, UserName });
            else
                ChangeStateToUserInTree(index, UserName);
        }

        private void ChangeStateToUserInTree(int imageindex, string username)
        {
            try
            {
                string FullName = (String)dbManager.ReturnByFullName(username, Table)["ADUser"];
                foreach (TreeNode node in usersTreeView.Nodes)
                {
                    if (node.Text == FullName)
                    {
                        node.ImageIndex = imageindex;
                        node.SelectedImageIndex = imageindex;
                        break;
                    }
                }
            }
            catch { }
        }

        private void PoblateUsersTree(object users)
        {
            try
            {
                
                if (this.InvokeRequired)
                    this.BeginInvoke(new ClearUserNode(ClearUsersNodes));
                else
                    usersTreeView.Nodes.Clear();
            }
            catch { }

            Thread.Sleep(200);

            List<string> Users = (List<string>)users;
            List<string> groups = new List<string>();
            //TreeNodeCollection groupNodes;
            List<TreeNode> nodes;
            TreeNode node;

            foreach (string user in Users)
            {
                if (!FindNode(user,usersTreeView.Nodes))
                {
                    nodes = new List<TreeNode>();
                    node = new TreeNode(user);
                    //groups = DomainClassHelper.GetUsersByGroup("Call Center", domain, groupsPath);
                    //groups = DomainClassHelper.GetGroupsByUser((String)dbManager.ReturnByUser(user, dataTable)["FullName"],Domain,UsersPath);
                    
                    //if (groups != null)
                    //{
                    //    foreach (string group in groups)
                    //    {
                    //        if (Groups.Contains(group))
                    //        {
                    //            nodes.Add(new TreeNode(group, 0, 0));
                    //            //java.SendData(new List<string> { "modifygroup", group, user });
                    //            //Thread.Sleep(150);
                    //        }
                    //        else
                    //        {
                    //            nodes.Add(new TreeNode(group, 2, 2));
                    //        }
                    //    }
                    //}
                    try
                    {
                        if (this.InvokeRequired)
                            this.Invoke(new AddUserNode(AddUsersNodes), new object[] { new TreeNode(user, 3, 3)});//, nodes.ToArray()) });
                        else
                            AddUsersNodes(new TreeNode(user, 3, 3));//, nodes.ToArray()));
                    }
                    catch
                    { }
                }
            }

            new Thread(new ThreadStart(CheckUserStats)).Start();
        }

        private void ClearUsersNodes()
        {
            try
            {
                usersTreeView.Nodes.Clear();
            }
            catch { }
        }

        private void ClearGroupsNodes()
        {
            try
            {
                groupsTreeView.Nodes.Clear();
            }
            catch { }
        }

        private void AddUsersNodes(TreeNode node)
        {
            try
            {
                usersTreeView.Nodes.Add(node);
                new Thread(() => { AddGroupsToUserNode(node.Text, node); }).Start();
                usersTreeView.Sort();
            }
            catch { }
        }

        private void AddGroupsNodes(TreeNode node)
        {
            try
            {
                groupsTreeView.Nodes.Add(node);
                groupsTreeView.Sort();
            }
            catch { }
        }

        private void PoblateGroupsTree()
        {
            try
            {
                if (this.InvokeRequired)
                    this.BeginInvoke(new ClearGroupNode(ClearGroupsNodes));
                else
                    groupsTreeView.Nodes.Clear();
            }
            catch { }

            List<string> groups = new List<string>();

            foreach (string group in Groups)
            {
                if (this.InvokeRequired)
                    this.Invoke(new AddGroupNode(AddGroupsNodes), new object[] { new TreeNode(group) });
                else
                    AddGroupsNodes(new TreeNode(group));
                //try
                //{
                //    daemon.SendData(new List<string> { "creategroup", group });
                //}
                //catch (Exception)
                //{
                //    //MessageBox.Show("El cliente esta desconectado del Servidor.\nPor favor comunique con el encargado de la red.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    //Reconnect();
                //    //daemon.SendData(new List<string> { "creategroup", group });
                //}
                //Thread.Sleep(150);
            }
            //try
            //{
            //    //groupsTreeView.Sort();
            //}
            //catch (Exception e)
            //{
            //}
        }
        #region Find Node
        private bool FindNode(string nameNode, TreeNodeCollection treeNodeCollection)
        {
            foreach (TreeNode tn in treeNodeCollection)
            {
                if (FindRecursive(nameNode, tn))
                    return true;
                if (tn.Text == nameNode)
                    return true;
            }
            return false;
        }

        private bool FindRecursive(string nameNode, TreeNode treeNode)
        {
            foreach (TreeNode tn in treeNode.Nodes)
            {
                if (tn.Text == nameNode)
                    return true;
                if (FindRecursive(nameNode, tn))
                    return true;
            }

            return false;
        }
        #endregion
        //-------Primero-------------------
        private void InsertToDB(object Users)
        {
            StopAllWorkingThreads();

            List<string> users = (List<string>)Users;

            foreach (string user in users)
            {
                if (!dbManager.ExistUser(user,dataTable))
                {
                    dbManager.Insert("", "", "", user, (String)DomainClassHelper.GetUserInfo(UsersPath, Domain, user)[1]);
                }
            }

            
            GetDataTableThread();
            UsersTreeViewThread();
            //UsersListViewThread();
            LogsListViewThread();
            GroupsTreeViewThread();

        }

        private void LogsListViewThread()
        {
            logsThread = new Thread(new ThreadStart(GetLogsFromDir));
            logsThread.IsBackground = true;
            logsThread.Start();
        }

        //private void UsersListViewThread()
        //{
        //    usersListViewThread = new Thread(new ThreadStart(GetOnOffUsersToListView));
        //    usersListViewThread.IsBackground = true;
        //    usersListViewThread.Start();
        //}

        //---------------------------------
        //-------Segundo-------------------
        private void CheckUsersInDB(object Datarows)
        {
            DataGridViewRowCollection datarows = (DataGridViewRowCollection)Datarows;
            foreach (DataRow row in datarows)
            {
                if (!Users.Contains((String)row["ADUser"]))
                {
                    dbManager.Delete((String)row["ADUser"],
                                        (String)row["pass"],
                                        (String)row["displayId"]);
                }
            }
        }
        //---------------------------------
        private void RefreshTable()
        {
            new Thread(new ParameterizedThreadStart(CheckUsersInDB)).Start(usersDataGridView.Rows);
        }

        private void CheckUserStats()
        {
            try
            {
                foreach (string clientconnected in ClientConnected)
                {
                    ChangeIconUser(4, clientconnected);
                }
            }
            catch { }
        }

        private void Reconnect_Daemon(object sender, EventArgs e)
        {
            DaemonInteraction();
        }

        private void DaemonInteraction()
        {
            try
            {
                if (daemon != null)
                    daemon = null;

                daemon = new DaemonInterpreter(this.SipIP, this.SipPort);
                if (this.InvokeRequired)
                    this.BeginInvoke(new Auxiliar_Obj(ChangeStatusPicture), new object[] { Properties.Resources.Led3 });
                else
                    ChangeStatusPicture(Properties.Resources.Led3);
                //daemon.disconnected += new EventHandler(Reconnect_Daemon);

                //daemon.dataReceived += new EventHandler(Daemon_DataReceived);

                //receiveThread = new Thread(new ThreadStart(daemon.ReceiveData));
                //receiveThread.Priority = ThreadPriority.AboveNormal;
                //receiveThread.IsBackground = true;
                //receiveThread.Start();
            }
            catch (Exception e)
            {
                if (this.InvokeRequired)
                    this.BeginInvoke(new Auxiliar_Obj(ChangeStatusPicture), new object[] { Properties.Resources.Led1 });
                else
                    ChangeStatusPicture(Properties.Resources.Led1);
                throw new Exception();
            }
        }

        private void Reconnect()
        {
            bool value = false;
            while (true)
            {
                if (threadControl)
                    break;
                if (daemon != null)
                {
                    try
                    {
                        if (!daemon.IsConnected)
                        {
                            value = false;
                            DaemonInteraction();
                            try
                            {
                                if (actualizeDaemon.IsBackground || actualizeDaemon.ThreadState == System.Threading.ThreadState.Background || actualizeDaemon.IsAlive)
                                    actualizeDaemon.Abort();
                            }
                            catch { }
                           
                            DaemonThread();

                        }
                        else
                        {
                            value = true;
                        }
                    }
                    catch
                    {
                        value = false;
                        if(this.InvokeRequired)
                            this.Invoke(new WriteTextBox(WriteLogTextBox), new object[] { "", "", "[CANTCONNECT]" });
                    }
                }
                else
                {
                    if(!value)
                    {
                        try
                        {
                            DaemonInteraction();
                            try
                            {
                                if (actualizeDaemon.IsBackground || actualizeDaemon.ThreadState == System.Threading.ThreadState.Background || actualizeDaemon.IsAlive)
                                    actualizeDaemon.Abort();
                            }
                            catch { }
                            DaemonThread();
                        }
                        catch
                        {
                            if(this.InvokeRequired)
                                this.Invoke(new WriteTextBox(WriteLogTextBox), new object[] { "", "", "[CANTCONNECT]" });
                        }
                    }
                }
            }
        }

        private void ChangeStatusPicture(object picture)
        {
            //statusPictureBox.Image.
            statusPictureBox.Image = (Bitmap)picture;
        }

        private void ActualizeDaemonDB()
        {
            List<string> users = Users;
            List<string> groups = Groups;

            try
            {
                daemon.SendData(new List<string> { "cleardata" });
                Thread.Sleep(150);
            }
            catch
            {
            }

            //Update table - Delete Users that are not in domain or Add them to the context on Linux
            foreach (DataRow row in Table.Rows)
            {
                    try
                    {
                        if (daemon.IsConnected)
                        {
                            daemon.SendData(new List<string> { "createuser", (String)row["FullName"], (String)row["userId"], (String)row["pass"], (String)row["displayId"] });
                            Thread.Sleep(30);
                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }
            }

            foreach (string group in groups)
            {
                try
                {
                    if (daemon.IsConnected)
                    {
                        daemon.SendData(new List<string> { "creategroup", group });
                        Thread.Sleep(150);
                    }
                }
                catch (Exception)
                {
                }
                foreach(string user in DomainClassHelper.GetUsersByGroup(group,Domain,GroupPath))
                {
                    try
                    {
                        if (daemon.IsConnected)
                        {
                            daemon.SendData(new List<string> { "modifygroup", group, (String)dbManager.ReturnByUser(user,Table)["FullName"], (String)dbManager.ReturnByUser(user,dataTable)["userId"] });
                            Thread.Sleep(150);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            try
            {
                if (daemon.DaemonClient.Connected)
                {
                    daemon.SendData(new List<string> { "reload" });
                    Thread.Sleep(150);
                }
            }
            catch (Exception)
            {
            }


                //    daemon.SendData(new List<string> { "creategroup", group });
                //    daemon.SendData(new List<string> { "modifygroup", addGroupForm.GroupName, user, (String)this.dbManager.ReturnByUser(user)["userId"] });
                //    daemon.SendData(new List<string> { "deleteusergroup", user, ShowUsersFormFromGroup.GroupName, (String)this.dbManager.ReturnByUser(user)["userId"] });
                //    daemon.SendData(new List<string> { "deletegroup", groupsTreeView.SelectedNode.Text });
                //    daemon.SendData(new List<string> { "deleteuser", (String)row["ADUser"] });
                //    daemon.SendData(new List<string> { "createuser", user, (string)data["userId"], (string)data["pass"], (string)data["displayId"]});
        }

        private void ModifyUser(DataRow userData)
        {
            var formParams = new UpdateForm((String)userData["ADUser"],
                                            (String)userData["userId"],
                                            (String)userData["displayId"],
                                            (String)userData["pass"],
                                            dbManager);

            using (formParams)
            {
                if (formParams.ShowDialog() == DialogResult.OK)
                {
                    Table = dbManager.GetDataTable();
                    UpdateUserSetting((String)userData["FullName"]);
                    ActualizeDaemon();
                }
            }
        }

        private void UpdateUserSetting(string fullname)
        {
            try
            {
                if(ClientConnected.IndexOf(fullname) >= 0)
                {
                    DataRow dr = dbManager.ReturnByFullName(fullname,Table);
                    server.SendTo(ClientConnected[ClientConnected.IndexOf(fullname)], "[USERPARAMS][USERID]" + dr["userId"] + "[PASS]" + dr["pass"] + "[DISPLAYID]" + dr["FullName"] + "[SIPSERVERIP]" + SipIP + "[SIPSERVERPORT]" + SipPort);
                    new Thread(new ParameterizedThreadStart(UpdateUserCatalog)).Start(ClientConnected[ClientConnected.IndexOf(fullname)]);                        
                }
            }
            catch { }
        }

        private void UpdateUsersCatalog()
        {
            try
            {
                foreach (string client in ClientConnected)
                {
                    if (Users.Contains((String)dbManager.ReturnByFullName(client,dataTable)["ADUser"]))
                    {
                        new Thread(new ParameterizedThreadStart(UpdateUserCatalog)).Start(client);
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        private void UpdateUserCatalog(object Client)
        {
            string client = (String)Client;
            List<string> argumentsToSend = new List<string>();
            //List<string> clientgroups;
            List<string> listofusers;
            string message = "";
            DataRow clientData;
            try
            {
                argumentsToSend.Clear();
                message = "";
                clientData = dbManager.ReturnByFullName(client, dataTable);

                foreach (string group in Groups)
                {
                    listofusers = DomainClassHelper.GetUsersByGroup(group, Domain, GroupPath);
                    if (!listofusers.Contains((String)clientData["ADUser"]))
                        continue;
                    foreach (string user in listofusers)
                    {
                        if (!clientData["ADUser"].ToString().Equals(user))
                        {
                            DataRow datauser = dbManager.ReturnByUser(user, dataTable);
                            argumentsToSend.Add("[" + group + "]" + (String)datauser["userId"] + ":" + (String)datauser["displayId"]);
                        }
                    }
                }
                message = string.Join(",", argumentsToSend);
                try
                {
                    server.SendTo(client, "[CATALOG]" + message);
                }
                catch (Exception e)
                {
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void StopAllWorkingThreads()
        {
            try
            {
                try
                {
                    if (usersTV.ThreadState == System.Threading.ThreadState.Running || usersTV.IsBackground || usersTV.IsAlive)
                    {
                        usersTV.Abort();
                        Thread.Sleep(100);
                    }
                }
                catch { }
                try
                {
                    if (groupTV.ThreadState == System.Threading.ThreadState.Running || groupTV.IsBackground || groupTV.IsAlive)
                    {
                        groupTV.Abort();
                        Thread.Sleep(100);
                    }
                }
                catch { }
                try
                {
                    if (checkDB.ThreadState == System.Threading.ThreadState.Running || checkDB.IsBackground || checkDB.IsAlive)
                    {
                        checkDB.Abort();
                        Thread.Sleep(100);
                    }
                }
                catch { }
                try
                {
                    if (actualizeDaemon.ThreadState == System.Threading.ThreadState.Running || actualizeDaemon.IsBackground || actualizeDaemon.IsAlive)
                    {
                        actualizeDaemon.Abort();
                        Thread.Sleep(100);
                        threadControl = true;
                    }
                }
                catch { }
                /*
                try
                {
                    if (receiveThread.ThreadState == System.Threading.ThreadState.Running || receiveThread.IsBackground || receiveThread.IsAlive)
                    {
                        receiveThread.Abort();
                        Thread.Sleep(100);
                        threadControl = true;
                    }
                }
                catch { }
                
                try
                {
                    if (reconnect.ThreadState == System.Threading.ThreadState.Running || reconnect.IsBackground || reconnect.IsAlive)
                    {
                        reconnect.Abort();
                        Thread.Sleep(100);
                        threadControl = true;
                    }
                }
                catch { }
                */
                try
                {
                    if (logsThread.ThreadState == System.Threading.ThreadState.Running || logsThread.IsBackground || logsThread.IsAlive)
                    {
                        logsThread.Abort();
                        Thread.Sleep(100);
                        threadControl = true;
                    }
                }
                catch { }
            }
            catch { }

        }

        private void HighLightUserNode(string Char)
        {
            try
            {
                if (usersTreeView.SelectedNode != null)
                {
                    //TreeNodeCollection nodes = usersTreeView.Nodes;
                    //nodes.nodes.IndexOf(usersTreeView.SelectedNode);
                    foreach (TreeNode node in usersTreeView.Nodes)
                    {
                        if (node.Text.ToLower().StartsWith(Char.ToLower()))
                        {
                            usersTreeView.SelectedNode = node;
                            usersTreeView.Focus();
                            break;
                        }
                    }
                }
                keysToFind.Substring(0, keysToFind.Length - 1);
            }
            catch { }
        }

        private void GetLogsFromDir()
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\DSCA Solutions\\LOGS");
                FileInfo[] files = dirInfo.GetFiles("*.txt");
                if (this.InvokeRequired)
                    this.BeginInvoke(new ChargeLogs(LoadLogs), new object[] { files });
                else
                    LoadLogs(files);
            }
            catch (Exception)
            {
            }
        }
        
        private void LoadLogs(FileInfo [] LogNames)
        {
            try
            {
                //ListViewGroup group = new ListViewGroup("logs", "Logs");
                //group.Items.Add(new ListViewItem(new string[]{"a","b","c"}));
                logsListView.Items.Clear();

                foreach (FileInfo log in LogNames)
                {
                    ListViewItem item = new ListViewItem(new string[] { log.Name.Remove(log.Name.IndexOf(".")), log.LastWriteTime.ToString(), 
                        log.Extension, log.Length.ToString() + " bytes" }, 0);// group);

                    logsListView.Items.Add(item);
                }
            }
            catch { }
        }

        private void GetOnOffUsersToListView()
        {
            //try
            //{
            //    ListViewGroup group = new ListViewGroup();
            //    //foreach(Users 
            //}
            //catch { }
        }

        //private void GetFileInfo()
        //{
            
        //    foreach (FileInfo file in filesInfo)
        //    {
        //        //file.
        //    }
        //}
        #endregion

        #region Callbacks Events
        
        //private void ShowIntIPAdd(object sender, EventArgs e)
        //{
        //    if(((NetworkInterface)interfaceList[comboBox1.SelectedIndex]).GetIPProperties().UnicastAddresses.Count > 1)
        //        interfaceIpAddress = ((NetworkInterface)interfaceList[comboBox1.SelectedIndex]).GetIPProperties().UnicastAddresses[1].Address.ToString();
        //    else
        //        interfaceIpAddress = ((NetworkInterface)interfaceList[comboBox1.SelectedIndex]).GetIPProperties().UnicastAddresses[0].Address.ToString();

        //    try
        //    {
        //        label3.Text = interfaceIpAddress;
        //    }
        //    catch (Exception Ex) { }
        //}


        private void logsFileSystemWatcher_WriteLog(object sender, FileSystemEventArgs e)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new Auxiliar(GetLogsFromDir));
            else
                GetLogsFromDir();
        }

        private void FormClosed_StopServer(object sender, FormClosedEventArgs e)
        {
            try
            {
                try
                {
                    StopAllWorkingThreads();
                    server.Stop();
                    receiveThread.Abort();
                    daemon.CloseClient();
                    quitThread = true;
                }
                catch { }
                finally
                {
                    Application.Exit();
                }                
            }
            catch
            {
            }
        }

        private void UserDataGridView_OnDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRow userData = dbManager.ReturnByUser((String)usersDataGridView["ADUser", e.RowIndex].Value, Table);
            //DataRow [] userData = dataTable.Select("ADUser = \'" + (String)usersDataGridView["ADUser", e.RowIndex].Value + "\' AND userId == \'" +
            //    (String)usersDataGridView["userId", e.RowIndex].Value + "\' AND FullName == '" + (String)usersDataGridView["FullName", e.RowIndex].Value + "\'");
            ModifyUser(userData);
        }

        private void GroupsTreeView_MouseUp(object sender, MouseEventArgs e)
        {
            Point clickPoint = new Point(e.X, e.Y);

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeNode selectedNode = (groupsTreeView.SelectedNode = groupsTreeView.GetNodeAt(clickPoint));
                if ((selectedNode) == null)
                {
                    groupsContextMenuStrip.Show(Cursor.Position);
                }

                else
                {
                    groupsPropertiesContextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        private void UsersTreeView_OnMouserUp(object sender, MouseEventArgs e)
        {
            Point clickPoint = new Point(e.X, e.Y);
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeNode selectedNode = (usersTreeView.SelectedNode = usersTreeView.GetNodeAt(clickPoint));
                if ((selectedNode) != null)
                {
                    usersContextMenuStrip.Show(Cursor.Position);
                }
            }
        }

        private void AddUserToGroup_OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeNode nodeSelected = groupsTreeView.SelectedNode;
                string groupName = nodeSelected.Text;

                List<string> users = new List<string>();

                foreach (TreeNode t in usersTreeView.Nodes)
                {
                    if (!FindNode(groupName, t.Nodes))
                        users.Add(t.Text);
                }

                var addGroupForm = new AddUserToGroupForm(users, groupName, GroupPath, UsersPath, Domain);

                using (addGroupForm)
                {
                    if (addGroupForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //foreach (string user in addGroupForm.Users)
                        //{
                        //    //try
                        //    //{
                        //    //    daemon.SendData(new List<string> { "modifygroup", addGroupForm.GroupName, user, (String)this.dbManager.ReturnByUser(user)["userId"] });
                        //    //}
                        //    //catch (Exception)
                        //    //{
                        //    //    //MessageBox.Show("El cliente esta desconectado del Servidor.\nPor favor comunique con el encargado de la red.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    //    //Reconnect();
                        //    //    //daemon.SendData(new List<string> { "modifygroup", addGroupForm.GroupName, user, (String)this.dbManager.ReturnByUser(user)["userId"] });
                        //    //}

                        //    //Thread.Sleep(150);
                        //}
                        UsersTreeViewThread();// new Thread(new ParameterizedThreadStart(PoblateUsersTree)).Start(users);
                        ActualizeDaemon();
                    }
                }
            }
        }

        private void ShowUsers_OnClick(object sender, MouseEventArgs e)
        {
            List<string> users = new List<string>();
            if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeNode nodeSelected = groupsTreeView.SelectedNode;
                string groupName = nodeSelected.Text;

                var ShowUsersFormFromGroup = new ShowUsersPerGroup(groupName, GroupPath, Domain, UsersPath);

                using (ShowUsersFormFromGroup)
                {
                    if (ShowUsersFormFromGroup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //users = ShowUsersFormFromGroup.usersData;
                        //foreach (string user in users)
                        //{
                        //    //try
                        //    //{
                        //    //    daemon.SendData(new List<string> { "deleteusergroup", user, ShowUsersFormFromGroup.GroupName, (String)this.dbManager.ReturnByUser(user)["userId"] });
                        //    //}
                        //    //catch (Exception)
                        //    //{
                        //    //    //MessageBox.Show("El cliente esta desconectado del Servidor.\nPor favor comunique con el encargado de la red.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    //    //Reconnect();
                        //    //    //daemon.SendData(new List<string> { "deleteusergroup", user, ShowUsersFormFromGroup.GroupName, (String)this.dbManager.ReturnByUser(user)["userId"] });
                        //    //}
                        //    //Thread.Sleep(150);
                        //}
                        UsersTreeViewThread();// new Thread(new ParameterizedThreadStart(PoblateUsersTree)).Start(users);
                        ActualizeDaemon();
                    }
                }
            }
        }

        private void AddGroupsContextStripMenu_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                var addGroupForm = new AddGroupForm(this.GroupPath.Substring(GroupPath.IndexOf("LDAP://") + 7), this.Domain);
                using (addGroupForm)
                {
                    if (addGroupForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //try
                        //{
                        //    daemon.SendData(new List<string> { "creategroup", addGroupForm.GroupName });
                        //}
                        //catch (Exception)
                        //{
                        //    //MessageBox.Show("El cliente esta desconectado del Servidor.\nPor favor comunique con el encargado de la red.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    //Reconnect();
                        //    //daemon.SendData(new List<string> { "creategroup", addGroupForm.GroupName });
                        //}
                        Groups = DomainClassHelper.GetGroupsFromDomain(Domain, GroupPath);
                        GroupsTreeViewThread();// groupTV.Start();
                        ActualizeDaemon();
                    }
                }
            }
        }

        private void DeleteGroup_OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (MessageBox.Show("Esta seguro que desea eliminar el grupo?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                {
                    List<string> users = DomainClassHelper.GetUsersFromDomain(UsersPath,Domain);
                    try
                    {
                        DomainClassHelper.RemoveGroup(
                            this.GroupPath.Substring(this.GroupPath.IndexOf("LDAP://") + 7),
                            this.groupsTreeView.SelectedNode.Text);
                        //try
                        //{
                        //    daemon.SendData(new List<string> { "deletegroup", groupsTreeView.SelectedNode.Text });
                        //}
                        //catch (Exception)
                        //{
                        //    //MessageBox.Show("El cliente esta desconectado del Servidor.\nPor favor comunique con el encargado de la red.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    //Reconnect();
                        //    //daemon.SendData(new List<string> { "deletegroup", groupsTreeView.SelectedNode.Text });
                        //}
                    }
                    catch (Exception ex)
                    {
                    }

                    Groups = DomainClassHelper.GetGroupsFromDomain(Domain, GroupPath);
                    UsersTreeViewThread();// new Thread(new ParameterizedThreadStart(PoblateUsersTree)).Start(users);
                    GroupsTreeViewThread();// groupTV.Start();
                    ActualizeDaemon();
                }
            }
        }

        private void AddToGroupToolContextStrip_OnMouseUp(object sender, MouseEventArgs e)
        {
            Point clickPoint = new Point(e.X, e.Y);
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    TreeNode nodeSelected = usersTreeView.SelectedNode;

                    //////////////////////////////
                    //////////////////////////////
                    //////////////////////////////
                    //////////////////////////////
                    //Poner aqui enumerar todos los grupos donde el usuario no se encuentre.
                }
            }
        }

        private void ModifyUserToolContextStrip_OnMouseUp(object sender, MouseEventArgs e)
        {
            Point clickPoint = new Point(e.X, e.Y);

            if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeNode nodeSelected = usersTreeView.SelectedNode;

                ModifyUser(dbManager.ReturnByUser(nodeSelected.Text,dataTable));
            }
        }

        private void NetworkConfig_OnClick(object sender, EventArgs e)
        {
            var NetworkConfig = new NetworkConfigForm();

            try
            {
                using (NetworkConfig)
                {
                    if (NetworkConfig.ShowDialog() == DialogResult.OK)
                    {
                        if (NetworkConfig.ServerIP != ServerIP || NetworkConfig.ServerPort != ServerPort ||
                            NetworkConfig.SipIP != SipIP || NetworkConfig.SipPort != SipPort)
                        {
                            ServerPort = NetworkConfig.ServerPort;
                            ServerIP = NetworkConfig.ServerIP;
                            SipPort = NetworkConfig.SipPort;
                            SipIP = NetworkConfig.SipIP;

                            RestartServer();
                            InitializeServer();
                            DaemonInteraction();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        //Aqui ta durando mucho.....
        //Arreglar esta parte....
        private void Refresh_OnClick(object sender, EventArgs e)
        {
            GetObjectsFromDomain();
            InsertToDB(Users);
            //UTV();
            //GTV();
            ActualizeDaemon();
            //updateButton_Click(null, EventArgs.Empty);
        }

        private void Close_OnClick(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    StopAllWorkingThreads();
                    server.Stop();
                    receiveThread.Abort();
                    daemon.CloseClient();
                }
                catch { }
                finally
                {
                    Environment.Exit(1);
                    Application.Exit();
                }
            }
            catch
            {
            }
        }

        private void ConfigDataBase_OnClick(object sender, EventArgs e)
        {
            var enumSQLInstances = new EnumSQLInstances();

            using (enumSQLInstances)
            {
                if (enumSQLInstances.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.ConnectionString = enumSQLInstances.ConnString;
                    dbManager = new DbManager(this.ConnectionString);
                    GetDataTableThread();
                }
            }
        }

        private void DomainConfiguration_OnClick(object sender, EventArgs e)
        {
            var DomainOu = new DomainOUForm(this.GroupPath);

            using (DomainOu)
            {
                if (DomainOu.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.GroupPath = DomainOu.GroupPath;
                    this.UsersPath = DomainOu.UsersPath;
                }
            }
        }

        private void usersTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;

            if (e.KeyValue >= 65 && e.KeyValue <= 90 || e.KeyValue >= 97 && e.KeyValue <= 122 || e.KeyData == Keys.Space)
            {
                if (this.InvokeRequired)
                    this.BeginInvoke(new KeyboardChar(HighLightUserNode), new object[] { Char.ConvertFromUtf32(e.KeyValue) });
                else
                    HighLightUserNode(keysToFind += Char.ConvertFromUtf32(e.KeyValue));
            }

            timer.Interval = 1500;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            keysToFind = "";
            timer.Stop();
        }

        private void usersTreeView_KeyUp(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = false;
        }

        private void logsListView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string file = logsListView.SelectedItems[0].SubItems[0].Text + ".txt";

                Process.Start(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles)
                    + "\\DSCA Solutions\\LOGS\\" + file);
            }
            catch { }
        }
        #endregion

        #region Asterisk Calls
        private void Daemon_DataReceived(object sender, EventArgs e)
        {
            List<string> data = ((DaemonInterpreter)sender).Data;

            switch (data[0])
            {
                case "[EXISTS]":
                    break;
                case "[NOTEXISTS]":
                    break;
                case "[DONE]":
                    break;
                case "[ERROR]":
                    break;
                case "[FINALLE]":
                    break;
                case "[GROUP]":
                    break;
                case "[USER]":
                    break;
                default:
                    break;
            }
        }

        private void DataReceived()
        {

        }
        #endregion

        #region Delegates Functions
        #region Server Events
        private void server_OnClientConnected(object Sender, ConnectedArguments CA)
        {
            try
            {
                if (Users.Contains((String)dbManager.ReturnByFullName(CA.Name, dataTable)["ADUser"]))
                {
                    if (this.InvokeRequired)
                    {
                        if(!ClientConnected.Contains(CA.Name))
                            ClientConnected.Add(CA.Name);
                        ChangeIconUser(4, CA.Name);
                        this.Invoke(new WriteTextBox(WriteLogTextBox), new object[] { CA.Name, CA.Ip, "[CONNECTED]" });
                    }
                }

            }catch
            {
                this.Invoke(new WriteTextBox(WriteLogTextBox), new object[] { CA.Name, CA.Ip, "[ATTEMPT]" });
            }
        }

        public void server_OnClientDisconnected(object sender, DisconnectedArguments e)
        {
            try
            {
                if (Users.Contains((String)dbManager.ReturnByFullName(e.Name, dataTable)["ADUser"]))
                    if (this.InvokeRequired)
                    {
                        ClientConnected.Remove(e.Name);
                        ChangeIconUser(3, e.Name);
                        this.Invoke(new WriteTextBox(WriteLogTextBox), new object[] { e.Name, e.Ip, "[DISCONNECTED]" });
                    }
            }
            catch (Exception ex)
            {
            }
        }

        private void server_ErrorArguments(object sender, ErrorArguments e)
        {
            
        }

        private void server_OnReceivedData(object sender, ReceivedArguments e)
        {
            string data = e.ReceivedData;
            string client = e.Name;

            if (data.StartsWith("[CHECKAVAILABILITY]"))
            {
                if (dbManager.ReturnByFullName(client,dataTable) != null)
                {
                    string adUser = (String)dbManager.ReturnByFullName(client, dataTable)["ADUser"];
                    if (Users.Contains(adUser))
                    {
                        server.SendTo(client, "[EXISTS]");
                    }
                    else
                    {
                        server.SendTo(client, "[NOTEXISTS]");
                    }
                }
                else
                    server.SendTo(client, "[NOTEXISTS]");
            }
            
            else if (data.StartsWith("[READYTORECIEVE]"))
            {
                DataRow dr = dbManager.ReturnByFullName(client,dataTable);

                server.SendTo(client, "[USERPARAMS][USERID]" + dr["userId"] + "[PASS]" + dr["pass"] + "[DISPLAYID]" + dr["FullName"] + "[SIPSERVERIP]" + SipIP + "[SIPSERVERPORT]" + SipPort);
                new Thread(new ParameterizedThreadStart(UpdateUserCatalog)).Start(client);
            }

            else if (data.StartsWith("[LOG]"))
            {
                try
                {
                    data = data.Substring(5);
                    string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) + "\\DSCA Solutions\\LOGS";// + e.Name;

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    //if(!Directory.Exists(path + "\\" + e.Name))
                    //    Directory.CreateDirectory(path + "\\" + e.Name);

                    WriteAFile.WriteFile(path + "\\" + e.Name, "Logs_" + e.Name + ".txt", data.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries), true);
                }
                catch { }
            }
        }
        #endregion
        private void WriteLogTextBox(string Name, string IP, string Stmt)
        {
            switch (Stmt)
            {
                case "[CONNECTED]":
                    this.logTextBox.AppendText(Name + " se ha conectado - Ip: " + IP + ".\n");
                    break;
                case "[DISCONNECTED]":
                    this.logTextBox.AppendText(Name + " se ha desconectado..." + "\n");
                    break;
                case "[CANTCONNECT]":
                    this.logTextBox.AppendText("Servidor no se puede conectar con Daemon. Reintentando..." + "\n");
                    break;
                case "[ATTEMPT]":
                    this.logTextBox.AppendText("Atento de conexion: " + Name + " : " + IP + "\n");
                    break;
            }
        }
        #endregion

        #region Button Handlers
        private void SearchBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && searchTextBox.Text != "")
            {
                SearchButton_OnClick(this, EventArgs.Empty);
            }
        }

        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            this.logTextBox.Clear();
        }

        private void RestartServer()
        {
            try
            {
                if (server != null)
                {
                    server.Stop();
                    server = null;
                }
            }
            catch (Exception ex)
            {
            }
        }
        
        private void SearchButton_OnClick(object sender, EventArgs e)
        {
            if (searchTextBox.Text != "")
            {
                usersDataGridView.ClearSelection();

                string searchCriteria = "";
                switch(searchByComboBox.SelectedItem.ToString())
                {
                    case "Cuenta Usuario":
                        searchCriteria = "ADUser";
                        break;
                    case "Extension":
                        searchCriteria = "userId";
                        break;
                    case "Nombre de Inicio de Sesion":
                        searchCriteria = "FullName";
                        break;
                    case "Nombre Usuario":
                        searchCriteria = "displayId";
                        break;
                }
                foreach (DataGridViewRow items in usersDataGridView.Rows)
                {
                    if (items.Cells[searchCriteria].Value.ToString().ToLower().StartsWith((String)searchTextBox.Text.ToLower()))
                    {
                        usersDataGridView.Rows[items.Index].Selected = true;
                        usersDataGridView.FirstDisplayedScrollingRowIndex = items.Index;
                        return;
                    }
                }

                MessageBox.Show("No se pudo encontrar \"" + (String)searchTextBox.Text + "\".\nIntente otra palabra clave.","No encontrado",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void clearLogButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esa seguro que desea eliminar las entradas?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                Array.ForEach(Directory.GetFiles(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) + "\\DSCA Solutions\\LOGS\\"), delegate(string f) { File.Delete(f); });
                this.BeginInvoke(new Auxiliar(GetLogsFromDir));
            }
        }
        #endregion        

        private void acercaDeDSCAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().Show();
        }

        private void comoUsarDSCAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new Form2
            new frmInstrucciones().Show();
        }
    }

    partial class DaemonInterpreter
    {
        private static string port = "";
        private static string header = "";
        private static string ip = "";

        private bool threadControl = false;
        private bool connected = false;

        private static List<string> data = new List<string>();

        private static TcpClient client;

        public TcpClient DaemonClient
        {
            get
            {
                try
                {
                    return client;
                }
                catch (Exception ex)
                {
                    throw new SocketException();
                }
            }
        }
        private string Ip
        {
            get { return ip; }
            set { ip = value; }
        }
        private string Port
        {
            get { return port; }
            set { port = value; }
        }
        public string Header
        {
            get { return header; }
            set { header = value; }
        }
        public List<string> Data
        {
            get { return data; }
            set { data = value; }
        }

        public bool IsConnected
        {
            get { return connected; }
            set
            {
                connected = value;
                if (!connected)
                {
                    ClientDisconnected();
                }
            }
        }
        /*
        public bool IsConnected
        {
            get
            {
                try
                {
                    if (!threadControl)
                    {
                        threadControl = true;
                        if (DaemonClient != null && DaemonClient.Client != null)// && !DaemonClient.Client.Connected)
                        {
                            if (DaemonClient.Client.Poll(0, SelectMode.SelectRead))
                            {
                                byte[] buff = new byte[1];
                                if (DaemonClient.Client.Receive(buff, SocketFlags.Peek) == 0)
                                {
                                    ClientDisconnected();
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            return true;
                        }
                        else
                        {
                            return true;
                        }

                        //Thread.Sleep(100);
                        threadControl = false;
                    }
                }
                catch
                {
                    ClientDisconnected();
                    return false;
                }

                return true;            
            }
            
        }
        */
        public DaemonInterpreter(string ip, string port)
        {
            this.Port = port;
            this.Ip = ip;

            try
            {
                client = new TcpClient(Ip, 2577);
            }
            catch (Exception e)
            {
                throw new Exception("Error en la conexion!");
            }
            connected = true;
        }

        ~DaemonInterpreter()
        {
            try
            {
                this.CloseClient();
            }
            catch { }
        }

        public void ReceiveData()
        {
            try
            {
                NetworkStream ns = DaemonClient.GetStream();
                StreamReader stream = new StreamReader(ns);
                string dataLine = "";

                try
                {
                    while (true)
                    {
                        while (!(dataLine = stream.ReadLine()).StartsWith("##########"))
                        {
                            Data.Add(dataLine);
                        }

                        DataReceive();
                        Data.Clear();
                    }
                }
                catch (Exception e)
                {
                    ns.Close();
                    stream.Close();
                    connected = false;
                }
            }
            catch (Exception e)
            {
                //throw new SocketException();
            }
        }

        public void SendData(List<string> dataToSend)
        {
            try
            {
                NetworkStream ns = DaemonClient.GetStream();
                StreamWriter sw = new StreamWriter(ns);

                try
                {
                    foreach (string data in dataToSend)
                    {
                        sw.WriteLine(data);
                        //sw.Flush();
                        Thread.Sleep(50);
                    }
                    sw.Flush();
                    Thread.Sleep(300);
                }

                catch (Exception e)
                {
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                connected = false;
                throw new Exception(e.Message);
            }
        }

        public void CloseClient()
        {
            dataReceived = null;

            if (client.Connected)
                client.Close();
        }        

        protected virtual void DataReceive()
        {
            if (dataReceived != null)
                dataReceived(this, EventArgs.Empty);
        }

        protected virtual void ClientDisconnected()
        {
            if (disconnected != null)
                disconnected(client, EventArgs.Empty);
        }

        public event EventHandler dataReceived;
        public event EventHandler disconnected;
    }
}
