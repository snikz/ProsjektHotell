using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellDesktop
{
    class DekstopController
    {
        HotellDLL.DatabaseDataContext database;
        public void init()
        {
            database = new HotellDLL.DatabaseDataContext();
        }
        public Table<HotellDLL.Booking> getBooking()
        {
            try
            {
                return database.Bookings;
            }
            catch (System.NullReferenceException)
            {
                Table<HotellDLL.Booking> empty = new Table<HotellDLL.Booking>();
                return empty;
            }
        }
        public Table<HotellDLL.Guest> getGuest()
        {
            try
            {
                return database.Guests;
            }
            catch (System.NullReferenceException)
            {
                Table<HotellDLL.Guest> empty = new Table<HotellDLL.Guest>();
                return empty;
            }
        }
        public Table<HotellDLL.Room> getRoom()
        {
            try
            {
                return database.Rooms;
            }
            catch (System.NullReferenceException)
            {
                Table<HotellDLL.Room> empty = new Table<HotellDLL.Room>();
                return empty;
            }
        }
        public Table<HotellDLL.Service> getService()
        {
            try
            {
                return database.Services;
            }
            catch (System.NullReferenceException)
            {
                Table<HotellDLL.Service> empty = new Table<HotellDLL.Service>();
                return empty;
            }
        }
    }
}
