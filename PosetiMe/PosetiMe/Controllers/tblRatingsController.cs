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
    public class tblRatingsController : Controller
    {
        private DB_PosetiMeEntities db = new DB_PosetiMeEntities();

        // GET: tblRatings
        public ActionResult Index()
        {
            var tblRatings = db.tblRatings.Include(t => t.tblLocal).Include(t => t.tblUser);
            return View(tblRatings.ToList());
        }

        // GET: tblRatings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRating tblRating = db.tblRatings.Find(id);
            if (tblRating == null)
            {
                return HttpNotFound();
            }
            return View(tblRating);
        }

        // GET: tblRatings/Create
        public ActionResult Create()
        {
            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name");
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "UserName");
            return View();
        }

        // POST: tblRatings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_User,ID_Local,Rate")] tblRating tblRating)
        {
            if (ModelState.IsValid)
            {
                db.tblRatings.Add(tblRating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name", tblRating.ID_Local);
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "UserName", tblRating.ID_User);
            return View(tblRating);
        }

        // GET: tblRatings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRating tblRating = db.tblRatings.Find(id);
            if (tblRating == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name", tblRating.ID_Local);
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "UserName", tblRating.ID_User);
            return View(tblRating);
        }

        // POST: tblRatings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_User,ID_Local,Rate")] tblRating tblRating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblRating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name", tblRating.ID_Local);
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "UserName", tblRating.ID_User);
            return View(tblRating);
        }

        // GET: tblRatings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRating tblRating = db.tblRatings.Find(id);
            if (tblRating == null)
            {
                return HttpNotFound();
            }
            return View(tblRating);
        }

        // POST: tblRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblRating tblRating = db.tblRatings.Find(id);
            db.tblRatings.Remove(tblRating);
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
