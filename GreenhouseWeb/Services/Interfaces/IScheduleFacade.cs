using Newtonsoft.Json.Linq;

namespace GreenhouseWeb.Services.Schedule
{
    public interface IScheduleFacade
    {
        JObject repackage(JObject rawSchedule);
    }
}