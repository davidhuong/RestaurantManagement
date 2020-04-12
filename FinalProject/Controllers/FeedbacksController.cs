using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class FeedbacksController : Controller
    {
        private RestaurantDB db = new RestaurantDB();

        // GET: Feedbacks
        public ActionResult Index()
        {
            var tblGuestFeedbacks = db.tblGuestFeedbacks.Include(t => t.tblGuest);
            return View(tblGuestFeedbacks.ToList());
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGuestFeedback tblGuestFeedback = db.tblGuestFeedbacks.Find(id);
            if (tblGuestFeedback == null)
            {
                return HttpNotFound();
            }
            return View(tblGuestFeedback);
        }

        // GET: Feedbacks/Create
        public ActionResult Create()
        {
            ViewBag.guest_phone_number = new SelectList(db.tblGuests, "guest_phone_number", "guest_name");
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "feedback_id,guest_phone_number,feedback_content,date_submitted,guest_email")] tblGuestFeedback tblGuestFeedback)
        {
            if (ModelState.IsValid)
            {
                db.tblGuestFeedbacks.Add(tblGuestFeedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.guest_phone_number = new SelectList(db.tblGuests, "guest_phone_number", "guest_name", tblGuestFeedback.guest_phone_number);
            return View(tblGuestFeedback);
        }

        // GET: Feedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGuestFeedback tblGuestFeedback = db.tblGuestFeedbacks.Find(id);
            if (tblGuestFeedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.guest_phone_number = new SelectList(db.tblGuests, "guest_phone_number", "guest_name", tblGuestFeedback.guest_phone_number);
            return View(tblGuestFeedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "feedback_id,guest_phone_number,feedback_content,date_submitted,guest_email")] tblGuestFeedback tblGuestFeedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGuestFeedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.guest_phone_number = new SelectList(db.tblGuests, "guest_phone_number", "guest_name", tblGuestFeedback.guest_phone_number);
            return View(tblGuestFeedback);
        }

        // GET: Feedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGuestFeedback tblGuestFeedback = db.tblGuestFeedbacks.Find(id);
            if (tblGuestFeedback == null)
            {
                return HttpNotFound();
            }
            return View(tblGuestFeedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGuestFeedback tblGuestFeedback = db.tblGuestFeedbacks.Find(id);
            db.tblGuestFeedbacks.Remove(tblGuestFeedback);
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
