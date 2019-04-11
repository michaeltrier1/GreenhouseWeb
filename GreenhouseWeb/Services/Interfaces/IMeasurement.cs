namespace GreenhouseWeb.Services.Interfaces
{
    public interface IMeasurement
    {
       double internalTemperature { get; }
       double externalTemperature { get; }
       double humidity { get; }
       double waterlevel { get; }
    }
}