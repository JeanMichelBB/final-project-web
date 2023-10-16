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
        public ActionResult Login(UserModel model)
        {
            using (PropertyRentalDBEntities context = new PropertyRentalDBEntities())
            {
                bool isValidUser = context.Logins.Any(login => login.Email.ToLower() ==
                model.Email.ToLower() && login.Password == model.Password); if (isValidUser)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false); return RedirectToAction("Index", "Employees");
                }
                ModelState.AddModelError("", "Invalid username or password !"); return View();
            }
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User user,Login login,Address address)
        {
            using (PropertyRentalDBEntities context = new
            PropertyRentalDBEntities())
            {
                context.Logins.Add(login);
                context.Users.Add(user);
                context.Addresses.Add(address);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut(); 
            return RedirectToAction("Login");
        }
    }
}