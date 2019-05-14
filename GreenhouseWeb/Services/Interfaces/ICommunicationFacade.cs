using Newtonsoft.Json.Linq;

namespace GreenhouseWeb.Services.Communication
{
    public interface ICommunicationFacade
    {
        void RetryConnection(string greenhouseID);
        void applySchedule(string greenhouseID, JObject processedSchedule);
        void getLiveData(string greenhouseID);
    }
}