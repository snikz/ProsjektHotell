using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Linq;

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
           string checkedIn;

                Table<HotellDLL.Booking> bookings = desktopController.getBooking();
                if (bookings != null)
                {
                    var bookingslist = bookings.Select(booking => new { booking.roomId, booking.guestId, booking.checkedIn })
                        .Join(desktopController.getGuest(), booking => booking.guestId, guest => guest.guestId, (booking, guest)
                            => new { booking.roomId, guest.firstName, guest.lastName,checkedIn = bitToString(booking.checkedIn) })
                            //.Join(desktopController.getService(), service => service.roomId, booking => booking.roomId, (service, booking, guest) =>
                            //new { guest.})
                            ;

                    var list = bookingslist.Select(booking => new { booking.checkedIn, booking.firstName, booking.lastName, booking.roomId })
                        .Join(desktopController.getService(), service => service.roomId, booking => booking.roomId,(booking,service) => 
                            new {booking.checkedIn, booking.firstName, booking.lastName, booking.roomId, note = checkNotes(service.note)});

                    listView.DataContext = list;
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

        private void newReservation_Click(object sender, RoutedEventArgs e)
        {
            Reservasjoner reservasjoner = new Reservasjoner();
            reservasjoner.Show();
        }
    }
}
