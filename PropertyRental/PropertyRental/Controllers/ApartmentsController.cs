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
    public class ApartmentsController : Controller
    {
        private PropertyRentalDBEntities db = new PropertyRentalDBEntities();

        // GET: Apartments
        [Authorize]
        public ActionResult Index()
        {
            var apartments = db.Apartments.Include(a => a.Address).Include(a => a.Building).Include(a => a.User).Include(a => a.Status);
            return View(apartments.ToList());
        }

        // GET: Apartments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        // GET: Apartments/Create
        [Authorize(Roles = "PropertyManager,Admin")]
        public ActionResult Create()
        {
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetName");
            ViewBag.BuildingID = new SelectList(db.Buildings, "BuildingID", "Amenities");
            ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName");
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName");
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="PropertyManager,Admin")]
        public ActionResult Create([Bind(Include = "ApartmentID,PropertyManagerID,AddressID,StatusID,BuildingID,NumberOfRooms,Amenities,Price,Floor,ConstructionYear,Area")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                db.Apartments.Add(apartment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetName", apartment.AddressID);
            ViewBag.BuildingID = new SelectList(db.Buildings, "BuildingID", "Amenities", apartment.BuildingID);
            ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName", apartment.PropertyManagerID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName", apartment.StatusID);
            return View(apartment);
        }

        // GET: Apartments/Edit/5
        [Authorize(Roles = "PropertyManager,Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetName", apartment.AddressID);
            ViewBag.BuildingID = new SelectList(db.Buildings, "BuildingID", "Amenities", apartment.BuildingID);
            ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName", apartment.PropertyManagerID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName", apartment.StatusID);
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="PropertyManager,Admin")]
        public ActionResult Edit([Bind(Include = "ApartmentID,PropertyManagerID,AddressID,StatusID,BuildingID,NumberOfRooms,Amenities,Price,Floor,ConstructionYear,Area")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apartment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetName", apartment.AddressID);
            ViewBag.BuildingID = new SelectList(db.Buildings, "BuildingID", "Amenities", apartment.BuildingID);
            ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName", apartment.PropertyManagerID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName", apartment.StatusID);
            return View(apartment);
        }

        // GET: Apartments/Delete/5
        [Authorize(Roles = "PropertyManager,Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="PropertyManager,Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Apartment apartment = db.Apartments.Find(id);
            db.Apartments.Remove(apartment);
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
