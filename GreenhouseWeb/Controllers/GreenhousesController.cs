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
    public class GreenhousesController : Controller
    {
        private GreenhouseDBContext db = new GreenhouseDBContext();

        // GET: Greenhouses
        public ActionResult Index()
        {
            return View(db.Greenhouses.ToList());
        }

        // GET: Greenhouses/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Greenhouse greenhouse = db.Greenhouses.Find(id);
            if (greenhouse == null)
            {
                return HttpNotFound();
            }
            return View(greenhouse);
        }

        // GET: Greenhouses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Greenhouses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GreenhouseID,Password,IP,Port")] Greenhouse greenhouse)
        {
            if (ModelState.IsValid)
            {
                db.Greenhouses.Add(greenhouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(greenhouse);
        }

        // GET: Greenhouses/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Greenhouse greenhouse = db.Greenhouses.Find(id);
            if (greenhouse == null)
            {
                return HttpNotFound();
            }
            return View(greenhouse);
        }

        // POST: Greenhouses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GreenhouseID,Password,IP,Port")] Greenhouse greenhouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(greenhouse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(greenhouse);
        }

        // GET: Greenhouses/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Greenhouse greenhouse = db.Greenhouses.Find(id);
            if (greenhouse == null)
            {
                return HttpNotFound();
            }
            return View(greenhouse);
        }

        // POST: Greenhouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Greenhouse greenhouse = db.Greenhouses.Find(id);
            db.Greenhouses.Remove(greenhouse);
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
