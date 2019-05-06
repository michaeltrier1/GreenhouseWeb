using Newtonsoft.Json.Linq;
using System;

namespace GreenhouseWeb.Services
{
    internal class ScheduleRepacker
    {

        internal JObject repackage(JObject rawSchedule)
        {
            JObject schedule = new JObject();
            
            //get raw data to readable format
            JArray data = (JArray)rawSchedule.GetValue("data");
            
            //get number of days
            int numberOfDays = 1;
            
            for (int dayNumber = 0; dayNumber < numberOfDays; dayNumber++)
            {
                JObject day = new JObject();

                for (int blockNumber = 1; blockNumber < 13; blockNumber++)
                {
                    JObject setpoints = new JObject();
                    JArray blockData = (JArray)data[blockNumber-1];

                    double blueLight = (double)blockData[1];
                    double redLight = (double)blockData[2];
                    double temperature = (double)blockData[3];
                    double humidity = (double)blockData[4];
                    double waterlevel = (double)blockData[5];

                    //insert Data
                    setpoints.Add("temperature", temperature);
                    setpoints.Add("humidity", humidity);
                    setpoints.Add("waterlevel", waterlevel);
                    setpoints.Add("light_blue", blueLight);
                    setpoints.Add("light_red", redLight);

                    day.Add("block"+blockNumber, setpoints);
                }

                schedule.Add("day"+dayNumber, day);
                
            }
            
            schedule.Add("days", numberOfDays);
            return schedule;
        }

    }
}