using System;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace HotellDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DesktopController desktopController;

        public MainWindow()
        {
            InitializeComponent();
            desktopController = new DesktopController();
            updateListView();
        }

        /// <summary>
        /// Updates the listview with the search criteria and date selection
        /// </summary>
        private void updateListView()
        {

            //Table<HotellDLL.Booking> bookings = desktopController.getBooking();
            //if (bookings != null)
            //{
            //    var bookingslist = bookings.Select(booking => new { booking.roomId, booking.guestId, booking.checkedIn, booking.checkInDate })
            //        .Join(desktopController.getGuest(), booking => booking.guestId, guest => guest.guestId, (booking, guest)
            //            => new { booking.roomId, guest.firstName, guest.lastName, checkedIn = bitToString(booking.checkedIn), booking.checkInDate })
            //            ;

            //    var list = bookingslist.Select(booking => new { booking.checkedIn, booking.firstName, booking.lastName, booking.roomId, booking.checkInDate })
            //        .Join(desktopController.getService(), service => service.roomId, booking => booking.roomId, (booking, service) =>
            //            new { booking.checkedIn, booking.firstName, booking.lastName, booking.roomId, notes = checkNotes(service.note), booking.checkInDate }
            //            ).Where(booking => booking.checkInDate >= datePicker.DisplayDate).OrderBy(booking => booking.checkInDate);

            //    listView.DataContext = list;
            //}

            Table<HotellDLL.Room> rooms = desktopController.getRoom();

            if (rooms != null)
            {

            }

        }


        private string checkNotes(String note)
        {
            if (note != null || note != "")
            {
                return "!";
            }
            else return "";
        }

        /// <summary>
        /// Method for converting bit value to string. 0 return No, 1 returns Yes
        /// </summary>
        /// <param name="bit"> the bit to be converted</param>
        /// <returns>No or Yes depending on the bit value</returns>
        private string bitToString(bool bit)
        {
            string answer = "";

            if (bit)
            {
                answer = "Yes";
            }
            else
            {
                answer = "No";
            }

            return answer;
        }

        /// <summary>
        /// Opens the window for reservations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newReservation_Click(object sender, RoutedEventArgs e)
        {
            Reservasjoner reservasjoner = new Reservasjoner();
            reservasjoner.Show();
        }
        /// <summary>
        /// Method that leaves the textbox blank when it gets focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            searchBox.Text = "";
 
        }

        /// <summary>
        /// Method for updating the listview based on the search and date selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            Table<HotellDLL.Booking> bookings = desktopController.getBooking();

            if (bookings != null)
            {
                var bookingslist = bookings.Select(booking => new { booking.Guest.lastName, booking.checkInDate, booking })
                    .Where(booking => booking.lastName.Contains(searchBox.Text))
                    .Where(booking => booking.checkInDate >= datePicker.DisplayDate)
                    .Join(desktopController.getService(), booking => booking.booking.roomId, service => service.roomId, (booking, service)
                        => new { booking.booking.roomId,booking.booking.Guest.firstName,booking.booking.Guest.lastName,booking.booking.checkedIn, notes = checkNotes(service.note) })
                        ;
            }
            

        }
    
        /// <summary>
        /// Dummy method for viewing the roomView, and must be modified laiter when the list in mainwindow is populated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RoomView roomView = new RoomView();
            roomView.Show();
        }
    }
}
