namespace GreenhouseWeb.Services.WatchdogModule
{
    public interface IWatchdogFacade
    {
        void PetWatchdog(string greenhouseID);
        void clear();
    }
}