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
        private IPAddress clientIpAddress;
        private int clientPort;
        private IPAddress serverIpAddress;
        private int serverPort;

        private TcpListener listener;
        private Thread listeningThread;

        private Thread pettingThread;
        private bool petting;

        private Thread liveDataThread;
        public bool SendLiveData { get; set; }
        private bool sentNewLiveData;
        string internalTemperature;
        string externalTemperature;
        string humidity;
        string waterLevel;

        private Thread uploadDataThread;
        private bool uploadLiveData;

        public string ID { get; set; }
        public bool ReceivedSchedule { get; set; }
        public bool RecievedRetryConnection { get; set; }

        public ClientMock(String ip, int port)
        {
            this.clientIpAddress = IPAddress.Parse(ip);
            this.clientPort = port;
            this.listener = new TcpListener(clientIpAddress, port);

            this.serverIpAddress = IPAddress.Parse("127.0.0.1");
            this.serverPort = 8090;

            this.ID = "";
            this.ReceivedSchedule = false;
            RecievedRetryConnection = false;

            sentNewLiveData = false;
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
                            case "getLiveData":
                                IPAddress iPAddress = IPAddress.Parse((string)jsonMessage.GetValue("IPAddress"));
                                int port = (int)jsonMessage.GetValue("port");
                                this.sendLiveDataContinually(iPAddress, port);
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

        internal void pet()
        {
            TcpClient client = new TcpClient();
            
            client.Connect(serverIpAddress, serverPort);
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

        internal void petContinually()
        {
            pettingThread = new Thread(() =>
            {
                TcpClient client = new TcpClient();

                client.Connect(serverIpAddress, serverPort);
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                petting = true;

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

        internal void uploadDataContinually(string timeOfReading, string internalTemperature, float externalTemperature, float humidity, float waterlevel)
        {
            uploadDataThread = new Thread(() =>
            {
                TcpClient client = new TcpClient();

                client.Connect(serverIpAddress, serverPort);
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                uploadLiveData = true;

                while (uploadLiveData)
                {
                    JObject message = new JObject();
                    message.Add("procedure", "Datalog");
                    message.Add("greenhouseID", ID);
                    message.Add("time of Reading", timeOfReading);
                    message.Add("internal temperature", internalTemperature);
                    message.Add("external temperature", externalTemperature);
                    message.Add("humidity", humidity);
                    message.Add("waterlevel", waterlevel);

                    String messageString = message.ToString(Newtonsoft.Json.Formatting.None);
                    writer.WriteLine(messageString);
                    writer.Flush();

                    Thread.Sleep(1000);
                }

                writer.Close();
                stream.Close();
            });
            uploadDataThread.Name = "Live Data Thread";
            uploadDataThread.Start();
        }

        internal void sendLiveDataContinually(IPAddress iPAddress, int port)
        {
            liveDataThread = new Thread(() =>
            {
                TcpClient client = new TcpClient();

                client.Connect(iPAddress, port);
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                SendLiveData = true;

                while (SendLiveData)
                {
                    JObject message = new JObject();
                    message.Add("procedure", "live data");
                    message.Add("greenhouseID", ID);
                    message.Add("internal temperature", internalTemperature);
                    message.Add("external temperature", externalTemperature);
                    message.Add("humidity", humidity);
                    message.Add("water level", waterLevel);

                    String messageString = message.ToString(Newtonsoft.Json.Formatting.None);
                    writer.WriteLine(messageString);
                    writer.Flush();

                    sentNewLiveData = true;
                    Thread.Sleep(1000);
                }

                writer.Close();
                stream.Close();
            });
            liveDataThread.Name = "Live Data Thread";
            liveDataThread.Start();
        }

        internal void setMeasurements(string internalTemperature, string externalTemperature, string humidity, string waterLevel)
        {
            this.internalTemperature = internalTemperature;
            this.externalTemperature = externalTemperature;
            this.humidity = humidity;
            this.waterLevel = waterLevel;
        }

        internal bool isSentNewLiveData()
        {
            bool newData = this.sentNewLiveData;
            this.sentNewLiveData = false;
            return newData;
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

            if (liveDataThread != null)
            {
                SendLiveData = false;
                liveDataThread.Interrupt();
            }

            if (liveDataThread != null)
            {
                uploadLiveData = false;
                uploadDataThread.Interrupt();
            }
        }


    }

    
}
