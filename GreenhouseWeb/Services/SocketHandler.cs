using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Web;

namespace GreenhouseWeb.Services
{
    public class SocketHandler
    {

        private TcpClient client;

        public SocketHandler(TcpClient client)
        {
            this.client = client;
        }

        public void handleSocketAsync()
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            String message = reader.ReadLine();


            System.Diagnostics.Debug.WriteLine(message);
            for (int i=0; i<5; i++)
            {
                System.Diagnostics.Debug.WriteLine("Mjallo");
                Thread.Sleep(1000);
            }
            
        }


    }

   


}