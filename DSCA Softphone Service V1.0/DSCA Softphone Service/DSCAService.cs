using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using NetworksApi.TCP.CLIENT;
using NetworksApi.TCP.SERVER;
using TextFile;
using System.Management;
using System.Security.Principal;
using System.Threading;
using System.IO;

namespace DSCA_Softphone_Service
{
    public partial class DSCAService : ServiceBase
    {
        #region Variables

        private Server server;
        private Client client;
        private System.Timers.Timer timer = new System.Timers.Timer();
        private Thread reconnectThread;

        private string serviceIp = "127.0.0.1";
        private string servicePort = "2500";

        private string serverIP;
        private string serverPORT;

        private string userName = "";
        private string id = "";
        private string password = "";
        private string hostname = "";
        private string port = "";
        //private static bool _existsClient = false;
        private readonly ManagementClass _wmiComputerSystem = new ManagementClass("Win32_ComputerSystem");
        private string User = "";
        private string FileDirectory = "";//C:\\Program Files\\DSCA Solutions";
        private List<string> sipParameters = new List<string>();
        string[] blah = { "hola", "que hace?", "", "" };

        private bool LogControl = false;
        private bool ReconnectControl = false;
        #endregion

        #region Properties
        public string serverIp
        {
            get { return serverIP; }

            set
            {
                this.serverIP = value;
                Properties.Settings.Default["ServerIp"] = this.serverIp;
                Properties.Settings.Default.Save();
            }
        }
        public string serverPort
        {
            get { return serverPORT; }

            set
            {
                this.serverPORT = value;
                Properties.Settings.Default["ServerPort"] = this.serverPort;
                Properties.Settings.Default.Save();
            }
        }
        
        public string Username
        {
            get { return this.userName; }
            set { this.userName = value; }
        }
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
        public string Hostname
        {
            get { return this.hostname; }
            set { this.hostname = value; }
        }
        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public string Port
        {
            get { return this.port; }
            set { this.port = value; }
        }
        public string user
        {
            get { return this.User; }
            set { this.User = value; }
        }
        #endregion

        #region Constructor
        public DSCAService()
        {
            InitializeComponent();
            InitializeParameters();

            FileDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\DSCA Solutions";
        }
        #endregion

        #region Functions
        private void InitializeParameters()
        {
            this.CanHandleSessionChangeEvent = true;
            this.CanHandlePowerEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;
            this.AutoLog = true;
        }

        private void startServer()
        {
            server = new Server(serviceIp, servicePort);
            startServerHandlers();
            server.Start();
        }

        private void startServerHandlers()
        {
            //Server event delegates
            server.OnClientConnected += new OnConnectedDelegate(server_OnClientConnected);
            server.OnClientDisconnected += new OnDisconnectedDelegate(server_OnClientDisconnected);
            server.OnDataReceived += new OnReceivedDelegate(server_OnDataReceived);
            server.OnServerError += new OnErrorDelegate(server_OnServerError);
        }
        
        private void startClientHandlers()
        {
            client.OnClientConnected += new OnClientConnectedDelegate(client_OnClientConnected);
            client.OnClientConnecting += new OnClientConnectingDelegate(client_OnClientConnecting);
            client.OnClientDisconnected += new OnClientDisconnectedDelegate(client_OnClientDisconnected);
            client.OnClientError += new OnClientErrorDelegate(client_OnClientError);
            client.OnClientFileSending += new OnClientFileSendingDelegate(client_OnClientFileSending);
            client.OnDataReceived += new OnClientReceivedDelegate(client_OnDataReceived);
        }

        private void startClient()
        {
            client = new Client();
            client.ServerIp = this.serverIp;
            client.ServerPort = this.serverPort;
            //re-check this part
            client.ClientName = this.user = GetExplorerUser(false);

            startClientHandlers();
            Thread.Sleep(200);
            try
            {
                client.Connect();
            }
            catch (Exception)
            {
            }
        }

        private void closeClient()
        {
            try
            {
                client.Disconnect();
                client = null;
            }
            catch (Exception)
            {
            }
        }
        
