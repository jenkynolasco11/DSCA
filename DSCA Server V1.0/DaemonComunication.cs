using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaDLL
{
    class DaemonComunication
    {
        //private const string 

        public static bool ExistsAccount(string User)
        {
            //if(

            return false;
        }

        private void receiveData(Object networkStream)
        {
            NetworkStream ns = (NetworkStream)networkStream;
            StreamReader stream = new StreamReader(ns);
            string dataReceived = "";
            string dataLine = "";

            try
            {
                while (true)
                {
                    while ((dataLine = stream.ReadLine()) != null)
                    {
                        dataReceived += dataLine;
                    }
                    /////////////// Poner Propiedad que dispara el evento //////////////////////
                    checkData(dataReceived);
                    dataReceived = "";
                    //sendData(ns, "[FINALLE]");
                    //sendControl = false;
                }
            }
            catch (Exception e)
            {
                ns.Close();
                stream.Close();
            }
        }

        private void sendData(NetworkStream ns, string data)
        {
            StreamWriter sw = new StreamWriter(ns);
            try
            {
                string[] dataSplited = data.Split('\n');

                foreach (string dataToSend in dataSplited)
                {
                    sw.WriteLine(dataToSend);
                }

                sw.Flush();
            }
            catch (Exception e)
            {
                sw.Close();   
            }
        }

        private void checkData(string data)
        {
        }
    }
}