using System;

namespace GreenhouseWeb.Services.Interfaces
{
    public interface IMeasurement
    {
        Nullable<double> InternalTemperature { get; }
        Nullable<double> ExternalTemperature { get; }
        Nullable<double> Humidity { get; }
        Nullable<double> Waterlevel { get; }
    }
}