        private string GetExplorerUser(bool choice)
        {
            var query = new ObjectQuery(
                "SELECT * FROM Win32_Process WHERE Name = 'explorer.exe'");

            var explorerProcesses = new ManagementObjectSearcher(query).Get();

            foreach (ManagementObject mo in explorerProcesses)
            {
                string[] ownerInfo = new string[2];
                ownerInfo[0] = "";

                //WriteAFile.WriteFile("C:\\", "SeQuedo.txt", blah, false);
                
                while(ownerInfo[0].ToString().Equals(""))
                    mo.InvokeMethod("GetOwner", (object[])ownerInfo);

                //WriteAFile.WriteFile("C:\\", "Salio.txt", ownerInfo, false);

                if (!choice)
                    return String.Concat(ownerInfo[0]);
                else
                    return String.Concat(ownerInfo[1] + "\\" + ownerInfo[0]);
            }

            return string.Empty;
        }

        //Para saber si es admin o no
        private bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            return isAdmin;
        }

        private void WriteLog(string logEntries, string user, string Action)
        {
            List<string> auxList = new List<string>();
            auxList.Add("---------------" + DateTime.Now + "---------------");
            auxList.Add(Action + ": " + user);
            auxList.Add("");
            auxList.Add(logEntries);
            auxList.Add("");

            WriteAFile.WriteFile(FileDirectory, "DSCALog.txt", auxList.ToArray(), true);
        }

        private void StartFileWatcherForLogs()
        {
            try
            {
                //Debugger.Launch();
                this.LogFileSystemWatcher = new FileSystemWatcher();

                this.LogFileSystemWatcher.Path = /*System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);*/ReturnAppDataPath();
                this.LogFileSystemWatcher.Filter = "LastCallLog.txt";
                
                this.LogFileSystemWatcher.IncludeSubdirectories = false;
                this.LogFileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite;
                
                this.LogFileSystemWatcher.Changed += new FileSystemEventHandler(LogFileSystemWatcher_Changed);
                this.LogFileSystemWatcher.EnableRaisingEvents = true;
            }
            catch { }
        }

        //private void Reconnect()
        //{
        //    while (ReconnectControl)
        //    {
        //        try
        //        {
        //            if (!client.IsConnected)
        //            {
        //                startClient();
        //                Thread.Sleep(5000);
        //            }
        //        }
        //        catch { try { startClient(); } catch { } }

        //        Thread.Sleep(1000);
        //    }
        //}

        //private void StartReconnectThread()
        //{
        //    Debugger.Launch();
        //    try
        //    {
        //        reconnectThread = new Thread(new ThreadStart(Reconnect));
        //        reconnectThread.IsBackground = true;
        //    }
        //    catch { }
        //}
        private void StartTimerForReconnect()
        {
            try
            {
                timer.AutoReset = true;
                timer.Interval = 5000;
                timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
                timer.Start();
            }
            catch { }
        }

        private string ReturnAppDataPath()
        {
            OperatingSystem os = Environment.OSVersion;

            if((os.Platform == PlatformID.Win32NT) && ((os.Version.Major > 5) || ((os.Version.Major == 5) && os.Version.Minor >= 1)))
            {
                return "C:\\Documents and Settings\\" + this.user + "\\Application Data";
            }

            else if (os.Platform == PlatformID.Win32NT && os.Version.Major >= 6)
            {
                return "C:\\Users\\" + this.user + "\\AppData";
            }

            return "";
        }
        #endregion

