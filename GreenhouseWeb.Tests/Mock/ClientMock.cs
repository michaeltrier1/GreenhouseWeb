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

        public string ID { get; set; }
        public bool ReceivedSchedule { get; set; }


        ///
        ///private Dictionary<string, SocketHandler> activeHandlers;

        public ClientMock()
        {
            this.ipAddress = IPAddress.Parse("127.0.0.1"); //TODO set right ip
            this.port = 8070;
            this.listener = new TcpListener(ipAddress, port);
            /// 
            /// this.servicesFacade = servicesFacade;
            /// this.activeHandlers = new Dictionary<string, SocketHandler>();

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

                        //SocketHandler socketHandler = new SocketHandler(client, this);

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
                            default:
                                break;
                        }

                        /// handler
                        ///Thread thread = new Thread(new ThreadStart(socketHandler.handleSocket));
                        ///thread.Start();
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
            this.listener.Stop();
        }
    }

    
}
