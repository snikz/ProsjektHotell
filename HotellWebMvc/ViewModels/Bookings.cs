using HotellDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotellWebMvc.ViewModels
{
    /// <summary>
    /// This class will prepare the different lists for the booking-view
    /// the controller will then populate these lists and return it to the view
    /// </summary>

    public class BookingIndex
    {
        public IEnumerable<Booking> Bookings { get; set; }
        
    }

}