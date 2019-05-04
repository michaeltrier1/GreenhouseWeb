using GreenhouseWeb.Services.Incoming;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GreenhouseWeb.Tests.Mock
{
    class ClientMock
    {
        private IPAddress ipAddress;
        private int port;
        private TcpListener listener;
        private Thread listeningThread;
        private Thread pettingThread;
        private bool petting;

        public string ID { get; set; }
        public bool ReceivedSchedule { get; set; }
        public bool RecievedRetryConnection { get; set; }

        public ClientMock(String ip, int port)
        {
            this.ipAddress = IPAddress.Parse(ip);
            this.port = port;
            this.listener = new TcpListener(ipAddress, port);

            this.ID = "";
            this.ReceivedSchedule = false;
        }

        public void ListenForCommunication()
        {
            listeningThread = new Thread(() => {
                while (true)
                {
                    try
                    {
                        listener.Start();

                        TcpClient client = listener.AcceptTcpClient();

                        NetworkStream stream = client.GetStream();
                        StreamReader reader = new StreamReader(stream);
                        StreamWriter writer = new StreamWriter(stream);

                        string message = reader.ReadLine();
                        JObject jsonMessage = JObject.Parse(message);

                        string procedure = jsonMessage.GetValue("procedure").ToString();

                        switch (procedure) {
                            case "applySchedule":
                                ReceivedSchedule = true;
                                break;
                            case "retryConnection":
                                RecievedRetryConnection = true;
                                break;
                            default:
                                break;
                        }
                    }
                    catch (IOException e) { Console.Write(e.StackTrace); }
                    catch (InvalidOperationException e) { break; }
                    catch (SocketException e) { break; }
                }
            });
            listeningThread.Name = "ClientMock";
            listeningThread.Start();
        }

        internal void Stop()
        {
            if (listener != null)
            {
                this.listener.Stop();
            }

            if (pettingThread != null)
            {
                petting = false;
                pettingThread.Interrupt();
            } 
        }

        internal void pet(string greenhouseID)
        {
            TcpClient client = new TcpClient();
            
            client.Connect("127.0.0.1", 8090);
            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);

            JObject message = new JObject();
            message.Add("procedure", "petWatchdog");
            message.Add("greenhouseID", ID);

            String messageString = message.ToString(Newtonsoft.Json.Formatting.None);
            writer.WriteLine(messageString);
            writer.Flush();

            writer.Close();
            stream.Close();  
        }

        internal void petContinually(string greenhouseID)
        {
            pettingThread = new Thread(() =>
            {
                TcpClient client = new TcpClient();

                client.Connect("127.0.0.1", 8090);
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);

                while (petting)
                {
                    JObject message = new JObject();
                    message.Add("procedure", "petWatchdog");
                    message.Add("greenhouseID", ID);

                    String messageString = message.ToString(Newtonsoft.Json.Formatting.None);
                    writer.WriteLine(messageString);
                    writer.Flush();

                    Thread.Sleep(1000);
                }

                writer.Close();
                stream.Close();
            });
            pettingThread.Name = "Petting Thread";
            pettingThread.Start();
        }
    }

    
}
