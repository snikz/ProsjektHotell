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

        /// <summary>
        /// Konstruktør som oppretter databaseContext
        /// </summary>
        public DesktopController()
        {

            database = new HotellDLL.DatabaseDataContext();

        }

        /// <summary>
        /// Returnerer alle bookinger i databasen
        /// </summary>
        /// <returns>Booking</returns>
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

        /// <summary>
        /// Returnerer alle gjester i databasen
        /// </summary>
        /// <returns>Guest</returns>
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


        /// <summary>
        /// Returnerer alle rom i databasen
        /// </summary>
        /// <returns>Room</returns>
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

        /// <summary>
        /// Returnerer alle services i databasen
        /// </summary>
        /// <returns>Service</returns>
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

        /// <summary>
        /// Ligger til an ny bruker i databasen
        /// </summary>
        /// <param name="newGuest">Guest gjesten som skal ligges inn</param>
        public void addUser(HotellDLL.Guest newGuest)
        {
            database.Guests.InsertOnSubmit(newGuest);
            database.SubmitChanges();
        }

        /// <summary>
        /// Ligger til en ny reservasjon i databasen
        /// </summary>
        /// <param name="newBooking">Booking bookingen som skal ligges inn</param>
        public void addReservation(HotellDLL.Booking newBooking)
        {
            database.Bookings.InsertOnSubmit(newBooking);
            database.SubmitChanges();
        }

        /// <summary>
        /// Sletter en reservasjon
        /// </summary>
        /// <param name="id">int id'en til reservasjonen som skal slettes</param>
        public void deleteReservation(int id)
        {
            HotellDLL.Booking delete = getBooking().Where(bookings => bookings.bookingId == id).FirstOrDefault();
            database.Bookings.DeleteOnSubmit(delete);
            database.SubmitChanges();
        }

        /// <summary>
        /// Bytter fra ett rom til ett annet
        /// </summary>
        /// <param name="id">id til rommet man skal bytte til</param>
        /// <param name="bookingId">booking id</param>
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

        /// <summary>
        /// Metode for å slette en service
        /// </summary>
        /// <param name="serviceID">id til service som skal bli slettet</param>
        public void deleteService(int serviceID)
        {
            HotellDLL.Service delete = getService().Where(service => service.id.Equals(serviceID)).FirstOrDefault();
            database.Services.DeleteOnSubmit(delete);
            database.SubmitChanges();
        }

        /// <summary>
        /// Metode for check in
        /// </summary>
        /// <param name="bookingId">id til booking som skal sjekkes inn</param>
        public void checkIn(int bookingId)
        {
            try
            {

                HotellDLL.Booking booking = getBooking().Where(book => book.bookingId == bookingId).FirstOrDefault();
                booking.checkedIn = true;
                database.SubmitChanges();
            }
            catch (Exception)
            {

            }

        }

        /// <summary>
        /// Metode for check out
        /// </summary>
        /// <param name="bookingId"></param>
        internal void checkOut(int bookingId)
        {
            try
            {
                HotellDLL.Booking booking = getBooking().Where(book => book.bookingId == bookingId).FirstOrDefault();
                database.Bookings.DeleteOnSubmit(booking);
                database.SubmitChanges();
            }
            catch (Exception)
            {

            }
        }
    }
}
