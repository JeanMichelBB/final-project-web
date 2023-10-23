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
        public ActionResult Details(int? appointmentID)
        {
            if (appointmentID == null)
            {
                return RedirectToAction("Index");
            }
            Appointment appointment = db.Appointments.Find(appointmentID);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        [Authorize]
        public ActionResult Create(int? apartmentID)
        {
            
            if (User.IsInRole("Potential Tenant"))
            {
                if (apartmentID == null)
                {
                    return RedirectToAction("Index");
                }

                var apartment = db.Apartments.Find(apartmentID);
                var tenantID = db.Logins.FirstOrDefault(u => u.Email == User.Identity.Name).UserID;

                // Show only the address of the selected appartment
                ViewBag.AddressID = db.Addresses
                    .Where(a => a.AddressID == apartment.AddressID)
                    .Select(a => new SelectListItem { Text = a.StreetNumber + " " + a.StreetName + ", " + a.City + ", " + a.Province, Value = a.AddressID.ToString() });

                // Show only the property manager of the selected appartment
                ViewBag.PropertyManagerID = db.Users
                    .Where(u => u.UserID == apartment.PropertyManagerID)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });

                // Show only the current user
                ViewBag.TenantID = db.Users
                    .Where(u => u.UserID == tenantID)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });
            }
            else
            {
                ViewBag.AddressID = db.Addresses
                    .Select(a => new SelectListItem { Text = a.StreetNumber + " " + a.StreetName + ", " + a.City + ", " + a.Province, Value = a.AddressID.ToString() });

                // Select only the users that are property managers
                ViewBag.PropertyManagerID = db.Users
                    .Where(u => u.RoleID == 3)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });

                // Select only the users that are potential tenants
                ViewBag.TenantID = db.Users
                    .Where(u => u.RoleID == 4)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });
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
            // If we got this far, something failed, redisplay form as in the create view
            if (User.IsInRole("Potential Tenant"))
            {
                ViewBag.AddressID = db.Addresses
                    .Where(a => a.AddressID == appointmentView.AddressID)
                    .Select(a => new SelectListItem { Text = a.StreetNumber + " " + a.StreetName + ", " + a.City + ", " + a.Province, Value = a.AddressID.ToString() });

                // Show only the property manager of the selected appartment
                ViewBag.PropertyManagerID = db.Users
                    .Where(u => u.UserID == appointmentView.PropertyManagerID)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });

                // Show only the current user
                ViewBag.TenantID = db.Users
                    .Where(u => u.UserID == appointmentView.TenantID)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });
            } else
            {
                ViewBag.AddressID = db.Addresses
                    .Select(a => new SelectListItem { Text = a.StreetNumber + " " + a.StreetName + ", " + a.City + ", " + a.Province, Value = a.AddressID.ToString() });

                // Select only the users that are property managers
                ViewBag.PropertyManagerID = db.Users
                    .Where(u => u.RoleID == 3)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });

                // Select only the users that are potential tenants
                ViewBag.TenantID = db.Users
                    .Where(u => u.RoleID == 4)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });
            }

            return View(appointmentView);
        }

        // GET: Appointments/Edit/5
        [Authorize]
        public ActionResult Edit(int? appointmentID)
        {
            if (appointmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(appointmentID);
            
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
                ViewBag.AddressID = db.Addresses
                   .Where(a => a.AddressID == appointmentView.AddressID)
                   .Select(a => new SelectListItem { Text = a.StreetNumber + " " + a.StreetName + ", " + a.City + ", " + a.Province, Value = a.AddressID.ToString() });

                // Show only the property manager of the selected appartment
                ViewBag.PropertyManagerID = db.Users
                    .Where(u => u.UserID == appointmentView.PropertyManagerID)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });

                // Show only the current user
                ViewBag.TenantID = db.Users
                    .Where(u => u.UserID == appointmentView.TenantID)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });
            }
            else
            {
                ViewBag.AddressID = db.Addresses
                    .Select(a => new SelectListItem { Text = a.StreetNumber + " " + a.StreetName + ", " + a.City + ", " + a.Province, Value = a.AddressID.ToString() });

                // Select only the users that are property managers
                ViewBag.PropertyManagerID = db.Users
                    .Where(u => u.RoleID == 3)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });

                // Select only the users that are potential tenants
                ViewBag.TenantID = db.Users
                    .Where(u => u.RoleID == 4)
                    .Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });
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
                DateTime selectedDateTime = DateTime.Parse(appointmentView.SelectedDate + " " + appointmentView.SelectedTime);
                appointmentView.Timestamp = selectedDateTime;
                Appointment appointment = new Appointment
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

        // GET: Appointments/Delete/5
        [Authorize]
        public ActionResult Delete(int? appointmentID)
        {
            if (appointmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(appointmentID);
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
        public ActionResult DeleteConfirmed(int appointmentID)
        {
            Appointment appointment = db.Appointments.Find(appointmentID);
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
