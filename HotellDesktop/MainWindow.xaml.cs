using System;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Diagnostics;

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
        /// Updates the listview with the date selection
        /// </summary>
        private void updateListView()
        {

            Table<HotellDLL.Room> roomTable = desktopController.getRoom();
            Table<HotellDLL.Booking> bookings = (Table<HotellDLL.Booking>)desktopController.getBooking();

            if (datePicker.SelectedDate == null)
            {
                datePicker.SelectedDate = datePicker.DisplayDate;
            }

            var bookingsToday = bookings.Where(book => book.checkInDate <= datePicker.SelectedDate && book.checkOutDate >= datePicker.SelectedDate);

            if (roomTable != null)
            {
                var roomsAndReservations =
                    from rooms in roomTable
                    join booking in bookingsToday on rooms.roomId equals booking.roomId into roomsAndRes
                    from book in roomsAndRes.DefaultIfEmpty()
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
        /// Opens the window for reservations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newReservation_Click(object sender, RoutedEventArgs e)
        {
            Reservasjoner reservasjoner = new Reservasjoner();
            reservasjoner.Closed += reservasjoner_Closed;
            reservasjoner.ShowDialog();
        }

        void reservasjoner_Closed(object sender, EventArgs e)
        {
            updateListView();
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

            if (searchBox.Text.Equals(""))
            {
                updateListView();
            }
            else
            {

                if ((bool)radioLastname.IsChecked)
                {

                    Table<HotellDLL.Room> roomTable = desktopController.getRoom();
                    Table<HotellDLL.Booking> bookings = (Table<HotellDLL.Booking>)desktopController.getBooking();

                    if (datePicker.SelectedDate == null)
                    {
                        datePicker.SelectedDate = datePicker.DisplayDate;
                    }

                    var bookingsToday = bookings.Where(book => book.checkInDate <= datePicker.SelectedDate && book.checkOutDate >= datePicker.SelectedDate);

                    if (roomTable != null)
                    {
                        var roomsAndReservations =
                            from rooms in roomTable
                            join booking in bookingsToday on rooms.roomId equals booking.roomId into roomsAndRes
                            from book in roomsAndRes.DefaultIfEmpty()
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

                else
                {
                    Table<HotellDLL.Room> roomTable = desktopController.getRoom();
                    Table<HotellDLL.Booking> bookings = (Table<HotellDLL.Booking>)desktopController.getBooking();

                    if (datePicker.SelectedDate == null)
                    {
                        datePicker.SelectedDate = datePicker.DisplayDate;
                    }

                    var bookingsToday = bookings.Where(book => book.checkInDate <= datePicker.SelectedDate && book.checkOutDate >= datePicker.SelectedDate);

                    if (roomTable != null)
                    {
                        var roomsAndReservations =
                            from rooms in roomTable
                            join booking in bookingsToday on rooms.roomId equals booking.roomId into roomsAndRes
                            from book in roomsAndRes.DefaultIfEmpty()
                            where rooms.roomId.ToString().Equals(searchBox.Text)
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
            }
        }
    
        /// <summary>
        /// Opens the roomView based on the selected room
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            try
            {
                var selectedItem = (listViewClass)roomListView.SelectedItems[0];

                if (selectedItem != null)
                {
                    int id = selectedItem.roomId;
                    RoomView roomView = new RoomView(id);
                    roomView.changes += new update(updateRequired);
                    roomView.ShowDialog();
                }
                else
                {

                    RoomView roomView = new RoomView();
                    roomView.changes += new update(updateRequired);
                    roomView.ShowDialog();
                }
            }

            catch(ArgumentOutOfRangeException){
                MessageBox.Show("Doble click on a room", "error");
            }
        }

        private void updateRequired()
        {
            updateListView();
        }

    
        private void datePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            updateListView();
        }

        private void todayDateButton_Click(object sender, RoutedEventArgs e)
        {
            datePicker.SelectedDate = DateTime.Today;
            
            updateListView();
        }

        private void searchBoxSetText()
        {
            
                if ((bool)radioLastname.IsChecked)
                {
                    searchBox.Text = "Search lastname";
                }
                else
                {
                    searchBox.Text = "Search room number";
                }

            
        }

        private void radioRoomNumber_Click(object sender, RoutedEventArgs e)
        {
            searchBoxSetText();
        }

        private void radioLastname_Click(object sender, RoutedEventArgs e)
        {
            searchBoxSetText();
        }
    }

    public class listViewClass
    {
        public int roomId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool checkedIn { get; set; }
        public string notes { get; set; }

    }


}
