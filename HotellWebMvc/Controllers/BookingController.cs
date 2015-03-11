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
            HotellDLL.DatabaseDataContext dc = new HotellDLL.DatabaseDataContext();
            return View(new BookingIndex
            {
                Bookings = dc.Bookings.ToList()
            });
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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
