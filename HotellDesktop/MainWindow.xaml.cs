using System;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Diagnostics;

namespace HotellDesktop
{

    public class listViewClass
    {
        public int roomId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool checkedIn { get; set; }
        public string notes { get; set; }    
    }


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

            Table<HotellDLL.Room> roomTable = desktopController.getRoom();
            Table<HotellDLL.Booking> bookinTable = desktopController.getBooking();

            if (roomTable != null)
            {
                var roomsAndReservations =
                    from rooms in roomTable
                    join booking in bookinTable on rooms.roomId equals booking.roomId into roomsAndReservation
                    from book in roomsAndReservation.DefaultIfEmpty()
                    select new listViewClass()  { roomId = rooms.roomId, firstName = book.Guest.firstName,lastName = book.Guest.lastName,
                        checkedIn = (book.checkedIn == null ? false : book.checkedIn), notes =(rooms.Services.First().note != null ? "!" : "") };
                roomListView.DataContext = roomsAndReservations;
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
           Table<HotellDLL.Room> roomTable = desktopController.getRoom();
           Table<HotellDLL.Booking> bookinTable = desktopController.getBooking();

                if (roomTable != null)
                {
                    var roomsAndReservations =
                        from rooms in roomTable
                        join booking in bookinTable on rooms.roomId equals booking.roomId into roomsAndReservation
                        from book in roomsAndReservation.DefaultIfEmpty()
                        where book.Guest.lastName.Contains(searchBox.Text.ToLower())
                        select new listViewClass()
                        {
                            roomId = rooms.roomId,
                            firstName = book.Guest.firstName,
                            lastName = book.Guest.lastName,
                            checkedIn = (book.checkedIn == null ? false : book.checkedIn),
                            notes = (rooms.Services.First().note != null ? "!" : "")
                        };
                    roomListView.DataContext = roomsAndReservations;
                }
        }
    
        /// <summary>
        /// Dummy method for viewing the roomView, and must be modified laiter when the list in mainwindow is populated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            

            var selectedItem = (listViewClass)roomListView.SelectedItems[0];

            if (selectedItem != null)
            {
                int id = selectedItem.roomId;
                RoomView roomView = new RoomView(id);
                roomView.Show();
            }
            else
            {

                RoomView roomView = new RoomView();
                roomView.Show();
            }
        }
    }


    

}
