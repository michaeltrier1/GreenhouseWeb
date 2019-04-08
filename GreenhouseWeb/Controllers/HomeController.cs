using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreenhouseWeb.Models;


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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Whatever = "I made this";

            return View();
        }

        public ActionResult ViewLiveData()
        {
            ViewLiveDataMock mock = new ViewLiveDataMock();
             double temp =  mock.getTemperature();
            return View(temp);
        }



    }
}