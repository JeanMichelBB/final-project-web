using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PropertyRental.Models;

namespace PropertyRental.Controllers
{
    [Authorize(Roles = "Property Manager,Admin,Property Owner")]
    public class EventsController : Controller
    {
        private PropertyRentalDBEntities db = new PropertyRentalDBEntities();

        // GET: Events
        public ActionResult Index()
        {
            ViewBag.ActiveLink = "Events";
            var events = db.Events
                .Include(e => e.Apartment)  // Assuming "Apartment" is a navigation property
                .Include(e => e.EventType)
                .Include(e => e.PropertyManager)
                .Include(e => e.PropertyOwner);

            return View(events.ToList());
        }


        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Amenities");
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "EventTypeName");
            ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName");
            ViewBag.PropertyOwnerID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,PropertyManagerID,PropertyOwnerID,EventDescription,ApartmentID,Timestamp,EventTypeID")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Amenities", @event.ApartmentID);
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "EventTypeName", @event.EventTypeID);
            ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName", @event.PropertyManagerID);
            ViewBag.PropertyOwnerID = new SelectList(db.Users, "UserID", "FirstName", @event.PropertyOwnerID);
            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Amenities", @event.ApartmentID);
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "EventTypeName", @event.EventTypeID);
            ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName", @event.PropertyManagerID);
            ViewBag.PropertyOwnerID = new SelectList(db.Users, "UserID", "FirstName", @event.PropertyOwnerID);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,PropertyManagerID,PropertyOwnerID,EventDescription,ApartmentID,Timestamp,EventTypeID")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApartmentID = new SelectList(db.Apartments, "ApartmentID", "Amenities", @event.ApartmentID);
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "EventTypeName", @event.EventTypeID);
            ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName", @event.PropertyManagerID);
            ViewBag.PropertyOwnerID = new SelectList(db.Users, "UserID", "FirstName", @event.PropertyOwnerID);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
