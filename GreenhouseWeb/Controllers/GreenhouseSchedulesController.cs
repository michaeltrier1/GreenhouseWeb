using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GreenhouseWeb.Models;

namespace GreenhouseWeb.Controllers
{
    public class GreenhouseSchedulesController : Controller
    {
        private GreenhouseDBContext db = new GreenhouseDBContext();

        // GET: GreenhouseSchedules
        public ActionResult Index()
        {
            return View(db.GreenhouseSchedules.ToList());
        }

        // GET: GreenhouseSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GreenhouseSchedule greenhouseSchedule = db.GreenhouseSchedules.Find(id);
            if (greenhouseSchedule == null)
            {
                return HttpNotFound();
            }
            return View(greenhouseSchedule);
        }

        // GET: GreenhouseSchedules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GreenhouseSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GreenhouseID,ScheduleID,StartDate")] GreenhouseSchedule greenhouseSchedule)
        {
            if (ModelState.IsValid)
            {
                db.GreenhouseSchedules.Add(greenhouseSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(greenhouseSchedule);
        }

        // GET: GreenhouseSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GreenhouseSchedule greenhouseSchedule = db.GreenhouseSchedules.Find(id);
            if (greenhouseSchedule == null)
            {
                return HttpNotFound();
            }
            return View(greenhouseSchedule);
        }

        // POST: GreenhouseSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GreenhouseID,ScheduleID,StartDate")] GreenhouseSchedule greenhouseSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(greenhouseSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(greenhouseSchedule);
        }

        // GET: GreenhouseSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GreenhouseSchedule greenhouseSchedule = db.GreenhouseSchedules.Find(id);
            if (greenhouseSchedule == null)
            {
                return HttpNotFound();
            }
            return View(greenhouseSchedule);
        }

        // POST: GreenhouseSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GreenhouseSchedule greenhouseSchedule = db.GreenhouseSchedules.Find(id);
            db.GreenhouseSchedules.Remove(greenhouseSchedule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
