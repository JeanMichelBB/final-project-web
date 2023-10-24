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
    public class MessagesController : Controller
    {
        private PropertyRentalDBEntities db = new PropertyRentalDBEntities();
        private int userID;
        // GET: Messages
        [Authorize]
        public ActionResult Index()
        {
            userID = db.Logins.FirstOrDefault(u => u.Email == User.Identity.Name).UserID;

            var messages = db.Messages.Where(m => m.ReceiverID == userID).ToList();
            ViewBag.IsReceived = true;
            return View(messages);
        }

        public ActionResult Received()
        {
            userID = db.Logins.FirstOrDefault(u => u.Email == User.Identity.Name).UserID;
            var receivedMessages = db.Messages.Where(m => m.ReceiverID == userID).ToList();
            ViewBag.IsReceived = true;
            ViewBag.IsSent = false;
            return View("Index", receivedMessages);
        }

        public ActionResult Sent()
        {
            userID = db.Logins.FirstOrDefault(u => u.Email == User.Identity.Name).UserID;
            var sentMessages = db.Messages.Where(m => m.SenderID == userID).ToList();
            var receivedMessages = db.Messages.Where(m => m.ReceiverID == userID).ToList();
            ViewBag.IsReceived = false;
            ViewBag.IsSent = true;
            return View("Index", sentMessages);
        }

        // GET: Messages/Details/5
        [Authorize]
        public ActionResult Details(int? id, bool? isReceived)
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

            if (isReceived == true)
            {
                message.MessageStatusID = 2;
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
            }
            var senderEmail = db.Logins.FirstOrDefault(u => u.UserID == message.SenderID);
            var receiverEmail = db.Logins.FirstOrDefault(u => u.UserID == message.ReceiverID);
            if (senderEmail == null || receiverEmail == null)
            {
                ViewBag.SenderEmail = "";
                ViewBag.ReceiverEmail = "";
            } else
            {
                ViewBag.SenderEmail = senderEmail.Email;
                ViewBag.ReceiverEmail = receiverEmail.Email;
            }
            return View(message);
        }

        // GET: Messages/Create
        [Authorize]
        public ActionResult Create(int? apartmentID)
        {
            var userID = db.Logins.FirstOrDefault(u => u.Email == User.Identity.Name).UserID;
            if (User.IsInRole("Potential Tenant"))
            {
                ViewBag.MessageStatusID = new SelectList(db.MessageStatuses, "MessageStatusID", "Status");
                // ViewBag.SenderID = new SelectList(db.Users.Where(predicate: u => u.UserID == user.UserID), "UserID", "FirstName");
                ViewBag.SenderID = db.Users.Where(u => u.UserID == userID).Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });
                if (apartmentID != null)
                {
                    var apartment = db.Apartments.Find(apartmentID);
                    ViewBag.ReceiverID = db.Users.Where(u => u.UserID == apartment.PropertyManagerID).Select(u => new SelectListItem { Text = u.FirstName + " " + u.LastName, Value = u.UserID.ToString() });
                }
                else
                {
                    return RedirectToAction("Index", "Apartments");
                }
            }
            else
            {
                ViewBag.MessageStatusID = new SelectList(db.MessageStatuses, "MessageStatusID", "Status");
                ViewBag.ReceiverID = new SelectList(db.Users.Where(u => u.UserID != userID), "UserID", "FirstName");
                ViewBag.SenderID = new SelectList(db.Users.Where(u => u.UserID == userID), "UserID", "FirstName");
            }
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "MessageID,SenderID,ReceiverID,Subject,MessageBody,Timestamp,MessageStatusID")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.Timestamp = DateTime.Now;
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MessageStatusID = new SelectList(db.MessageStatuses, "MessageStatusID", "Status", message.MessageStatusID);
            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "FirstName", message.ReceiverID);
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "FirstName", message.SenderID);
            return View(message);
        }

        // GET: Messages/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
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
            ViewBag.MessageStatusID = new SelectList(db.MessageStatuses, "MessageStatusID", "Status", message.MessageStatusID);
            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "FirstName", message.ReceiverID);
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "FirstName", message.SenderID);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public ActionResult Edit([Bind(Include = "MessageID,SenderID,ReceiverID,Subject,MessageBody,Timestamp,MessageStatusID")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MessageStatusID = new SelectList(db.MessageStatuses, "MessageStatusID", "Status", message.MessageStatusID);
            ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "FirstName", message.ReceiverID);
            ViewBag.SenderID = new SelectList(db.Users, "UserID", "FirstName", message.SenderID);
            return View(message);
        }

        // GET: Messages/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
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
        [Authorize(Roles ="Admin")]
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
