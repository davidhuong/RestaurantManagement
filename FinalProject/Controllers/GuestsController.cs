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
    public class GuestsController : Controller
    {
        private RestaurantDB db = new RestaurantDB();

        // GET: Guests
        public ActionResult Index()
        {
            var tblGuests = db.tblGuests.Include(t => t.tblUsedTable);
            return View(tblGuests.ToList());
        }

        // GET: Guests/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGuest tblGuest = db.tblGuests.Find(id);
            if (tblGuest == null)
            {
                return HttpNotFound();
            }
            return View(tblGuest);
        }

        // GET: Guests/Create
        public ActionResult Create()
        {
            ViewBag.table_num = new SelectList(db.tblUnusedTables, "table_num", "description");
            return View();
        }

        // POST: Guests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "guest_phone_number,guest_name,time_arrived,num_of_guests,table_num,reservation")] tblGuest tblGuest)
        {
            if (ModelState.IsValid)
            {
                tblUnusedTable table = db.tblUnusedTables.Find(tblGuest.table_num);
                tblUsedTable usedTable = new tblUsedTable();
                usedTable.table_num = table.table_num;
                usedTable.description = table.description;
                //add used table
                db.tblUsedTables.Add(usedTable);
                //remove from available tables
                db.tblUnusedTables.Remove(table);
                db.tblGuests.Add(tblGuest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.table_num = new SelectList(db.tblUnusedTables, "table_num", "description", tblGuest.table_num);
            return View(tblGuest);
        }

        // GET: Guests/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGuest tblGuest = db.tblGuests.Find(id);
            if (tblGuest == null)
            {
                return HttpNotFound();
            }
            ViewBag.table_num = new SelectList(db.tblUnusedTables, "table_num", "description", tblGuest.table_num);
            return View(tblGuest);
        }

        // POST: Guests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "guest_phone_number,guest_name,time_arrived,num_of_guests,table_num,reservation")] tblGuest tblGuest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGuest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.table_num = new SelectList(db.tblUnusedTables, "table_num", "description", tblGuest.table_num);
            return View(tblGuest);
        }

        // GET: Guests/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGuest tblGuest = db.tblGuests.Find(id);
            if (tblGuest == null)
            {
                return HttpNotFound();
            }
            return View(tblGuest);
        }

        // POST: Guests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tblGuest tblGuest = db.tblGuests.Find(id);
            tblUsedTable table = db.tblUsedTables.Find(tblGuest.table_num);
            tblUnusedTable unusedTable = new tblUnusedTable();
            unusedTable.table_num = table.table_num;
            unusedTable.description = table.description;
            db.tblUnusedTables.Add(unusedTable);
            db.tblUsedTables.Remove(table);
            db.tblGuests.Remove(tblGuest);
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
