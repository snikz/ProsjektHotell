using HotellDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotellWebMvc.ViewModels
{
    public class BookingIndex
    {
        public IEnumerable<Booking> Bookings { get; set; }
    }
}