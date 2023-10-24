using PropertyRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PropertyRental.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            using (PropertyRentalDBEntities context = new PropertyRentalDBEntities())
            {
                // TODO: Input validation
                var user = context.Logins.FirstOrDefault(login =>
                    login.Email.ToLower() == model.Email.ToLower() &&
                    login.Password == model.Password);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);

                    // Retrieve the user's RoleID from the Users table
                    var userRole = context.Users.FirstOrDefault(u => u.UserID == user.UserID);

                    if (userRole != null)
                    {
                        // Redirect based on the user's RoleID
                        switch (userRole.RoleID)
                        {
                            case 1: // Admin
                                return RedirectToAction("Index", "Apartments");
                            case 2: // Property Owner
                                return RedirectToAction("Index", "Apartments");
                            case 3: // Property Manager
                                return RedirectToAction("Index", "Apartments");
                            case 4: // Tenant
                                return RedirectToAction("Index", "Apartments");
                            default:
                                return RedirectToAction("login");
                        }
                    }
                }
                ModelState.AddModelError("", "Invalid username or password!");
            }
            return View();
        }


        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(LoginModel signup)
        {
            if (ModelState.IsValid)
            {
                using (PropertyRentalDBEntities context = new PropertyRentalDBEntities())
                {
                    // Create instences for login
                    var login = new Login
                    {
                        Email = signup.Email,
                        Password = signup.Password
                    };
                    // Create instances for User
                    var user = new User
                    {
                        FirstName = signup.FirstName,
                        LastName = signup.LastName,
                        Phone = signup.Phone,
                        RoleID = 4
                    };
                    // Create instances for Address
                    var address = new Address
                    {
                        StreetName = signup.StreetName,
                        StreetNumber = signup.StreetNumber,
                        City = signup.City,
                        PostalCode = signup.PostalCode,
                        Country = signup.Country,
                        Province = signup.Province,
                    };

                    // Add User and Address to the context
                    context.Logins.Add(login);
                    context.Users.Add(user);
                    context.Addresses.Add(address);

                    // Save changes to the database
                    
                    context.SaveChanges();

                    return RedirectToAction("Login");
                }
            }

            // If ModelState is not valid, return to the signup view with validation errors.
            return View(signup);
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut(); 
            return RedirectToAction("Login");
        }
    }
}