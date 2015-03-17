using HotellDLL;
using HotellWebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotellWebMvc.Controllers
{
    [Authorize(Roles = "guest")]
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Index(BookingIndex form)
        {
            DatabaseDataContext dc = new DatabaseDataContext();
            //henter alle eksisterende bookings for aktiv bruker
            IEnumerable<Booking> bookingsForUser = dc.Bookings
                .Where(x => x.Guest.email == User.Identity.Name)
                .ToList();

            // hvis ikke brukeren har valgt formdata / første gang brukeren ser forsiden
            if (form.checkIn == DateTime.MinValue || form.checkOut == DateTime.MinValue)
            {
                return View(new BookingIndex
                    {
                        Bookings = bookingsForUser,
                        SelectedRoom = null
                    });
            }
            else
            {

                // logikk for å vise treff matchene søk på dato
                var viewObject = dc.Rooms.Select(room => new { bed = room.bed, roomId = room.roomId, Bookings = room.Bookings, price = room.price, quality = room.quality })
                    .Where(room => room.Bookings.First() == null || room.Bookings.Any(booking => (form.checkIn > booking.checkOutDate) || (form.checkOut < booking.checkInDate)))
                    .Where(room => room.quality == form.quality)
                    .Where(room => room.bed == form.beds).FirstOrDefault();

                // Hvis vi finner et matchene rom til søk
                if (viewObject != null)
                {
                    Room tempRoom = dc.Rooms.Where(x => x.roomId == viewObject.roomId).First();

                    Booking b = new Booking();
                    b.checkInDate = form.checkIn;
                    b.checkOutDate = form.checkOut;
                    b.Guest = dc.Guests.Where(x => x.email.Equals(User.Identity.Name)).FirstOrDefault();

                    if (b.Guest == null)
                        return HttpNotFound();

                    b.Room = tempRoom;

                    Session["datacontext"] = dc;
                    Session["booking"] = b;


                    return View(new BookingIndex
                    {
                        Bookings = bookingsForUser,
                        SelectedRoom = b.Room,
                        matchesSearch = true
                    });
                }
                else
                    return View(new BookingIndex
                    {
                        Bookings = bookingsForUser,
                        SelectedRoom = null,
                        matchesSearch = false
                    });
            }
        }

        // POST: Booking
        [HttpPost]
        public ActionResult Booking()
        {
            if (Session["datacontext"] == null)
                return HttpNotFound();

            using (DatabaseDataContext dc = (DatabaseDataContext)Session["datacontext"])
            {
                try
                {
                    Booking newBooking = (Booking)Session["booking"];


                    if (newBooking == null)
                        return HttpNotFound();

                    dc.Bookings.InsertOnSubmit(newBooking);
                    dc.SubmitChanges();



                    //if (g == null)
                    //    return HttpNotFound();
                    //if (form.SelectedRoom == null)
                    //    return HttpNotFound();

                    //b.Room = form.SelectedRoom;
                    //b.checkInDate = form.checkIn;
                    //b.checkOutDate = form.checkOut;
                    //b.Guest = g;

                    //dc.Bookings.InsertOnSubmit(b);
                    //dc.SubmitChanges();

                    return RedirectToRoute("Booking");
                }
                catch
                {
                    return RedirectToRoute("Login");
                }
            }
        }
    }
}
