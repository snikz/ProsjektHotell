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
        public ActionResult Index(BookingIndex form)
        {
            DatabaseDataContext dc = new DatabaseDataContext();
                //henter alle eksisterende bookings for aktiv bruker
                IEnumerable<Booking> bookingsForUser = dc.Bookings
                    .Where(x => x.Guest.email == User.Identity.Name)
                    .ToList();

                //// hvis ikke brukeren har valgt formdata / første gang brukeren ser forsiden
                //if (form.checkIn == null || form.checkOut == null)
                //{
                //    return View(new BookingIndex
                //        {
                //            Bookings = bookingsForUser,
                //            SelectedRoom = null
                //        });
                //}
                //else
                //{
                Room tempRoom = dc.Rooms
                    .Where(room => room.bed == form.beds
                        && room.quality == form.quality
                        ).FirstOrDefault();
                // insert full logikk for å vise treff matchene søk på dato


                // Hvis det eksisterer et rom som treffer søk
                if (tempRoom != null)
                {
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

        // POST: Booking
        [HttpPost]
        public ActionResult Booking(BookingIndex b)
        {
            if (Session["datacontext"] == null)
                return HttpNotFound();

            using (DatabaseDataContext dc = (DatabaseDataContext)Session["datacontext"])
            {
                try
                {
                    Booking newBooking = new Booking();
                    newBooking = (Booking)Session["booking"];


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
