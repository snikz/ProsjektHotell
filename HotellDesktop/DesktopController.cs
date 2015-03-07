using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellDesktop
{
    class DesktopController
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
                return null;
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
                return null;
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
                return null;
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
                return null;
            }
        }
    }
}
