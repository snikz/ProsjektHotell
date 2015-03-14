using HotellDLL;
using HotellWebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotellWebMvc.Controllers
{
    public class RegistrationController : Controller
    {
        // GET - /registration
        public ActionResult Register()
        {
            return View();
        }


        // POST: Registration
        [HttpPost]
        public ActionResult Register(NewGuest form)
        {
            //sjekke at brukernavn er unikt, og at formen er valid
            //hvis ikke, send formskjemaet tilbake og vis feilmelding på samme side
            //ellers, prøve å registrere brukeren
            //evt logg bruker automatisk inn og vis hovedside
            
            DatabaseDataContext dc = new DatabaseDataContext();

            if (dc.Guests.Any(e => e.email == form.Email))
                ModelState.AddModelError("Email", "Email must be unique");
            if (!ModelState.IsValid)
                return View(form);

            var guest = new Guest
            {
                email = form.Email,
                firstName = form.FirstName,
                lastName = form.LastName
            };
            
            //skal egentlig bruke en SetPassword-metode her som hasher passordet og returnerer hashed streng
            guest.password = form.Password;

            dc.Guests.InsertOnSubmit(guest);
            dc.SubmitChanges();


            //sender nytt, utfylt AuthLogin-objekt til Authentication post-metoden
            //for å automatisk logge inn registrert bruker og følge sideflyten
            //return RedirectToAction("Login", "Authentication", new AuthLogin
            //{
            //    Email = guest.email,
            //    Password = guest.password
            //});

            //midlertidig returner tilbake til login (koden over returnerer til Login med parameterene i URL'en istedet)
            return RedirectToRoute("Login");

        }
    }
}