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
            JObject jsonObject = new JObject("{}");
            jsonObject.Add("procedure", "retryConnection");

            this.send(greenhouseConnectionInfo, jsonObject);
        }

        public void applySchedule(string greenhouseConnectionInfo, JObject schedule)
        {
            schedule.Add("procedure", "applySchedule");
            this.send(greenhouseConnectionInfo, schedule);
        }

        private void send(string greenhouseConnectionInfo, JObject message)
        {
            char c = ':';
            string[] ipAndPort = greenhouseConnectionInfo.Split(c);

            using (TcpClient client = new TcpClient())
            {
                client.Connect(ipAndPort[0], int.Parse(ipAndPort[1]));
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);

                writer.Write(message);
                writer.Flush();

                writer.Close();
                stream.Close();
            }
        }

        public void getLiveData(string greenhouseConnectionInfo, JObject message)
        {

            this.send(greenhouseConnectionInfo, message);
        }



    }
}