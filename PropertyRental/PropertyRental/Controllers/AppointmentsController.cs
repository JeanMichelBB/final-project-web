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
    public class AppointmentsController : Controller
    {
        private PropertyRentalDBEntities db = new PropertyRentalDBEntities();

        // GET: Appointments
public ActionResult Index()
{
    if (User.IsInRole("Potential Tenant"))
    {
        User user = db.Users.FirstOrDefault(u => u.FirstName == User.Identity.Name);

        var appointments = db.Appointments
            .Include(a => a.Address)
            .Include(a => a.User)
            .Include(a => a.User1)
            .Where(a => a.TenantID == user.UserID); // Filter appointments for the current user

        return View(appointments.ToList());
    }
    else
    {
        var appointments = db.Appointments
            .Include(a => a.Address)
            .Include(a => a.User)
            .Include(a => a.User1);

        return View(appointments.ToList());
    }
}


        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create(int? id)
        {
            // find the aartment by id
            var model = db.Apartments.Find(id);
            // get the tenant id
            User tenant = db.Users.FirstOrDefault(u => u.FirstName == User.Identity.Name);

            if (User.IsInRole("Potential Tenant"))
            {
                // show only the apartments form model.AddressID
                ViewBag.AddressID = new SelectList(db.Addresses.Where(a => a.AddressID == model.AddressID), "AddressID", "StreetName");
                //ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetName", model.AddressID);
                ViewBag.PropertyManagerID = new SelectList(db.Users.Where(u => u.UserID == model.PropertyManagerID), "UserID", "FirstName");
                ViewBag.TenantID = new SelectList(db.Users.Where(u => u.UserID == tenant.UserID), "UserID", "FirstName");
            }
            else
            {
                ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetName");
                ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName");
                ViewBag.TenantID = new SelectList(db.Users, "UserID", "FirstName");
            }

            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentID,PropertyManagerID,TenantID,Timestamp,AddressID")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetName", appointment.AddressID);
            ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName", appointment.PropertyManagerID);
            ViewBag.TenantID = new SelectList(db.Users, "UserID", "FirstName", appointment.TenantID);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetName", appointment.AddressID);
            ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName", appointment.PropertyManagerID);
            ViewBag.TenantID = new SelectList(db.Users, "UserID", "FirstName", appointment.TenantID);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentID,PropertyManagerID,TenantID,Timestamp,AddressID")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetName", appointment.AddressID);
            ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName", appointment.PropertyManagerID);
            ViewBag.TenantID = new SelectList(db.Users, "UserID", "FirstName", appointment.TenantID);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
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
