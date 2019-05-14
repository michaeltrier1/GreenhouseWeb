namespace GreenhouseWeb.Services.Incoming
{
    public interface IIncomingCommunicator
    {
        void listenForConnections();
        void stopLiveData(string greenhouseID);
        void clear();
    }
}