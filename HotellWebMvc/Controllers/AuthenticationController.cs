using HotellWebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotellDLL;
using System.Web.Security;   

namespace HotellWebMvc.Controllers
{
    public class AuthenticationController : Controller
    {
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("login");
        }


        // "" - GET
        public ActionResult Login()
        {
            return View();
        }

        // "" - POST
        [HttpPost]
        public ActionResult Login(AuthLogin form)
        {
            // valid form
            if (!ModelState.IsValid)
                return View(form);

            //sjekk mot db
            HotellDLL.DatabaseDataContext dc = new HotellDLL.DatabaseDataContext();
            Guest g = dc.Guests.Where(x => x.email.Equals(form.Email)).FirstOrDefault();
            
            if (g != null && g.password.Equals(form.Password)) 
            {
                FormsAuthentication.SetAuthCookie(form.Email, true);
                return RedirectToRoute("Booking");
            }

            

            else
                return RedirectToRoute("Login");
            
            
        }
    }
}