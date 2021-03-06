﻿using System;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace HotellDesktop
{
    public class selectedRoom
    {
        public int roomId { get; set; }
        public int bed { get; set; }
        public int price { get; set; }
        public EntitySet<HotellDLL.Booking> Bookings { get; set; }
        public int quality { get; set; }

    }
    public class user
    {
        public int guestId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
    /// <summary>
    /// Delegate to handling event.
    /// </summary>
    public delegate void DoWhenTick();
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NyReservasjon : Window
    {

        DateTime? checkIn;
        DateTime? checkOut;
        public event DoWhenTick evl;
        DesktopController controller;
        /// <summary>
        /// Constructor to initialize variables and window.
        /// </summary>
        public NyReservasjon()
        {
            InitializeComponent();
            controller = new DesktopController();
            for (int i = 1; i <= 5; i++)
            {
                Quality.Items.Add(i);
                Bed.Items.Add(i);
            }
            Quality.SelectedIndex = 0;
            Bed.SelectedIndex = 0;

            checkOutDate.SelectedDate = checkOutDate.DisplayDate;
            checkInDate.SelectedDate = checkInDate.DisplayDate;
            checkIn = checkInDate.SelectedDate;
            checkOut = checkOutDate.SelectedDate;
            
        }
        /// <summary>
        /// If date is changed, clear roomlist(maybe autoupdate?), and set's selected date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkIn_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            RoomView.Items.Clear();
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;
            if (date != null)
            {
                checkIn = (DateTime)date;
            }
        }
        /// <summary>
        /// If date is changed, clear roomlist(maybe autoupdate?), and set's selected date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkOut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            RoomView.Items.Clear();
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;
            if (date != null)
            {
                checkOut = (DateTime)date;
            }
        }
        /// <summary>
        /// If number of beds is changed, clear roomlist(maybe autoupdate?).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RoomView.Items.Clear();
        }
        /// <summary>
        /// If number of beds is changed, clear roomlist(maybe autoupdate?).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RoomView.Items.Clear();
        }
        /// <summary>
        /// Finds available rooms with search terms.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindRoom_Clicked(object sender, RoutedEventArgs e)
        {
            if (checkIn >= checkOut)
            {
                MessageBoxResult error = MessageBox.Show("Checkout date have to be bigger than checkin date!", "Error");
            }
            else if (checkIn != null && checkOut != null)
            {
                try
                {
                    int beds = (int)Bed.SelectedItem;
                    int quality = (int)Quality.SelectedItem;
                    Table<HotellDLL.Room> roomTable = controller.getRoom();
                    var viewObject = roomTable.Select(room => new selectedRoom() { bed = room.bed, roomId = room.roomId, Bookings = room.Bookings, price = room.price, quality = room.quality })
                        .Where(room => room.Bookings.First() == null || room.Bookings.Any(booking => (checkIn > booking.checkOutDate) || (checkOut < booking.checkInDate)))
                        .Where(room => room.quality == quality)
                        .Where(room => room.bed == beds).First();
                    RoomView.Items.Clear();
                    RoomView.Items.Add(viewObject);
                }
                catch (InvalidOperationException)
                {
                    MessageBoxResult noRooms = MessageBox.Show("No rooms found!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBoxResult message = MessageBox.Show("Please choose checkin/out", "Error"); ;
            }
        }
        /// <summary>
        /// Creates new reservation in database from given data, and sends an event to 
        /// Reservasjon so it can get new data from controller.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewReservation_Clicked(object sender, RoutedEventArgs e)
        {

            try
            {
                selectedRoom item = (selectedRoom)RoomView.SelectedItems[0];
                if ((bool)New.IsChecked)
                {
                    try
                    {
                        HotellDLL.Guest newGuest = new HotellDLL.Guest();
                        newGuest.firstName = firstName.Text;
                        newGuest.lastName = lastName.Text;
                        newGuest.email = ePost.Text;
                        newGuest.password = ePost.Text;
                        controller.addUser(newGuest);

                        DateTime sendIn = (DateTime)checkIn;
                        DateTime sendOut = (DateTime)checkOut;
                        HotellDLL.Booking newBooking = new HotellDLL.Booking();
                        newBooking.checkedIn = false;
                        newBooking.checkedOut = false;
                        newBooking.checkInDate = sendIn;
                        newBooking.checkOutDate = sendOut;
                        newBooking.guestId = newGuest.guestId;
                        newBooking.roomId = item.roomId;
                        controller.addReservation(newBooking);

                        evl();
                        this.Close();
                    }
                    catch (ArgumentOutOfRangeException )
                    {
                        MessageBoxResult error = MessageBox.Show("Please choose user or create a new one.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }

                else
                {
                    try
                    {
                        user tempUser = (user)userList.SelectedItems[0];
                        HotellDLL.Guest oldGuest = controller.getGuest().Where(guest => guest.guestId == tempUser.guestId).FirstOrDefault();
                        DateTime sendIn = (DateTime)checkIn;
                        DateTime sendOut = (DateTime)checkOut;
                        HotellDLL.Booking newBooking = new HotellDLL.Booking();
                        newBooking.checkedIn = false;
                        newBooking.checkedOut = false;
                        newBooking.checkInDate = sendIn;
                        newBooking.checkOutDate = sendOut;
                        newBooking.guestId = oldGuest.guestId;
                        newBooking.roomId = item.roomId;
                        controller.addReservation(newBooking);

                        evl();
                        this.Close();
                    }
                    catch (ArgumentOutOfRangeException )
                    {
                        MessageBoxResult error = MessageBox.Show("Please choose user or create a new one.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
            catch (ArgumentOutOfRangeException )
            {
                MessageBoxResult error = MessageBox.Show("Please choose a room!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }


        }
        /// <summary>
        /// Clears textbox when got focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void firstName_Focus(object sender, RoutedEventArgs e)
        {
            firstName.Text = "";
        }
        /// <summary>
        /// Clears textbox when got focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lastName_Focus(object sender, RoutedEventArgs e)
        {
            lastName.Text = "";
        }
        /// <summary>
        /// Clears textbox when got focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ePost_Focus(object sender, RoutedEventArgs e)
        {
            ePost.Text = "";
        }
        /// <summary>
        /// If textbox is empty when lost focus. Default back to original text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void firstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (firstName.Text == "")
                firstName.Text = "Firstname";
        }
        /// <summary>
        /// If textbox is empty when lost focus. Default back to original text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (lastName.Text == "")
                lastName.Text = "Lastname";
        }
        /// <summary>
        /// If textbox is empty when lost focus. Default back to original text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ePost_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ePost.Text == "")
                ePost.Text = "Epost";
        }
        /// <summary>
        /// Enables/disable fields for creating new user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void New_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                firstName.IsEnabled = true;
                lastName.IsEnabled = true;
                ePost.IsEnabled = true;
                userList.IsEnabled = false;
            }
            catch (NullReferenceException)
            {
            }
        }
        /// <summary>
        /// Enables/disables fields for current user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Old_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var userVar = controller.getGuest().Select(guest => new user { guestId = guest.guestId, firstName = guest.firstName, lastName = guest.lastName, email = guest.email, password = guest.password });
                userList.DataContext = userVar;

                firstName.IsEnabled = false;
                lastName.IsEnabled = false;
                ePost.IsEnabled = false;
                userList.IsEnabled = true;
            }
            catch (NullReferenceException)
            {
            }

        }
        /// <summary>
        /// Close window without changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
