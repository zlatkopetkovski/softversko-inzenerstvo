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
    public class LocalsController : Controller
    {
        private DBPosetiMeEntities db = new DBPosetiMeEntities();

        // GET: Locals
        public ActionResult Index()
        {
            var tblLocals = db.tblLocals.Include(t => t.tblCity);
            return View(tblLocals.ToList());
        }

        // GET: Locals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLocal tblLocal = db.tblLocals.Find(id);
            if (tblLocal == null)
            {
                return HttpNotFound();
            }
            return View(tblLocal);
        }

        // GET: Locals/Create
        public ActionResult Create()
        {
            ViewBag.ID_City = new SelectList(db.tblCities, "ID", "Name");
            return View();
        }

        // POST: Locals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,ID_City,Location,Phone,Phone2,Imege")] tblLocal tblLocal)
        {
            if (ModelState.IsValid)
            {
                db.tblLocals.Add(tblLocal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_City = new SelectList(db.tblCities, "ID", "Name", tblLocal.ID_City);
            return View(tblLocal);
        }

        // GET: Locals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLocal tblLocal = db.tblLocals.Find(id);
            if (tblLocal == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_City = new SelectList(db.tblCities, "ID", "Name", tblLocal.ID_City);
            return View(tblLocal);
        }

        // POST: Locals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,ID_City,Location,Phone,Phone2,Imege")] tblLocal tblLocal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblLocal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_City = new SelectList(db.tblCities, "ID", "Name", tblLocal.ID_City);
            return View(tblLocal);
        }

        // GET: Locals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblLocal tblLocal = db.tblLocals.Find(id);
            if (tblLocal == null)
            {
                return HttpNotFound();
            }
            return View(tblLocal);
        }

        // POST: Locals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblLocal tblLocal = db.tblLocals.Find(id);
            db.tblLocals.Remove(tblLocal);
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
