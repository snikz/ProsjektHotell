using System;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Documents;
using HotellDesktop;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

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
        public NyReservasjon()
        {
            checkIn = null;
            checkOut = null;
            InitializeComponent();
            controller = new DesktopController();
            for (int i = 1; i <= 5; i++)
            {
                Quality.Items.Add(i);
                Bed.Items.Add(i);
            }
            Quality.SelectedIndex = 0;
            Bed.SelectedIndex = 0;

            
        }


        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;
            if (date != null)
            {
                checkIn = (DateTime)date;
            }
        }

        private void DatePicker_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;
            if (date != null)
            {
                checkOut = (DateTime)date;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (checkIn != null || checkOut != null)
            {
                int beds = (int)Bed.SelectedItem;
                int quality = (int)Quality.SelectedItem;
                Table<HotellDLL.Room> roomTable = controller.getRoom();
                var viewObject = roomTable.Select(room => new selectedRoom() { bed = room.bed, roomId = room.roomId, Bookings = room.Bookings, price = room.price, quality = room.quality })
                    .Where(room => room.Bookings.First() == null || room.Bookings.Any(booking => (checkIn > booking.checkInDate && checkOut > booking.checkInDate) || (checkIn < booking.checkOutDate && checkOut < booking.checkOutDate)))
                    .Where(room => room.quality == quality)
                    .Where(room => room.bed == beds).FirstOrDefault();
                RoomView.Items.Clear();
                RoomView.Items.Add(viewObject);
            }
            else
            {
                MessageBoxResult message = MessageBox.Show("Please choose checkin/out", "Error"); ;
            }
        }
        private bool isBusy(EntitySet<HotellDLL.Booking> bookings)
        {
            HotellDLL.Booking current;
            int size = bookings.Count;
            for (int i = 0; i < size; i++)
            {
                current = bookings.ElementAt(i);
                if (current.checkOutDate > checkIn && current.checkInDate < checkIn)
                    return false;
                else if (current.checkOutDate > checkOut && current.checkInDate < checkOut)
                    return false;
            }
            return true;
        }

        private void NewReservation_Clicked(object sender, RoutedEventArgs e)
        {
            if ((bool)New.IsChecked)
            {
                HotellDLL.Guest newGuest = new HotellDLL.Guest();
                newGuest.firstName = firstName.Text;
                newGuest.lastName = lastName.Text;
                newGuest.email = ePost.Text;
                newGuest.password = password.Text;
                controller.addUser(newGuest);

                selectedRoom item = (selectedRoom)RoomView.SelectedItems[0];

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
            }
            else
            {
                selectedRoom item = (selectedRoom)RoomView.SelectedItems[0];
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
            }
            evl();
            this.Close();
        }

        private void firstName_Focus(object sender, RoutedEventArgs e)
        {
            firstName.Text = "";
        }

        private void lastName_Focus(object sender, RoutedEventArgs e)
        {
            lastName.Text = "";
        }

        private void ePost_Focus(object sender, RoutedEventArgs e)
        {
            ePost.Text = "";
        }

        private void Password_Focus(object sender, RoutedEventArgs e)
        {
            password.Text = "";
        }

        private void firstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (firstName.Text == "")
                firstName.Text = "Firstname";
        }

        private void lastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (lastName.Text == "")
                lastName.Text = "Lastname";
        }

        private void ePost_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ePost.Text == "")
                ePost.Text = "Epost";
        }

        private void password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (password.Text == "")
                password.Text = "Password";
        }

        private void New_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                firstName.IsEnabled = true;
                lastName.IsEnabled = true;
                ePost.IsEnabled = true;
                password.IsEnabled = true;
                userList.IsEnabled = false;
            }
            catch (NullReferenceException e1)
            {
                Debug.Print("SOMETHING WRONG HAPPEND");
            }
        }

        private void Old_Checked(object sender, RoutedEventArgs e)
        {
            try { 
                var userVar = controller.getGuest().Select(guest => new user{ guestId = guest.guestId, firstName = guest.firstName, lastName = guest.lastName, email = guest.email, password = guest.password });
                userList.DataContext = userVar;

                firstName.IsEnabled = false;
                lastName.IsEnabled = false;
                ePost.IsEnabled = false;
                password.IsEnabled = false;
                userList.IsEnabled = true;
            }
            catch (NullReferenceException e1)
            {
                Debug.Print("SOMETHING WRONG HAPPEND");
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}
