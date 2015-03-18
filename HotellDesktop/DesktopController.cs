using System.Linq;
using System.Data.Linq;

namespace HotellDesktop
{
    class DesktopController
    {
        HotellDLL.DatabaseDataContext database;

        /// <summary>
        /// Constructor that initiates databaseContext
        /// </summary>
        public DesktopController()
        {

            database = new HotellDLL.DatabaseDataContext();

        }

        /// <summary>
        /// Returns all bookings in database
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
        /// Returns all guest in database
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
        /// Returns all rooms in database
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
        /// Returns all service in database
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
        /// Adds a new user in database
        /// </summary>
        /// <param name="newGuest">Guest to be added</param>
        public void addUser(HotellDLL.Guest newGuest)
        {
            database.Guests.InsertOnSubmit(newGuest);
            database.SubmitChanges();
        }

        /// <summary>
        /// Adds a new reservation in databse
        /// </summary>
        /// <param name="newBooking">Booking to be added</param>
        public void addReservation(HotellDLL.Booking newBooking)
        {
            database.Bookings.InsertOnSubmit(newBooking);
            database.SubmitChanges();
        }

        /// <summary>
        /// Deletes a reservation
        /// </summary>
        /// <param name="id">int id to the reservation</param>
        public void deleteReservation(int id)
        {
            HotellDLL.Booking delete = getBooking().Where(bookings => bookings.bookingId == id).FirstOrDefault();
            database.Bookings.DeleteOnSubmit(delete);
            database.SubmitChanges();
        }

        /// <summary>
        /// Changes from one room to another
        /// </summary>
        /// <param name="id">int id to new room</param>
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
        /// Adds a new service in database
        /// </summary>
        /// <param name="newService">new service</param>
        public void addService(HotellDLL.Service newService)
        {
            database.Services.InsertOnSubmit(newService);
            database.SubmitChanges();
        }

        /// <summary>
        /// Deletes a service
        /// </summary>
        /// <param name="serviceID">int id to service</param>
        public void deleteService(int serviceID)
        {
            HotellDLL.Service delete = getService().Where(service => service.id.Equals(serviceID)).FirstOrDefault();
            database.Services.DeleteOnSubmit(delete);
            database.SubmitChanges();
        }

        /// <summary>
        /// Method for check in
        /// </summary>
        /// <param name="bookingId">int booking id</param>
        public void checkIn(int bookingId)
        {

            HotellDLL.Booking booking = getBooking().Where(book => book.bookingId == bookingId).FirstOrDefault();
            booking.checkedIn = true;
            database.SubmitChanges();


        }

        /// <summary>
        /// Method for check out
        /// </summary>
        /// <param name="bookingId">int booking id</param>
        public void checkOut(int bookingId)
        {

            HotellDLL.Booking booking = getBooking().Where(book => book.bookingId == bookingId).FirstOrDefault();
            booking.checkedOut = true;
            database.SubmitChanges();

        }
    }
}
