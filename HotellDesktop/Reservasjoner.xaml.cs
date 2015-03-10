using System.Linq;
using System.Data.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Diagnostics;
using System;

namespace HotellDesktop
{
    public class Reservations
    {
        public int bookingId { get; set; }
        public int roomId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime checkInDate { get; set; }
        public DateTime checkOutDate { get; set; }
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
        /// Create objects to show.
        /// </summary>
        public void init()
        {
            try
            {
                Table<HotellDLL.Booking> bookingList = controller.getBooking();
                Table<HotellDLL.Guest> guestList = controller.getGuest();
                var viewData = bookingList.Select(bookings
                    => new { bookings.bookingId, bookings.guestId, bookings.roomId, bookings.checkInDate, bookings.checkOutDate })
                    .Join(guestList, bookings => bookings.guestId, guests => guests.guestId, (bookings, guests)
                        => new Reservations{ bookingId = bookings.bookingId, roomId = bookings.roomId, checkInDate = bookings.checkInDate, checkOutDate = bookings.checkOutDate, firstName = guests.firstName, lastName = guests.lastName });
                GridReservasjoner.DataContext = viewData;
            }
            catch (System.NullReferenceException)
            {
                Debug.Print("No data in tables. NullReferenceException.");
            }
            catch (System.ArgumentNullException)
            {
                Debug.Print("No data in tables. ArgumentNullException.");
            }
            GridChangeRoom.Visibility = Visibility.Hidden;
            GridReservasjoner.Margin = new Thickness(10,10,140,10);
            okButton.Visibility = Visibility.Hidden;
            cancelButton.Visibility = Visibility.Hidden;
            borderBottom.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NyReservasjon ny = new NyReservasjon();
            ny.evl += new DoWhenTick(updateRequired);
            ny.Show();
            init();
        }

        private void delete_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                Reservations delete = (Reservations)GridReservasjoner.SelectedItems[0];
                string deleteString = "Do you want to delete this?\n" + delete.checkInDate.ToShortDateString() + "->" + delete.checkOutDate.ToShortDateString() + "\nName: " + delete.firstName + " " + delete.lastName + "\nRoom: " + delete.roomId;
                MessageBoxResult confirm = MessageBox.Show(deleteString, "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (confirm == MessageBoxResult.Yes)
                {
                    controller.deleteReservation(delete.bookingId);
                    init();
                }
            }
            catch (ArgumentOutOfRangeException e1)
            {

            }
        }

        private void chRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                Reservations change = (Reservations)GridReservasjoner.SelectedItems[0];
                newBooking = controller.getBooking().Where(order => order.bookingId == change.bookingId).FirstOrDefault();
                DateTime checkIn = newBooking.checkInDate;
                DateTime checkOut = newBooking.checkOutDate;
                var viewObject = controller.getRoom().Select(room => new selectedRoom() { bed = room.bed, roomId = room.roomId, Bookings = room.Bookings, price = room.price, quality = room.quality })
                    .Where(room => room.Bookings.First() == null  || room.Bookings.Any(booking => (checkIn > booking.checkInDate && checkOut > booking.checkInDate) || (checkIn < booking.checkOutDate && checkOut < booking.checkOutDate)))
                    .Where(room => room.quality == newBooking.Room.quality)
                    .Where(room => room.bed == newBooking.Room.bed);
                GridChangeRoom.DataContext = viewObject;

                GridReservasjoner.Margin = new Thickness(10, 10, 140, 140);
                okButton.Visibility = Visibility.Visible;
                cancelButton.Visibility = Visibility.Visible;
                GridChangeRoom.Visibility = Visibility.Visible;
                borderBottom.Visibility = Visibility.Visible;
            }
            catch (ArgumentOutOfRangeException e1)
            {
                MessageBoxResult error = MessageBox.Show("No rooms available", "Error");
            }
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            init();
        }

        private void ChangeRoom_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedRoom change = (selectedRoom)GridChangeRoom.SelectedItems[0];
                controller.changeRoom(change.roomId, newBooking.bookingId);
                init();
            }
            catch (ArgumentOutOfRangeException e1)
            {
                MessageBoxResult error = MessageBox.Show("No room selected.\nPress cancel or select a room!", "Error");
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchField.Text = "";
        }

        private void Search_Clicked(object sender, RoutedEventArgs e)
        {
            try{
                if (SearchField.Text == "")
                {
                    init();
                }
                else
                {
                    int room = Convert.ToInt32(SearchField.Text);
                    Table<HotellDLL.Booking> bookingList = controller.getBooking();
                    Table<HotellDLL.Guest> guestList = controller.getGuest();
                    var viewData = bookingList.Select(bookings
                        => new { bookings.bookingId, bookings.guestId, bookings.roomId, bookings.checkInDate, bookings.checkOutDate })
                        .Where(bookings => bookings.roomId.ToString().Contains(room.ToString()))
                        .Join(guestList, bookings => bookings.guestId, guests => guests.guestId, (bookings, guests)
                            => new Reservations { bookingId = bookings.bookingId, roomId = bookings.roomId, checkInDate = bookings.checkInDate, checkOutDate = bookings.checkOutDate, firstName = guests.firstName, lastName = guests.lastName });
                    GridReservasjoner.DataContext = viewData;
                }
            }catch(FormatException){
                MessageBoxResult error = MessageBox.Show("Error searching. Please enter room number!", "Error");
            }
        }
        private void updateRequired()
        {
            init();
        }
    }
}
