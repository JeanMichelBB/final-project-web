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
    public class UsersController : Controller
    {
        private PropertyRentalDBEntities db = new PropertyRentalDBEntities();

        // GET: Users
        public ActionResult Index()
        {
            ViewBag.ActiveLink = "Users";
            var users = db.Users.Include(u => u.Address).Include(u => u.Role);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "StreetName");
            ViewBag.RoleID = new SelectList(db.Roles.Where(r => r.RoleID != 4), "RoleID", "RoleName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FirstName,LastName,RoleID,Phone,AddressID,Email,Password,ConfirmPassword, StreetName, StreetNumber, City, PostalCode, Country, Province")] LoginModel userModel)
        {
            if (ModelState.IsValid)
            {
                using (PropertyRentalDBEntities context = new PropertyRentalDBEntities())
                {
                    // Create instences for login
                    var login = new Login
                    {
                        Email = userModel.Email,
                        Password = userModel.Password
                    };
                    // Create instances for User
                    var user = new User
                    {
                        FirstName = userModel.FirstName,
                        LastName = userModel.LastName,
                        Phone = userModel.Phone,
                        RoleID = userModel.RoleID
                    };
                    // Create instances for Address
                    var address = new Address
                    {
                        StreetName = userModel.StreetName,
                        StreetNumber = userModel.StreetNumber,
                        City = userModel.City,
                        PostalCode = userModel.PostalCode,
                        Country = userModel.Country,
                        Province = userModel.Province,
                    };

                    // Add User and Address to the context
                    context.Logins.Add(login);
                    context.Users.Add(user);
                    context.Addresses.Add(address);

                    // Save changes to the database

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(userModel);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginModel userModel = (from u in db.Users
                                        join l in db.Logins on u.UserID equals l.UserID
                                        join a in db.Addresses on u.AddressID equals a.AddressID
                                        where u.UserID == id
                                        select new LoginModel
                                        {
                                        UserID = u.UserID,
                                        FirstName = u.FirstName,
                                        LastName = u.LastName,
                                        RoleID = u.RoleID,
                                        Phone = u.Phone,
                                        Email = l.Email,
                                        Password = l.Password,
                                        StreetName = a.StreetName,
                                        StreetNumber = a.StreetNumber,
                                        City = a.City,
                                        PostalCode = a.PostalCode,
                                        Country = a.Country,
                                        Province = a.Province
                                    }).FirstOrDefault();
            if (userModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", userModel.RoleID);
            return View(userModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,FirstName,LastName,RoleID,Phone,LoginID,Email,Password,StreetName,StreetNumber,City,PostalCode,Country,Province")] LoginModel userModel)
        {
            if (ModelState.IsValid)
            {
                // Fetch the existing user, login, and address from the database
                var existingUser = db.Users.Find(userModel.UserID);
                var existingLogin = db.Logins.Where(l => l.UserID == userModel.UserID).FirstOrDefault();
                var existingAddress = db.Addresses.Find(existingUser.AddressID);

                if (existingUser != null && existingLogin != null)
                {
                    // Update the user properties
                    existingUser.FirstName = userModel.FirstName;
                    existingUser.LastName = userModel.LastName;
                    existingUser.RoleID = userModel.RoleID;
                    existingUser.Phone = userModel.Phone;

                    // Update the login properties
                    existingLogin.Email = userModel.Email;
                    existingLogin.Password = userModel.Password;

                    // Update the address properties
                    existingAddress.StreetName = userModel.StreetName;
                    existingAddress.StreetNumber = userModel.StreetNumber;
                    existingAddress.City = userModel.City;
                    existingAddress.PostalCode = userModel.PostalCode;
                    existingAddress.Country = userModel.Country;
                    existingAddress.Province = userModel.Province;

                    // Mark the entities as Modified
                    db.Entry(existingUser).State = EntityState.Modified;
                    db.Entry(existingLogin).State = EntityState.Modified;
                    db.Entry(existingAddress).State = EntityState.Modified;

                    // Save changes
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return View(userModel);
        }



        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
