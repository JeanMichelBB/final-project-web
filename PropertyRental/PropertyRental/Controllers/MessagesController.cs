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

            var messages = db.Messages
                .Include(m => m.MessageStatus)
                .Include(m => m.User)
                .Include(m => m.User1)
                .Where(m => m.SenderID == userID || m.ReceiverID == userID);

            return View(messages.ToList());
        }

        // GET: Messages/Details/5
        [Authorize]
        public ActionResult Details(int? id)
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
        [Authorize]
        public ActionResult Create(int? id)
        {
            if (User.IsInRole("Potential Tenant"))
            {
                ViewBag.MessageStatusID = new SelectList(db.MessageStatuses, "MessageStatusID", "Status");
                
                ViewBag.SenderID = new SelectList(db.Users.Where(u => u.UserID == userID), "UserID", "FirstName");
                // reviver id is null
                if (id != null)
                {
                    var apartment = db.Apartments.Find(id);

                    ViewBag.ReceiverID = new SelectList(db.Users.Where(u => u.UserID == apartment.PropertyManagerID), "UserID", "FirstName");
                }
                else
                {
                    ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "FirstName");
                }
            }
            else
            {
                ViewBag.MessageStatusID = new SelectList(db.MessageStatuses, "MessageStatusID", "Status");
                ViewBag.ReceiverID = new SelectList(db.Users, "UserID", "FirstName");
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
