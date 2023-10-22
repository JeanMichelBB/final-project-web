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
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("Potential Tenant"))
            {
                var userID = db.Logins.FirstOrDefault(u => u.Email == User.Identity.Name).UserID;

                var appointments = db.Appointments
                    .Include(a => a.Address)
                    .Include(a => a.PropertyManager)
                    .Include(a => a.PotentialTenant)
                    .Where(a => a.TenantID == userID); // Filter appointments for the current user

                return View(appointments.ToList());
            }
            else
            {
                var appointments = db.Appointments
                    .Include(a => a.Address)
                    .Include(a => a.PropertyManager)
                    .Include(a => a.PotentialTenant);

                return View(appointments.ToList());
            }
        }


        // GET: Appointments/Details/5
        [Authorize]
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
        [Authorize]
        public ActionResult Create(int? id)
        {
            var apartment = db.Apartments.Find(id);
            var tenantID = db.Logins.FirstOrDefault(u => u.Email == User.Identity.Name).UserID;

            if (User.IsInRole("Potential Tenant"))
            {
                ViewBag.AddressID = new SelectList(db.Addresses.Where(a => a.AddressID == apartment.AddressID), "AddressID", "StreetName");
                ViewBag.PropertyManagerID = new SelectList(db.Users.Where(u => u.UserID == apartment.PropertyManagerID), "UserID", "FirstName");
                ViewBag.TenantID = new SelectList(db.Users.Where(u => u.UserID == tenantID), "UserID", "FirstName");
            }
            else
            {
                ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetName");
                ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName");
                ViewBag.TenantID = new SelectList(db.Users, "UserID", "FirstName");
            }
            // Create a select list of hours from 9am to 5pm
            ViewBag.SelectedTime = new SelectList(Enumerable.Range(9, 9).Select(x => new SelectListItem { Text = x.ToString() + ":00", Value = x.ToString() + ":00" }), "Value", "Text");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "AppointmentID,PropertyManagerID,TenantID,SelectedDate,SelectedTime,AddressID")] AppointmentViewModel appointmentView)
        {
            Appointment appointment;
            if (ModelState.IsValid)
            {
                DateTime selectedDateTime = DateTime.Parse(appointmentView.SelectedDate + " " + appointmentView.SelectedTime);
                appointmentView.Timestamp = selectedDateTime;
                appointment = new Appointment
                {
                    PropertyManagerID = appointmentView.PropertyManagerID,
                    TenantID = appointmentView.TenantID,
                    Timestamp = appointmentView.Timestamp,
                    AddressID = appointmentView.AddressID
                };
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetName", appointmentView.AddressID);
            ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName", appointmentView.PropertyManagerID);
            ViewBag.TenantID = new SelectList(db.Users, "UserID", "FirstName", appointmentView.TenantID);
            return View(appointmentView);
        }

        // GET: Appointments/Edit/5
        [Authorize]
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
            AppointmentViewModel appointmentView = new AppointmentViewModel
            {
                AppointmentID = appointment.AppointmentID,
                PropertyManagerID = appointment.PropertyManagerID,
                TenantID = appointment.TenantID,
                SelectedDate = appointment.Timestamp.Value.ToShortDateString(),
                SelectedTime = appointment.Timestamp.Value.ToShortTimeString(),
                AddressID = appointment.AddressID
            };
            if (User.IsInRole("Potential Tenant"))
            {
                ViewBag.AddressID = new SelectList(db.Addresses.Where(a => a.AddressID == appointment.AddressID), "AddressID", "StreetName");
                ViewBag.PropertyManagerID = new SelectList(db.Users.Where(u => u.UserID == appointment.PropertyManagerID), "UserID", "FirstName");
                ViewBag.TenantID = new SelectList(db.Users.Where(u => u.UserID == appointment.TenantID), "UserID", "FirstName");
            }
            else
            {
                ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetName");
                ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName");
                ViewBag.TenantID = new SelectList(db.Users, "UserID", "FirstName");
            }

            ViewBag.SelectedTime = new SelectList(Enumerable.Range(9, 9).Select(x => new SelectListItem { Text = x.ToString() + ":00", Value = x.ToString() + ":00" }), "Value", "Text");
            return View(appointmentView);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "AppointmentID,PropertyManagerID,TenantID,SelectedDate,SelectedTime,AddressID")] AppointmentViewModel appointmentView)
        {
            if (ModelState.IsValid)
            {
                var appointment = db.Appointments.Find(appointmentView.AppointmentID);
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetName", appointmentView.AddressID);
            ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName", appointmentView.PropertyManagerID);
            ViewBag.TenantID = new SelectList(db.Users, "UserID", "FirstName", appointmentView.TenantID);
            return View(appointmentView);
        }

        // GET: Appointments/Delete/5
        [Authorize]
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
        [Authorize]
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
