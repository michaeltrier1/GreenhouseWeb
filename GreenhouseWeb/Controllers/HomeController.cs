using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreenhouseWeb.Models;
using GreenhouseWeb.Services;
using GreenhouseWeb.Tests;
using GreenhouseWeb.Tests.Mock;

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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Whatever = "I made this";

            return View();
        }
        
        [HttpGet]
        public JsonResult getNewestData()
        {
            return Json(new { internalTemperature = new Random().NextDouble() * 100, externalTemperature = new Random().NextDouble() * 100, humidity = new Random().NextDouble() * 100, waterlevel = new Random().NextDouble() * 100 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewLiveData()
        {
            Servicesfacade sF = new Servicesfacade();
            //ViewBag.Measurements = sF.getCurrentLiveData("testgreenhouse");  
            Random rnd = new Random();
            ViewBag.Measurements = rnd.NextDouble()*100;

            return View();
        }



    }
}