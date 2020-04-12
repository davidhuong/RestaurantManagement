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
    public class DeliveryOrdersController : Controller
    {
        private RestaurantDB db = new RestaurantDB();

        // GET: DeliveryOrders
        public ActionResult Index()
        {
            return View(db.tblDeliveryOrders.ToList());
        }

        public ActionResult SortByLatest()
        {
            var orders = from o in db.tblDeliveryOrders
                         orderby o.time_order_placed descending
                         select o;
            return View(orders);
        }

        public ActionResult SortByEarliest()
        {
            var orders = from o in db.tblDeliveryOrders
                         orderby o.time_order_placed ascending
                         select o;
            return View(orders);
        }

        // GET: DeliveryOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDeliveryOrder tblDeliveryOrder = db.tblDeliveryOrders.Find(id);
            if (tblDeliveryOrder == null)
            {
                return HttpNotFound();
            }
            return View(tblDeliveryOrder);
        }

        // GET: DeliveryOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliveryOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_num,special_instructions,time_order_placed,customer_address")] tblDeliveryOrder tblDeliveryOrder)
        {
            if (ModelState.IsValid)
            {
                db.tblDeliveryOrders.Add(tblDeliveryOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblDeliveryOrder);
        }

        // GET: DeliveryOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDeliveryOrder tblDeliveryOrder = db.tblDeliveryOrders.Find(id);
            if (tblDeliveryOrder == null)
            {
                return HttpNotFound();
            }
            return View(tblDeliveryOrder);
        }

        // POST: DeliveryOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_num,special_instructions,time_order_placed,customer_address")] tblDeliveryOrder tblDeliveryOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDeliveryOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblDeliveryOrder);
        }

        // GET: DeliveryOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDeliveryOrder tblDeliveryOrder = db.tblDeliveryOrders.Find(id);
            if (tblDeliveryOrder == null)
            {
                return HttpNotFound();
            }
            return View(tblDeliveryOrder);
        }

        // POST: DeliveryOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDeliveryOrder tblDeliveryOrder = db.tblDeliveryOrders.Find(id);
            db.tblDeliveryOrders.Remove(tblDeliveryOrder);
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
