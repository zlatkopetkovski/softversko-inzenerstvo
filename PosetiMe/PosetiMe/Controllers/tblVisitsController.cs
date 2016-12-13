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
    public class tblVisitsController : Controller
    {
        private DB_PosetiMeEntities db = new DB_PosetiMeEntities();

        // GET: tblVisits
        public ActionResult Index()
        {
            var tblVisits = db.tblVisits.Include(t => t.tblLocal).Include(t => t.tblUser);
            return View(tblVisits.ToList());
        }

        // GET: tblVisits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVisit tblVisit = db.tblVisits.Find(id);
            if (tblVisit == null)
            {
                return HttpNotFound();
            }
            return View(tblVisit);
        }

        // GET: tblVisits/Create
        public ActionResult Create()
        {
            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name");
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "UserName");
            return View();
        }

        // POST: tblVisits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_User,ID_Local,Date")] tblVisit tblVisit)
        {
            if (ModelState.IsValid)
            {
                db.tblVisits.Add(tblVisit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name", tblVisit.ID_Local);
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "UserName", tblVisit.ID_User);
            return View(tblVisit);
        }

        // GET: tblVisits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVisit tblVisit = db.tblVisits.Find(id);
            if (tblVisit == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name", tblVisit.ID_Local);
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "UserName", tblVisit.ID_User);
            return View(tblVisit);
        }

        // POST: tblVisits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_User,ID_Local,Date")] tblVisit tblVisit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblVisit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name", tblVisit.ID_Local);
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "UserName", tblVisit.ID_User);
            return View(tblVisit);
        }

        // GET: tblVisits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVisit tblVisit = db.tblVisits.Find(id);
            if (tblVisit == null)
            {
                return HttpNotFound();
            }
            return View(tblVisit);
        }

        // POST: tblVisits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblVisit tblVisit = db.tblVisits.Find(id);
            db.tblVisits.Remove(tblVisit);
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
