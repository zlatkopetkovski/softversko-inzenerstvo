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
    public class VisitsController : Controller
    {
        private DBPosetiMeEntities db = new DBPosetiMeEntities();

        // GET: Visits
        public ActionResult Index()
        {
            var tblVisits = db.tblVisits.Include(t => t.tblLocal).Include(t => t.tblUser);

            //служи да се провери во view дали одредената посета е од најавениот корисник 
            ViewBag.user = User.Identity.GetUserId();
                return View(tblVisits.ToList());
        }

        // GET: Visits/Details/5
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
            tblLocal tblLocal = db.tblLocals.Find(tblVisit.ID_Local);

            int idLocal = tblLocal.ID; // потребно ни е за прашалникот за коментарите и рејтингот
            string idUser = User.Identity.GetUserId(); //зачувување на IDUser во локална променлива кој ни е потребен за 
                                                       //наоѓање на коментарите

            //земање на сите гласови за оваа локација
            var getRate = from a in db.tblRatings
                          where a.ID_Local == idLocal
                          select a;

            //земање на сите коментари за локалот од одреден корисник
            var getComments = from c in db.tblComments
                              where c.ID_Local == idLocal && c.ID_User == idUser
                              select c;

            //пресметување на рејтингот за оваа локација
            float rateing = new int();
            foreach (tblRating r in getRate)
            {
                rateing += r.Rate;
            }
            rateing = rateing / getRate.Count();
            TempData["rateing"] = rateing;//се проследува пресметаниот рејтинг до View-то
            ViewBag.comments = getComments.ToList();//се проследуваат пронајдените коментари до view-то
            return View(tblLocal);
        }

        // GET: Visits/Create
        public ActionResult Create()
        {
            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name");
            //ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "ID");
            return View();
        }

        // POST: Visits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_User,ID_Local,Date")] tblVisit tblVisit)
        {
            tblVisit.ID_User = User.Identity.GetUserId();
            tblVisit.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.tblVisits.Add(tblVisit);
                db.SaveChanges();

                //проследување на ID од штотуку внесениот локал до контролетрот Ratings
                TempData["idL"] = tblVisit.ID_Local;
                return RedirectToAction("Create", "Ratings");
            }

            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name", tblVisit.ID_Local);
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "ID", tblVisit.ID_User);
            return RedirectToAction("Create", "Rating");
        }

        // GET: Visits/Edit/5
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
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "ID", tblVisit.ID_User);
            return View(tblVisit);
        }

        // POST: Visits/Edit/5
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
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "ID", tblVisit.ID_User);
            return View(tblVisit);
        }

        // GET: Visits/Delete/5
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

        // POST: Visits/Delete/5
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
