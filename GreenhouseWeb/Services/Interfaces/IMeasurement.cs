namespace GreenhouseWeb.Services.Interfaces
{
    public interface IMeasurement
    {
       double InternalTemperature { get; }
       double ExternalTemperature { get; }
       double Humidity { get; }
       double Waterlevel { get; }
    }
}