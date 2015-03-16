using HotellDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotellWebMvc.ViewModels
{
    /// <summary>
    /// This class will prepare the different lists for the booking-view,
    /// the controller will then populate these lists and return it to the view
    /// </summary>

    public class BookingIndex
    {
        public IEnumerable<Booking> Bookings { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
    }


    // IKKE LINKET OPP MED CONTROLLER OG VIEW SKIKKELIG ENDA
    public class Book
    {
        // allerede satt requried i html'en
        [Required]
        public DateTime checkIn { get; set; }

        [Required]
        public DateTime checkOut { get; set; }

        public int roomQuality { get; set; }
        public int numberBeds { get; set; }
    }

}