﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreenhouseWeb.Models;
using GreenhouseWeb.Services;
using GreenhouseWeb.Services.Interfaces;
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

        public ActionResult Schedule()
        {
            ViewBag.Message = "Fill out the tabel below or load a premade schedule";

            return View();
        }
        
        [HttpGet]
        public JsonResult getNewestData()
        {
            return Json(new { internalTemperature = new Random().NextDouble() * 100, externalTemperature = new Random().NextDouble() * 100, humidity = new Random().NextDouble() * 100, waterlevel = new Random().NextDouble() * 100 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewLiveData()
        {
            IMeasurement imeasurement = ServiceFacadeGetter.getInstance().getFacade().getCurrentLiveData("testgreenhouse");
           
            return View();
        }

    }
}