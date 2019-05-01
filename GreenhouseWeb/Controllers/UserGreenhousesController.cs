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
    public class UserGreenhousesController : Controller
    {
        private GreenhouseDBContext db = new GreenhouseDBContext();

        // GET: UserGreenhouses
        public ActionResult Index()
        {
            return View(db.UserGreenhouses.ToList());
        }

        // GET: UserGreenhouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGreenhouse userGreenhouse = db.UserGreenhouses.Find(id);
            if (userGreenhouse == null)
            {
                return HttpNotFound();
            }
            return View(userGreenhouse);
        }

        // GET: UserGreenhouses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserGreenhouses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,User,GreenhouseID")] UserGreenhouse userGreenhouse)
        {
            if (ModelState.IsValid)
            {
                db.UserGreenhouses.Add(userGreenhouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userGreenhouse);
        }

        // GET: UserGreenhouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGreenhouse userGreenhouse = db.UserGreenhouses.Find(id);
            if (userGreenhouse == null)
            {
                return HttpNotFound();
            }
            return View(userGreenhouse);
        }

        // POST: UserGreenhouses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,User,GreenhouseID")] UserGreenhouse userGreenhouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userGreenhouse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userGreenhouse);
        }

        // GET: UserGreenhouses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserGreenhouse userGreenhouse = db.UserGreenhouses.Find(id);
            if (userGreenhouse == null)
            {
                return HttpNotFound();
            }
            return View(userGreenhouse);
        }

        // POST: UserGreenhouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserGreenhouse userGreenhouse = db.UserGreenhouses.Find(id);
            db.UserGreenhouses.Remove(userGreenhouse);
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
