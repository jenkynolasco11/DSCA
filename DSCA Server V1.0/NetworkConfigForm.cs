using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;

namespace PruebaDLL
{
    public partial class NetworkConfigForm : Form
    {
        #region Variables
        private List<object> interfaceList = new List<object>();
        private string interfaceIpAddress = "";
        private string listeningPort = "";
        private IPAddress address;
        private int sipport;
        private int serverport;
        #endregion
        #region Properties
        public string ServerPort
        {
            get { return this.serverPortTextBox.Text; }
        }
        public string SipPort
        {
            get { return this.sipServerPortTextBox.Text; }
        }
        public string ServerIP
        {
            get { return this.interfaceIpAddress; }
        }
        public string SipIP
        {
            get { return this.sipServerIpTextBox.Text; }
        }
        #endregion
        #region Constructor
        public NetworkConfigForm()
        {
            InitializeComponent();
        }
        #endregion
        #region Callbacks
        private void ShowIntIPAdd_OnIndexChanged(object sender, EventArgs e)
        {
            if (((NetworkInterface)interfaceList[interfacesComboBox.SelectedIndex]).GetIPProperties().UnicastAddresses.Count > 1)
                interfaceIpAddress = ((NetworkInterface)interfaceList[interfacesComboBox.SelectedIndex]).GetIPProperties().UnicastAddresses[1].Address.ToString();
            else
                interfaceIpAddress = ((NetworkInterface)interfaceList[interfacesComboBox.SelectedIndex]).GetIPProperties().UnicastAddresses[0].Address.ToString();

            try
            {
                serverIpLabel.Text = interfaceIpAddress;
            }
            catch (Exception Ex) { }
        }

        private void NetworkConfigForm_OnLoad(object sender, EventArgs e)
        {
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface netInterface in interfaces)
            {
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet || netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
                    || netInterface.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet || netInterface.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                {
                    interfacesComboBox.Items.Add(netInterface.Name);
                    interfaceList.Add(netInterface);
                }
            }
            interfacesComboBox.SelectedIndex = 0;
        }

        private void OkButton_OnClick(object sender, EventArgs e)
        {
            if (this.serverPortTextBox.Text == "" || this.sipServerIpTextBox.Text == "" || this.sipServerPortTextBox.Text == "")
            {
                MessageBox.Show("Los parametros de red no deben estar en blanco.\nPor favor configure", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if(!IPAddress.TryParse(this.sipServerIpTextBox.Text, out address))
            {
                MessageBox.Show("Por favor inserte una direccion valida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if(!int.TryParse(this.serverPortTextBox.Text, out serverport))
            {
                MessageBox.Show("Por favor inserte un numero de puerto valido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else if (!int.TryParse(this.sipServerPortTextBox.Text, out sipport))
            {
                MessageBox.Show("Por favor inserte un numero de puerto valido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //else
            //{

            //}
        }
        #endregion
    }
}
