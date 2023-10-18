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
    public class TenantController : Controller
    {
        private PropertyRentalDBEntities db = new PropertyRentalDBEntities();

        public ActionResult Index()
        {
            var apartments = db.Apartments.Include(a => a.Address).Include(a => a.Building).Include(a => a.User).Include(a => a.Status);
            return View(apartments.ToList());
        }
         public ActionResult Appointment()
        {
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetName");
            ViewBag.PropertyManagerID = new SelectList(db.Users, "UserID", "FirstName");
            ViewBag.TenantID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Appointment([Bind(Include = "AppointmentID,PropertyManagerID,TenantID,Timestamp,AddressID")] Appointment appointment)
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
        public ActionResult NewMessage()
        {
            ViewBag.MessageStatusID = new SelectList(db.MessageStatuses, "MessageStatusID", "Status");
            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "FirstName");
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewMessage([Bind(Include = "MessageID,SenderID,ReceiverID,Subject,MessageBody,Timestamp,MessageStatusID")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MessageStatusID = new SelectList(db.MessageStatuses, "MessageStatusID", "Status", message.MessageStatusID);
            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "FirstName", message.ReceiverID);
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "FirstName", message.SenderID);
            return View(message);
        }

        public ActionResult MessageBox()
        {
            ViewBag.MessageStatusID = new SelectList(db.MessageStatuses, "MessageStatusID", "Status");
            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "FirstName");
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }
        public ActionResult Messages(Message message, int id)
        {

            //using (PropertyRentalDBEntities context = new PropertyRentalDBEntities())
            //{
            //   var user = context.Users.FirstOrDefault(u => u.UserID == id);

            //    var apartments = context.Apartments.Where(a => a.StatusID == 1).ToList();

            //    tenantModel.FirstName = user.FirstName;
            //    tenantModel.LastName = user.LastName;

            //    tenantModel.Apartments = apartments;
            //}
            return View(message);
        }
        public ActionResult MessageIndex()
        {
            var messages = db.Messages.Include(m => m.MessageStatus).Include(m => m.User).Include(m => m.User1);
            return View(messages.ToList());
        }
        public ActionResult DetailsMessage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult CreateMessage()
        {
            ViewBag.MessageStatusID = new SelectList(db.MessageStatuses, "MessageStatusID", "Status");
            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "FirstName");
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "FirstName");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMessage([Bind(Include = "MessageID,SenderID,ReceiverID,Subject,MessageBody,Timestamp,MessageStatusID")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MessageStatusID = new SelectList(db.MessageStatuses, "MessageStatusID", "Status", message.MessageStatusID);
            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "FirstName", message.ReceiverID);
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "FirstName", message.SenderID);
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult DeleteMessage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
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