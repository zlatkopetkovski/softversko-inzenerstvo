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
    public class tblCommentsController : Controller
    {
        private DB_PosetiMeEntities db = new DB_PosetiMeEntities();

        // GET: tblComments
        public ActionResult Index()
        {
            var tblComments = db.tblComments.Include(t => t.tblLocal).Include(t => t.tblUser);
            return View(tblComments.ToList());
        }

        // GET: tblComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblComment tblComment = db.tblComments.Find(id);
            if (tblComment == null)
            {
                return HttpNotFound();
            }
            return View(tblComment);
        }

        // GET: tblComments/Create
        public ActionResult Create()
        {
            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name");
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "UserName");
            return View();
        }

        // POST: tblComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_User,ID_Local,Comment")] tblComment tblComment)
        {
            if (ModelState.IsValid)
            {
                db.tblComments.Add(tblComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name", tblComment.ID_Local);
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "UserName", tblComment.ID_User);
            return View(tblComment);
        }

        // GET: tblComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblComment tblComment = db.tblComments.Find(id);
            if (tblComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name", tblComment.ID_Local);
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "UserName", tblComment.ID_User);
            return View(tblComment);
        }

        // POST: tblComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_User,ID_Local,Comment")] tblComment tblComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name", tblComment.ID_Local);
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "UserName", tblComment.ID_User);
            return View(tblComment);
        }

        // GET: tblComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblComment tblComment = db.tblComments.Find(id);
            if (tblComment == null)
            {
                return HttpNotFound();
            }
            return View(tblComment);
        }

        // POST: tblComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblComment tblComment = db.tblComments.Find(id);
            db.tblComments.Remove(tblComment);
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