        #region Event Handlers
        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (!client.IsConnected)
                {
                    startClient();
                    //StartTimerForReconnect();
                }
            }
            catch { startClient(); }
        }

        private void LogFileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            LogFileSystemWatcher.EnableRaisingEvents = false;
            //Debugger.Launch();
            //WriteAFile.WriteFile(ReturnAppDataPath()/*Environment.GetFolderPath(Environment.SpecialFolder.Desktop)*/, "HizoAlgo1.txt", blah, false);
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {

                //WriteAFile.WriteFile(ReturnAppDataPath()/*Environment.GetFolderPath(Environment.SpecialFolder.Desktop)*/, "HizoAlgo2.txt", blah, false);
                if (!LogControl)
                {
                    LogControl = true;
                    Thread.Sleep(100);
                    //WriteAFile.WriteFile(ReturnAppDataPath()/*Environment.GetFolderPath(Environment.SpecialFolder.Desktop)*/, "HizoAlgo3.txt", blah, false);
                    string[] Lines = ReadAFile.ReadFile(ReturnAppDataPath(), "LastCallLog.txt");
                    string Data = "[LOG]";
                    
                    foreach (string line in Lines)
                    {
                        Data += line + "\n";
                    }

                    client.Send(Data);

                    Thread.Sleep(100);
                    
                    LogControl = false;
                    //WriteAFile.WriteFile(ReturnAppDataPath()/*Environment.GetFolderPath(Environment.SpecialFolder.Desktop)*/, "Salio.txt", blah, false);
                }
            }

            LogFileSystemWatcher.EnableRaisingEvents = true;
        }

        #region Client and Server Handlers
        
        #region Client Handlers
        void client_OnDataReceived(object Sender, ClientReceivedArguments R)
        {
            //Debugger.Break();
            string data = R.ReceivedData;

            if (data.StartsWith("[EXISTS]"))
            {
                WriteLog("Connection attempt was sucessful.", GetExplorerUser(false), "Connected");
                client.Send("[READYTORECIEVE]");
            }

            else if (data.StartsWith("[NOTEXISTS]"))
            {
                WriteLog("Attempted to connect, but user doesn't exists in database.", GetExplorerUser(false), "Couldn't connect");
                try
                {
                    if (CheckFile.CheckIfFileExists(FileDirectory, "SoftphoneParams.cfg"))
                        CheckFile.DeleteFile(FileDirectory, "SoftphoneParams.cfg");
                    if (CheckFile.CheckIfFileExists(FileDirectory, "Catalog.cfg"))
                        CheckFile.DeleteFile(FileDirectory, "Catalog.cfg");
                }
                catch (Exception)
                { }
            }

            else if (data.StartsWith("[USERPARAMS]"))
            {
                sipParameters.Clear();
                int indexId = data.IndexOf("[USERID]");
                int indexPass = data.IndexOf("[PASS]");
                int indexDId = data.IndexOf("[DISPLAYID]");
                int indexSIPServerip = data.IndexOf("[SIPSERVERIP]");
                int indexSIPServerPort = data.IndexOf("[SIPSERVERPORT]");

                this.Username = data.Substring(indexId + 8, indexPass - (indexId + 8));
                this.Password = data.Substring(indexPass + 6, indexDId - (indexPass + 6));
                this.ID = data.Substring(indexDId + 11, indexSIPServerip - (indexDId + 11));
                this.Hostname = data.Substring(indexSIPServerip + 13, indexSIPServerPort - (indexSIPServerip + 13));
                this.Port = data.Substring(indexSIPServerPort + 15);

                sipParameters.Add(data.Substring(indexId, indexPass - indexId));
                sipParameters.Add(data.Substring(indexDId, indexSIPServerip - indexDId));
                sipParameters.Add(data.Substring(indexPass, indexDId - indexPass));
                sipParameters.Add(data.Substring(indexSIPServerip, indexSIPServerPort - indexSIPServerip));
                sipParameters.Add(data.Substring(indexSIPServerPort));

                //////////////////////////////////////////
                //WriteAFile.WriteFile("C:\\Users\\" + this.user + "\\Desktop", "SOFONPARAMS.txt", sipParameters.ToArray(), false);
                //////////////////////////////////////////
                WriteAFile.WriteFile(FileDirectory, "SoftphoneParams.cfg", sipParameters.ToArray(), false);
            }

            else if(data.StartsWith("[CATALOG]"))
            {
                string[] catalog;
                char[] chrs = {','}; 
                data = data.Substring(9);
                catalog = data.Split(chrs);
                WriteAFile.WriteFile(FileDirectory, "Catalog.cfg", catalog, false);
            }
        }

        void client_OnClientFileSending(object Sender, ClientFileSendingArguments R)
        {
            //throw new NotImplementedException();
        }

        void client_OnClientError(object Sender, ClientErrorArguments R)
        {
            //Debugger.Launch();
            List<string> auxList = new List<string>();
            auxList.Add("---------------" + DateTime.Now + "---------------");
            auxList.Add(R.ErrorMessage);
            auxList.Add("");
            auxList.Add(R.Exception);
            auxList.Add("");
            auxList.Add(GetExplorerUser(true));
            auxList.Add("");
            //////////////////////////////////////////
            //WriteAFile.WriteFile("C:\\Users\\" + this.user + "\\Desktop", "ERROR.txt", auxList.ToArray(), false);
            WriteAFile.WriteFile(FileDirectory, "ERROR.txt", auxList.ToArray(), true);
            //////////////////////////////////////////

            try
            {
            }
            catch (Exception)
            { }
            finally
            {
                closeClient();
            }
            //int count = 9;
            //bool control = true;
            //while (control)
            //{
            //    try
            //    {
            //        startClient();
            //        //Thread.Sleep(150);
            //        control = false;
            //    }
            //    catch
            //    {
            //        control = true;
            //    }

            //    if (count-- < 0)
            //        control = false;
            //}
            //startClient();
            //StartTimerForReconnect();
            //StartReconnectThread();
            //reconnectThread.Start();
        }

        void client_OnClientDisconnected(object Sender, ClientDisconnectedArguments R)
        {
            
        }

        void client_OnClientConnecting(object Sender, ClientConnectingArguments R)
        {
            //throw new NotImplementedException();
        }

        void client_OnClientConnected(object Sender, ClientConnectedArguments R)
        {
            //////////////////////////////////////////
            //WriteAFile.WriteFile("C:\\Users\\" + this.user + "\\Desktop", "CONNECTED.txt", blah, false);
            //////////////////////////////////////////

            //Debugger.Break();
            client.Send("[CHECKAVAILABILITY]");
            //throw new NotImplementedException();
        }
        #endregion
        
        #region Server Handlers
        void server_OnServerError(object Sender, ErrorArguments R)
        {
            //throw new NotImplementedException();
        }

        void server_OnDataReceived(object Sender, ReceivedArguments R)
        {
            //Debugger.Break();
            string data = R.ReceivedData;

            try
            {
                if (data.StartsWith("[CHANGESERVERIPPORT]"))
                {
                    int indexA = data.IndexOf("[SERVERIP]");
                    int indexB = data.IndexOf("[PORT]");
                    this.serverIp = data.Substring(indexA + 10, indexB - (indexA + 10));
                    this.serverPort = data.Substring(indexB + 6);
                }

                /*
                else if (data.StartsWith("[CHANGESIPIPPORT]"))
                {
                    int indexA = data.IndexOf("[SERVERIP]");
                    int indexB = data.IndexOf("[PORT]");
                    this.Hostname = data.Substring(indexA + 10, indexB - (indexA + 10));
                    this.Port = data.Substring(indexB + 6);
                }*/

                else if (data.StartsWith("[REQUEST]"))
                {
                    server.SendTo(R.Name, "SERVER IP: " + serverIp + "\nSERVER PORT: " + serverPort);
                }
            }
            catch (IndexOutOfRangeException OOR)
            {
            }
        }

        void server_OnClientDisconnected(object Sender, DisconnectedArguments R)
        {
        }

        void server_OnClientConnected(object Sender, ConnectedArguments R)
        {          
        }
        #endregion
        #endregion
        #endregion

        #region Service Handlers
        protected override void OnStart(string[] args)
        {
            try
            {
                this.serverIp = Properties.Settings.Default.ServerIp;
                this.serverPort = Properties.Settings.Default.ServerPort;
            }
            catch (Exception e)
            {
                WriteAFile.WriteFile("C:\\Users\\" + GetExplorerUser(false) + "\\Desktop", "Error en parametros.txt", blah, false);
                EventLog.WriteEntry(e.Message + e.StackTrace); 
            }

            startServer();
        }

        protected override void OnStop()
        {
            Properties.Settings.Default.Save();
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            //Debugger.Launch();
            //Debugger.Break();
            Thread.Sleep(3000);
            this.user = GetExplorerUser(false);

            if(CheckFile.CheckIfFileExists(FileDirectory, "SoftphoneParams.cfg"))
                CheckFile.DeleteFile(FileDirectory, "SoftphoneParams.cfg");
            if (CheckFile.CheckIfFileExists(FileDirectory, "Catalog.cfg"))
                CheckFile.DeleteFile(FileDirectory, "Catalog.cfg");

            switch (changeDescription.Reason)
            {
                case SessionChangeReason.SessionLogon:
                    startClient();
                    StartFileWatcherForLogs();
                    StartTimerForReconnect();
                    //ReconnectControl = true;
                    //StartReconnectThread();
                    //reconnectThread.Start();                    
                    break;
                case SessionChangeReason.SessionUnlock:
                    break;
                case SessionChangeReason.SessionLogoff:
                    //ReconnectControl = false;
                    //reconnectThread.Abort();
                    timer.Stop();
                    closeClient();
                    CheckFile.DeleteFile(FileDirectory, "ERROR.txt");
                    break;
                case SessionChangeReason.SessionLock:
                    break;
                default:
                    break;
            }

            
        }
        #endregion        
    }    
}
