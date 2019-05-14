using GreenhouseWeb.Services.Interfaces;

namespace GreenhouseWeb.Services
{
    public interface ILiveDataFacade
    {
        IMeasurement getMeasurements(string greenhouseID);
        void setMeasurements(string greenhouseID, IMeasurement measurement);
        void stopLiveData(string greenhouseID);
    }
}