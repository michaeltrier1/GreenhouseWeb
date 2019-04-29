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
using GreenhouseWeb.Services;

namespace GreenhouseWeb.Controllers
{
    public class HomeController : Controller
    {
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

    }
}