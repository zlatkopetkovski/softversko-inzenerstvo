using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PosetiMe.Models;

namespace PosetiMe.Controllers
{
    public class CitiesController : Controller
    {
        private DBPosetiMeEntities db = new DBPosetiMeEntities();

        // GET: Cities
        public ActionResult Index()
        {
            return View(db.tblCities.ToList());
        }

        // GET: Cities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCity tblCity = db.tblCities.Find(id);
            if (tblCity == null)
            {
                return HttpNotFound();
            }
            return View(tblCity);
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] tblCity tblCity)
        {
            if (ModelState.IsValid)
            {
                db.tblCities.Add(tblCity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblCity);
        }

        // GET: Cities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCity tblCity = db.tblCities.Find(id);
            if (tblCity == null)
            {
                return HttpNotFound();
            }
            return View(tblCity);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] tblCity tblCity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblCity);
        }

        // GET: Cities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCity tblCity = db.tblCities.Find(id);
            if (tblCity == null)
            {
                return HttpNotFound();
            }
            return View(tblCity);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCity tblCity = db.tblCities.Find(id);
            db.tblCities.Remove(tblCity);
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
