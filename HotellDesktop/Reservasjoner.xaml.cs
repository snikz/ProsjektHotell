﻿using System.Linq;
using System.Data.Linq;
using System.Windows;
using System.Windows.Documents;
using System;

namespace HotellDesktop
{
    /// <summary>
    /// Class to use in linq query
    /// </summary>
    public class Reservations
    {
        public int bookingId { get; set; }
        public int roomId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public String checkInDate { get; set; }
        public String checkOutDate { get; set; }
    }
    /// <summary>
    /// Interaction logic for Reservasjoner.xaml
    /// </summary>
    public partial class Reservasjoner : Window
    {
        DesktopController controller;
        HotellDLL.Booking newBooking;
        /// <summary>
        /// StartMethod
        /// </summary>
        public Reservasjoner()
        {
            controller = new DesktopController();
            InitializeComponent();
            init();

        }
        /// <summary>
        /// Initializes the window. Creating data to put into listview.
        /// </summary>
        public void init()
        {
            try
            {
                Table<HotellDLL.Booking> bookingList = controller.getBooking();
                Table<HotellDLL.Guest> guestList = controller.getGuest();
                var viewData = bookingList.Select(bookings
                    => new { bookings.checkedOut, bookings.bookingId, bookings.guestId, bookings.roomId, bookings.checkInDate, 
                        bookings.checkOutDate })
                    .Where(bookings => bookings.checkedOut == false)
                    .Join(guestList, bookings => bookings.guestId, guests => guests.guestId, (bookings, guests)
                        => new Reservations{ bookingId = bookings.bookingId, roomId = bookings.roomId, checkInDate = bookings.checkInDate.ToShortDateString(), 
                            checkOutDate = bookings.checkOutDate.ToShortDateString(), firstName = guests.firstName, lastName = guests.lastName })
                        .OrderBy(booking => booking.roomId);

                GridReservasjoner.DataContext = viewData;
            }
            catch (NullReferenceException)
            {
            }
            catch (ArgumentNullException)
            {
            }
            GridChangeRoom.Visibility = Visibility.Hidden;
            okButton.Visibility = Visibility.Hidden;
            cancelButton.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Open new WPF window for new reservation, and creates an event to updateRequired function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewReservation_Clicked(object sender, RoutedEventArgs e)
        {
            NyReservasjon ny = new NyReservasjon();
            ny.evl += new DoWhenTick(updateRequired);
            ny.ShowDialog();
        }
        /// <summary>
        /// Delete order from database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Reservations delete = (Reservations)GridReservasjoner.SelectedItems[0];
                string deleteString = "Do you want to delete this?\n" + delete.checkInDate + "->" + delete.checkOutDate + "\nName: " + delete.firstName + " " + delete.lastName + "\nRoom: " + delete.roomId;
                MessageBoxResult confirm = MessageBox.Show(deleteString, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (confirm == MessageBoxResult.Yes)
                {
                    controller.deleteReservation(delete.bookingId);
                    init();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }
        /// <summary>
        /// Creates view for changing room, and get's available rooms from database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                Reservations change = (Reservations)GridReservasjoner.SelectedItems[0];
                newBooking = controller.getBooking().Where(order => order.bookingId == change.bookingId).FirstOrDefault();
                DateTime checkIn = newBooking.checkInDate;
                DateTime checkOut = newBooking.checkOutDate;
                var viewObject = controller.getRoom().Select(room => new selectedRoom() { bed = room.bed, roomId = room.roomId, 
                    Bookings = room.Bookings, price = room.price, quality = room.quality })
                    .Where(room => room.Bookings.First() == null  || room.Bookings.Any(booking => (checkIn > booking.checkOutDate) || 
                        (checkOut < booking.checkInDate) ))
                    .Where(room => room.quality == newBooking.Room.quality)
                    .Where(room => room.bed == newBooking.Room.bed)
                    .Where(room => room.roomId != newBooking.roomId).OrderBy(room => room.roomId);
                GridChangeRoom.DataContext = viewObject;

                okButton.Visibility = Visibility.Visible;
                cancelButton.Visibility = Visibility.Visible;
                GridChangeRoom.Visibility = Visibility.Visible;
            }
            catch (ArgumentOutOfRangeException )
            {
                MessageBoxResult error = MessageBox.Show("Please select a booking first!", "Error");
            }
        }
        /// <summary>
        /// Close changeroom view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            init();
        }
        /// <summary>
        /// Changing room of order to selected room.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeRoom_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedRoom change = (selectedRoom)GridChangeRoom.SelectedItems[0];
                controller.changeRoom(change.roomId, newBooking.bookingId);
                init();
            }
            catch (ArgumentOutOfRangeException )
            {
                MessageBoxResult error = MessageBox.Show("No room selected.\nPress cancel or select a room!", "Error");
            }
        }
        /// <summary>
        /// Empties textbox each time it got focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchField.Text = "";
        }
        /// <summary>
        /// Gets data with searchtext as clause.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Clicked(object sender, RoutedEventArgs e)
        {
            try{
                if (SearchField.Text == "")
                {
                    init();
                }
                else
                {
                    Table<HotellDLL.Booking> bookingList = controller.getBooking();
                    Table<HotellDLL.Guest> guestList = controller.getGuest();
                    var viewData = bookingList.Select(bookings
                        => new { bookings.Guest, bookings.bookingId, bookings.guestId, bookings.roomId, bookings.checkInDate, bookings.checkOutDate })
                        .Where(bookings => bookings.Guest.lastName.Contains(SearchField.Text))
                        .Join(guestList, bookings => bookings.guestId, guests => guests.guestId, (bookings, guests)
                            => new Reservations { bookingId = bookings.bookingId, roomId = bookings.roomId, checkInDate = bookings.checkInDate.ToShortDateString(), checkOutDate = bookings.checkOutDate.ToShortDateString(), firstName = guests.firstName, lastName = guests.lastName })
                            .OrderBy(booking => booking.roomId);
                    GridReservasjoner.DataContext = viewData;
                }
            }catch(Exception ){
                MessageBoxResult error = MessageBox.Show("Error searching. Please enter room number!", "Error");
            }
        }
        /// <summary>
        /// This get's called from event in NyReservasjon if updating window because of new reservation is nessessary.
        /// </summary>
        private void updateRequired()
        {
            init();
        }
    }
}
