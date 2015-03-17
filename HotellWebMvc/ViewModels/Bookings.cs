using HotellDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotellWebMvc.ViewModels
{
    /// <summary>
    /// This class will prepare the different lists for the booking-view,
    /// the controller will then populate these lists and return it to the view
    /// </summary>

    public class BookingIndex
    {
        public IEnumerable<Booking> Bookings { get; set; }
        public Room SelectedRoom { get; set; }

        [Required]
        public DateTime checkIn { get; set; }

        [Required]
        public DateTime checkOut { get; set; }

        public int quality { get; set; }
        public int beds { get; set; }

        public bool matchesSearch { get; set; }
    }
}