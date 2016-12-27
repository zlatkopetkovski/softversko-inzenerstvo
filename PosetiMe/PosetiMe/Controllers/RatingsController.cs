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
            string idU = User.Identity.GetUserId();
            tblRating.ID_User = idU;
            int idL = Convert.ToInt32(TempData["idL"]);//податокот се зема од VisitsController
            tblRating.ID_Local = idL;
            TempData["idL"] = idL;//проследување на ID од штотуку внесениот (и рејтувн) локал до контролетрот Comments

            try
            {
                var validateNull = (from a in db.tblRatings
                                    where a.ID_User == idU
                                    && a.ID_Local == idL
                                    select a).First(); //проверва дали веќе од корисникот постои рејтинг за локалот
                validateNull.Rate = tblRating.Rate;//го додава новиот рејтинг
                db.SaveChanges();//ако не постои рејтинг од корисникот за локалот се префрлува во catch
                
                return RedirectToAction("Create", "Comments");
            }

            catch
            {
                if (ModelState.IsValid)
                {
                    db.tblRatings.Add(tblRating); //се додава нов запис со податоците од tblRating локалната променлива
                    db.SaveChanges();
                    return RedirectToAction("Create", "Comments");
                }
            }

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
