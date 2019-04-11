using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Web;

namespace GreenhouseWeb.Services.Incoming
{
    public class SocketHandler
    {

        private TcpClient client;
        private IncomingCommunicator incomingCommunicator;
        private ProcedureInterpreter interpreter;

        public SocketHandler(TcpClient client, IncomingCommunicator incomingCommunicator)
        {
            this.interpreter = new ProcedureInterpreter();
            this.client = client;
            this.incomingCommunicator = incomingCommunicator;
        }

        public void handleSocket()
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            String message = reader.ReadLine();

            this.interpreter.interpret(message, this.incomingCommunicator);
        }


    }

   


}