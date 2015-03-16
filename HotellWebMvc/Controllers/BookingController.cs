using HotellDLL;
using HotellWebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotellWebMvc.Controllers
{
    [Authorize(Roles ="guest")]
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Index(Book form)
        {
            DatabaseDataContext dc = new HotellDLL.DatabaseDataContext();
            
            //henter alle eksisterende bookings for aktiv bruker
            IEnumerable<Booking> bookingsForUser = dc.Bookings
                .Where(x => x.Guest.email == User.Identity.Name)
                .ToList();
            
            // hvis ikke brukeren har valgt formdata / første gang brukeren ser forsiden
            if (form.checkIn == null || form.checkOut == null)
            {
                return View(new BookingIndex
                    {
                        Bookings = bookingsForUser,
                        Rooms = null
                    });
                // returner view uten å hente availableRoom-data fra DB
            }
            else
            {
                //lag liste basert på data fra form og klargjør det til view under
                return View(new BookingIndex
                {
                    Bookings = bookingsForUser,
                    Rooms = dc.Rooms.ToList()
                });
            }
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: Index
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
