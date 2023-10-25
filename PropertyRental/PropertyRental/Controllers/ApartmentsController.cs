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
            ViewBag.ActiveLink = "Home";
            IQueryable<Apartment> apartments;
            // Show only the apartments that are available to the Tenants
            if (User.IsInRole("Potential Tenant"))
            {
                apartments = db.Apartments.Include(a => a.Address).Include(a => a.Building).Include(a => a.User).Include(a => a.Status).Where(a => a.StatusID == 1);
            } else
            {
                apartments = db.Apartments.Include(a => a.Address).Include(a => a.Building).Include(a => a.User).Include(a => a.Status);
            }
            return View(apartments.ToList());
        }

        // GET: Apartments/Details/5
        [Authorize]
        public ActionResult Details(int? apartmentID)
        {
            if (apartmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Find(apartmentID);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        // GET: Apartments/Create
        [Authorize(Roles = "Property Manager,Admin,Property Owner")]
        public ActionResult Create()
        {
            // show only the addresses that belong to the buildings
            var addresses = db.Buildings.Select(a => a.AddressID).ToList();
            ViewBag.AddressID = db.Addresses
                .Where(a => addresses.Contains(a.AddressID))
                .Select(a => new SelectListItem { Value = a.AddressID.ToString(), Text = a.StreetName + a.StreetNumber + a.City + a.Province });
            ViewBag.BuildingID = db.Buildings.Select(b => new SelectListItem { Text = b.BuildingName, Value = b.BuildingID.ToString() });
            ViewBag.PropertyManagerID = db.Users
                    .Where(u => u.RoleID == 3)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName");
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Property Manager,Admin,Property Owner")]
        public ActionResult Create([Bind(Include = "ApartmentID,PropertyManagerID,AddressID,StatusID,BuildingID,NumberOfRooms,Amenities,Price,Floor,ConstructionYear,Area")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                db.Apartments.Add(apartment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var addresses = db.Buildings.Select(a => a.AddressID).ToList();
            ViewBag.AddressID = db.Addresses
                .Where(a => addresses.Contains(a.AddressID))
                .Select(a => new SelectListItem { Value = a.AddressID.ToString(), Text = a.StreetName + a.StreetNumber + a.City + a.Province });
            ViewBag.BuildingID = db.Buildings.Select(b => new SelectListItem { Text = b.BuildingName, Value = b.BuildingID.ToString() });
            ViewBag.PropertyManagerID = db.Users
                    .Where(u => u.RoleID == 3)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName");
            return View(apartment);
        }

        // GET: Apartments/Edit/5
        [Authorize(Roles = "Property Manager,Admin,Property Owner")]
        public ActionResult Edit(int? apartmentID)
        {
            if (apartmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Find(apartmentID);
            if (apartment == null)
            {
                return HttpNotFound();
            }

            var addresses = db.Buildings.Select(a => a.AddressID).ToList();
            ViewBag.AddressID = db.Addresses
                .Where(a => addresses.Contains(a.AddressID))
                .Select(a => new SelectListItem { Value = a.AddressID.ToString(), Text = a.StreetName + a.StreetNumber + a.City + a.Province });
            ViewBag.BuildingID = db.Buildings.Select(b => new SelectListItem { Text = b.BuildingName, Value = b.BuildingID.ToString() });
            ViewBag.PropertyManagerID = db.Users
                    .Where(u => u.RoleID == 3)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName", apartment.StatusID);
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Property Manager,Admin,Property Owner")]
        public ActionResult Edit([Bind(Include = "ApartmentID,PropertyManagerID,AddressID,StatusID,BuildingID,NumberOfRooms,Amenities,Price,Floor,ConstructionYear,Area")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apartment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = db.Addresses.Select(a => new SelectListItem
            {
                Text = a.StreetName + " " + a.StreetNumber + ", " + a.City + ", " + a.Province,
                Value = a.AddressID.ToString()
            });
            ViewBag.BuildingID = db.Buildings.Select(b => new SelectListItem {  Text = b.BuildingName, Value = b.BuildingID.ToString() });
            ViewBag.PropertyManagerID = db.Users
                    .Where(u => u.RoleID == 3)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName", apartment.StatusID);
            return View(apartment);
        }

        // GET: Apartments/Delete/5
        [Authorize(Roles = "Property Manager,Admin,Property Owner")]
        public ActionResult Delete(int? apartmentID)
        {
            if (apartmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Find(apartmentID);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropertyManagerID = db.Users
                    .Where(u => u.RoleID == 3)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });
            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Property Manager,Admin,Property Owner")]
        public ActionResult DeleteConfirmed(int apartmentID)
        {
            Apartment apartment = db.Apartments.Find(apartmentID);
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
