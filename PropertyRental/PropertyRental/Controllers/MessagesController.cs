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

        // GET: Messages
        public ActionResult Index()
        {
            var messages = db.Messages.Include(m => m.MessageStatus).Include(m => m.User).Include(m => m.User1);
            return View(messages.ToList());
        }

        // GET: Messages/Details/5
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
        public ActionResult Create()
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
