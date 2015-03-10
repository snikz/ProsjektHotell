using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HotellDesktop
{
    class DesktopController
    {
        HotellDLL.DatabaseDataContext database;
        public DesktopController()
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
        public void addUser(HotellDLL.Guest newGuest)
        {
            database.Guests.InsertOnSubmit(newGuest);
            database.SubmitChanges();
        }
        public void addReservation(HotellDLL.Booking newBooking)
        {
            database.Bookings.InsertOnSubmit(newBooking);
            database.SubmitChanges();
        }
        public void deleteReservation(int id)
        {
            HotellDLL.Booking delete = getBooking().Where(bookings => bookings.bookingId == id).FirstOrDefault();
            database.Bookings.DeleteOnSubmit(delete);
            database.SubmitChanges();
        }
        public void changeRoom(int id, int bookingId)
        {
            HotellDLL.DatabaseDataContext dbTemp = new HotellDLL.DatabaseDataContext();
            HotellDLL.Room newRoom = getRoom().Where(room => room.roomId == id).FirstOrDefault();
            HotellDLL.Booking oldBooking = getBooking().Where(booking => booking.bookingId == bookingId).FirstOrDefault();
            HotellDLL.Booking newBooking = new HotellDLL.Booking();
            newBooking.checkedIn = oldBooking.checkedIn;
            newBooking.checkedOut = oldBooking.checkedOut;
            newBooking.checkInDate = oldBooking.checkInDate;
            newBooking.checkOutDate = oldBooking.checkOutDate;
            newBooking.Guest = oldBooking.Guest;
            newBooking.guestId = oldBooking.guestId;
            newBooking.Room = newRoom;
            newBooking.roomId = newRoom.roomId;
            
            database.Bookings.DeleteOnSubmit(oldBooking);
            database.Bookings.InsertOnSubmit(newBooking);
            database.SubmitChanges();
        }

        /// <summary>
        /// Metode for å ligge til en ny service i databasen
        /// </summary>
        /// <param name="newService">ny service</param>
        public void addService(HotellDLL.Service newService)
        {
            database.Services.InsertOnSubmit(newService);
            database.SubmitChanges();
        }
    }
}
