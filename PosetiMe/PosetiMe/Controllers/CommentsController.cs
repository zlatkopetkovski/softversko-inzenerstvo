﻿using System;
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
    public class CommentsController : Controller
    {
        private DBPosetiMeEntities db = new DBPosetiMeEntities();

        // GET: Comments
        
        public ActionResult Index()
        {
            var tblComments = db.tblComments.Include(t => t.tblLocal).Include(t => t.tblUser);
            ViewBag.user = User.Identity.GetUserId();
           // ViewBag.local = id;
            return View(tblComments.ToList());
        }

        // GET: Comments/Details/5
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

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name");
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "ID");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_User,ID_Local,Comment")] tblComment tblComment)
        {
            tblComment.ID_User = User.Identity.GetUserId();
            int idL = Convert.ToInt32(TempData["idL"]);//податокот се зема од RatingsController
            tblComment.ID_Local = idL;
            tblComment.Date = DateTime.Now.Date;
            if (ModelState.IsValid)
            {
                db.tblComments.Add(tblComment);
                db.SaveChanges();
                return RedirectToAction("Index", "Visits");
            }

            ViewBag.ID_Local = new SelectList(db.tblLocals, "ID", "Name", tblComment.ID_Local);
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "ID", tblComment.ID_User);
            return View(tblComment);
        }

        // GET: Comments/Edit/5
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
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "ID", tblComment.ID_User);
            return View(tblComment);
        }

        // POST: Comments/Edit/5
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
            ViewBag.ID_User = new SelectList(db.tblUsers, "ID", "ID", tblComment.ID_User);
            return View(tblComment);
        }

        // GET: Comments/Delete/5
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

        // POST: Comments/Delete/5
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
