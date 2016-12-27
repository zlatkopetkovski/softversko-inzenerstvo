using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PosetiMe.Models;
using Microsoft.AspNet.Identity;

namespace PosetiMe.Controllers
{
    public class RatingsController : Controller
    {
        private DBPosetiMeEntities db = new DBPosetiMeEntities();

        // GET: Ratings
        public ActionResult Index()
        {
            var tblRatings = db.tblRatings.Include(t => t.tblLocal).Include(t => t.tblUser);
            return View(tblRatings.ToList());
        }

        // GET: Ratings/Details/5
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

        // GET: Ratings/Create
        public ActionResult Create()
        {
            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name");
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "ID");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_User,ID_Local,Rate")] tblRating tblRating)
        {
            //додавање на моето ид и ид од последниот локал кој сум го посетил
            tblRating.ID_User = User.Identity.GetUserId();
            int idL = Convert.ToInt32(TempData["idL"]);//податокот се зема од VisitsController
            tblRating.ID_Local = idL;
            if (tblRating.Rate >= 1 & tblRating.Rate <= 5)//рејтинг дозволен од 1-5
            {
                try
                {
                    var validateNull = (from a in db.tblRatings
                                        where a.ID_User == User.Identity.GetUserId()
                                        && a.ID_Local == idL
                                        select a).First();
                    validateNull.Rate = tblRating.Rate;
                    db.SaveChanges();
                }

                catch
                {
                    if (ModelState.IsValid)
                    {
                        db.tblRatings.Add(tblRating);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                
                
            }
            //if (ModelState.IsValid)
            //{
            //    db.tblRatings.Add(tblRating);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name", tblRating.ID_Local);
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "ID", tblRating.ID_User);
            return View(tblRating);
        }

        // GET: Ratings/Edit/5
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
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "ID", tblRating.ID_User);
            return View(tblRating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_User,ID_Local,Rate")] tblRating tblRating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblRating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name", tblRating.ID_Local);
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "ID", tblRating.ID_User);
            return View(tblRating);
        }

        // GET: Ratings/Delete/5
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

        // POST: Ratings/Delete/5
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
