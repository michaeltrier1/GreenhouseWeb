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
    public class DatalogsController : Controller
    {
        private GreenhouseDBContext db = new GreenhouseDBContext();

        // GET: Datalogs
        public ActionResult Index()
        {
            return View(db.Datalogs.ToList());
        }

        // GET: Datalogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Datalog datalog = db.Datalogs.Find(id);
            if (datalog == null)
            {
                return HttpNotFound();
            }
            return View(datalog);
        }

        // GET: Datalogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Datalogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Greenhouse_ID,TimeOfReading,InternalTemperature,ExternalTemperature,Humidity,Waterlevel")] Datalog datalog)
        {
            if (ModelState.IsValid)
            {
                db.Datalogs.Add(datalog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(datalog);
        }

        // GET: Datalogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Datalog datalog = db.Datalogs.Find(id);
            if (datalog == null)
            {
                return HttpNotFound();
            }
            return View(datalog);
        }

        // POST: Datalogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Greenhouse_ID,TimeOfReading,InternalTemperature,ExternalTemperature,Humidity,Waterlevel")] Datalog datalog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datalog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(datalog);
        }

        // GET: Datalogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Datalog datalog = db.Datalogs.Find(id);
            if (datalog == null)
            {
                return HttpNotFound();
            }
            return View(datalog);
        }

        // POST: Datalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Datalog datalog = db.Datalogs.Find(id);
            db.Datalogs.Remove(datalog);
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
