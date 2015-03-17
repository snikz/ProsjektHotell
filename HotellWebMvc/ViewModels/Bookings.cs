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
    /// We prepare a booking-list for the view, and prepare some data that the user will 
    /// fill in. Then we can have the controller populate the list and handle the data later.
    /// </summary>

    public class BookingIndex
    {
        public IEnumerable<Booking> Bookings { get; set; }
        public Room SelectedRoom { get; set; }

        
        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }

        public int quality { get; set; }
        public int beds { get; set; }

        public bool matchesSearch { get; set; }
    }
}