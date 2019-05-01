using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreenhouseWeb.Models;
using GreenhouseWeb.Services;
using GreenhouseWeb.Services.Interfaces;
using GreenhouseWeb.Tests;
using GreenhouseWeb.Tests.Mock;
using Newtonsoft.Json.Linq;

namespace GreenhouseWeb.Controllers
{
    public class HomeController : Controller
    {
        private GreenhouseDBContext db = new GreenhouseDBContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Setup()
        {
            ViewBag.Message = "Select your greenhouse to the left and then apply a schedule";

            return View();
        }

        public ActionResult Schedule()
        {
            ViewBag.Message = "Fill out the tabel below or load a premade schedule";





            return View();
        }

        [HttpPost]
        public JsonResult saveSchedule(string rawSchedule, string scheduleID)
        {
            string scheduleId = scheduleID.Trim();
            if (scheduleId != "")
            {
                JObject scheduleJson = JObject.Parse(rawSchedule);


                //get raw data to readable format
                JArray data = (JArray)scheduleJson.GetValue("rawSchedule");

                ////get number of days
                int numberOfDays = 1;

                for (int dayNumber = 0; dayNumber < numberOfDays; dayNumber++)
                {


                    for (int blockNumber = 1; blockNumber < 13; blockNumber++)
                    {


                        JArray blockData = (JArray)data[blockNumber - 1];
                        Schedule schedule = new Schedule();
                        double blueLight = (double)blockData[1];
                        double redLight = (double)blockData[2];
                        double temperature = (double)blockData[3];
                        double humidity = (double)blockData[4];
                        double waterlevel = (double)blockData[5];

                        //insert Data
                        //setpoints.Add("temperature", temperature);
                        //setpoints.Add("humidity", humidity);
                        //setpoints.Add("waterlevel", waterlevel);
                        //setpoints.Add("light_blue", blueLight);
                        //setpoints.Add("light_red", redLight);
                        schedule.ScheduleID = scheduleId;
                        schedule.Blocknumber = blockNumber;
                        schedule.BlueLight = blueLight;
                        schedule.RedLight = redLight;
                        schedule.InternalTemperature = temperature;
                        schedule.Humidity = humidity;
                        schedule.WaterLevel = waterlevel;
                        if (ModelState.IsValid)
                        {
                            db.Schedules.Add(schedule);
                            db.SaveChanges();

                        }

                    }


                }

            }
            return Json(new { stuff = "success" }, JsonRequestBehavior.AllowGet);
       } 
                  
    [HttpGet]
        public JsonResult getNewestData(string GreenhouseID)
        {
            IMeasurement measurement = ServiceFacadeGetter.getInstance().getFacade().getCurrentLiveData(GreenhouseID);
            return Json(new { internalTemperature = measurement.InternalTemperature, externalTemperature = measurement.ExternalTemperature,
                humidity = measurement.Humidity, waterlevel = measurement.Waterlevel }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewLiveData()
        {
            IMeasurement imeasurement = ServiceFacadeGetter.getInstance().getFacade().getCurrentLiveData("testgreenhouse");
           
            return View();
        }

        public void StopLiveData(string ID)
        {
            ServiceFacadeGetter.getInstance().getFacade().stopLiveData(ID);

        }

    }
}