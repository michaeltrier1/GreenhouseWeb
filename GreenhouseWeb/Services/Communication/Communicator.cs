using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Web;

namespace GreenhouseWeb.Services.Communication
{
    public class Communicator
    {

        public void SendRetryConnection(string greenhouseConnectionInfo)
        {
            using (TcpClient client = new TcpClient()) {                 
                char c = ':';
                string[] ipAndPort = greenhouseConnectionInfo.Split(c);
                client.Connect(ipAndPort[0], int.Parse(ipAndPort[1]));
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);

                JObject jsonObject = new JObject("{}");
                jsonObject.Add("procedure", "retryConnection");

                writer.Write(jsonObject);
                writer.Flush();

                writer.Close();
                stream.Close();
            }



        }


    }
}