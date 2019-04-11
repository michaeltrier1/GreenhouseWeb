using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace GreenhouseWeb.Services.Incoming
{
    public class SocketHandler
    {
        private TcpClient client;
        private IncomingCommunicator incomingCommunicator;
        private ProcedureInterpreter interpreter;
        private bool stopped;

        public SocketHandler(TcpClient client, IncomingCommunicator incomingCommunicator)
        {
            this.interpreter = new ProcedureInterpreter();
            this.client = client;
            this.incomingCommunicator = incomingCommunicator;
            this.stopped = false;
        }

        public void handleSocket()
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);

            bool registered = false;
            string registerID = "just once";
            while (registerID.Length != 0 && !stopped)
            {
                string message = reader.ReadLine();
                registerID = this.interpreter.interpret(message, this.incomingCommunicator);

                if (!registered && registerID.Length != 0)
                {
                    this.incomingCommunicator.registerSocketHandler(registerID, this);
                    registered = true;
                }
                    
            }

            if (registered)
            {
                this.incomingCommunicator.unregisterSocketHandler(registerID);
            }
        }

        public void stop()
        {
            this.stopped = true;
        }


    }

   


}