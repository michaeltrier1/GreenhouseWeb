using Newtonsoft.Json.Linq;
using System;

namespace GreenhouseWeb.Services
{
    internal class ScheduleRepacker
    {

        internal JObject repackage(JObject rawSchedule)
        {
            JObject schedule = new JObject("{}");

            //get raw data to readable format
            //get number of days
            int numberOfDays = 1;


            for (int dayNumber = 0; dayNumber < numberOfDays; dayNumber++)
            {
                JObject day = new JObject("{}");
                /*
                for (int blockNumber = 1; blockNumber =< 13; blockNumber++)
                {
                    JObject setpoints = new JObject("{}");

                    //insert Data
                    setpoints.Add("temperature", );
                    setpoints.Add("humidity", );
                    setpoints.Add("waterlevel", );
                    setpoints.Add("light_blue", );
                    setpoints.Add("light_red", );

                    day.Add("block"+blockNumber, );
                }

                schedule.Add("day"+dayNumber, );
                */
            }

            schedule.Add("days", numberOfDays);
            //return schedule;
            throw new NotImplementedException();
        }

    }
}