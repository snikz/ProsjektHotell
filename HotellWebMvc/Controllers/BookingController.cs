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
        public ActionResult Index()
        {
            DatabaseDataContext dc = new HotellDLL.DatabaseDataContext();
            //IEnumerable<Booking> l = dc.Bookings.ToList();

            //lager aktuelle lister som skal brukes i view
            return View(new BookingIndex
            {
                Bookings = dc.Bookings
                .Where(x => x.Guest.email == User.Identity.Name)
                .ToList()

            });
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
