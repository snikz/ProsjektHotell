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
        /// Updates the listview with the date selection
        /// </summary>
        private void updateListView()
        {

            Table<HotellDLL.Room> roomTable = desktopController.getRoom(); // all rooms
            Table<HotellDLL.Booking> bookings = (Table<HotellDLL.Booking>)desktopController.getBooking(); // all bookings

            //if a date has not been selected, set to todays date
            if (datePicker.SelectedDate == null)
            {
                datePicker.SelectedDate = datePicker.DisplayDate;
            }

            //all bookings that are ongoing on todays date
            var bookingsToday = bookings.Where(book => book.checkInDate <= datePicker.SelectedDate && book.checkOutDate >= datePicker.SelectedDate);

            //all rooms and match with todays bookings
            if (roomTable != null)
            {
                var roomsAndReservations =
                    from rooms in roomTable
                    join booking in bookingsToday on rooms.roomId equals booking.roomId into roomsAndRes
                    from book in roomsAndRes.DefaultIfEmpty() // return default if empty
                    select new listViewClass()
                    {
                        roomId = rooms.roomId,
                        firstName = book.Guest.firstName,
                        lastName = book.Guest.lastName,
                        checkedIn = (book.checkedIn == null ? false : book.checkedIn),
                        checkedInString = book.checkedIn == null ? "" : book.checkedIn == false ? "No" : "Yes",
                        notes = (rooms.Services.First().note != null ? "!" : ""),
                        bookingId = (book.bookingId == null ?  0: book.bookingId),
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
            reservasjoner.Closed += reservasjoner_Closed;//event that occurs when Reservasjoner is closed
            reservasjoner.ShowDialog(); //will only return when the window is closed
        }

        /// <summary>
        /// Occurs when Reservasjon window is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                if ((bool)radioLastname.IsChecked) //search for lastname
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
                                
                                notes = (rooms.Services.First().note != null ? "!" : ""),
                                bookingId = (book.bookingId == null ? 0 : book.bookingId),
                            };

                        roomListView.DataContext = roomsAndReservations;

                    }
                }

                else //search for room number
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
                                notes = (rooms.Services.First().note != null ? "!" : ""),
                                bookingId = (book.bookingId == null ? 0 : book.bookingId),
                            };

                        roomListView.DataContext = roomsAndReservations;

                    }
                }
            }
        }
    
        /// <summary>
        /// Calls selectRoom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            selectRoom();
        }

        /// <summary>
        /// Opens the roomView based on the selected room
        /// </summary>
        private void selectRoom()
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

            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Select a room from the list", "Error");
            }
        }

        /// <summary>
        /// Calls updateListView()
        /// </summary>
        private void updateRequired()
        {
            updateListView();
        }

    
        /// <summary>
        /// Occurs when selections in datepicker has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            updateListView();
        }

        /// <summary>
        /// Sets datepicker to todays day
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void todayDateButton_Click(object sender, RoutedEventArgs e)
        {
            datePicker.SelectedDate = DateTime.Today;
            
            updateListView();
        }

        /// <summary>
        /// Refresh the textbox based on radiobuttons choice
        /// </summary>
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

        /// <summary>
        /// Occurs when radiobutton is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioRoomNumber_Click(object sender, RoutedEventArgs e)
        {
            searchBoxSetText();
        }
        /// <summary>
        /// occurs when radiobutton is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioLastname_Click(object sender, RoutedEventArgs e)
        {
            searchBoxSetText();
        }

        /// <summary>
        /// Check in a reservation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var selectedRoom = (listViewClass)roomListView.SelectedItems[0];
                if (selectedRoom.bookingId != 0)
                {
                    if (selectedRoom.checkedIn == false)
                    {
                        desktopController.checkIn(selectedRoom.bookingId);
                        updateListView();
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Velg et rom", "Error");
            }
        }

        /// <summary>
        /// Reservation check out, and delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkOutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var selectedRoom = (listViewClass)roomListView.SelectedItems[0];
                if (selectedRoom.bookingId != 0)
                {
                    if (selectedRoom.checkedIn == true)
                    {

                    desktopController.checkOut(selectedRoom.bookingId);

                    HotellDLL.Service service = new HotellDLL.Service();
                    service.type = 0;
                    service.roomId = selectedRoom.roomId;
                    service.status = 0;
                    service.note = "Auto generated cleaning note";

                    desktopController.addService(service);

                    updateListView();
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Velg et rom", "Error");
            }
        }

        private void selectRoomButton_Click(object sender, RoutedEventArgs e)
        {
            selectRoom();
        }
    }

    /// <summary>
    /// Class for view in listView
    /// </summary>
    public class listViewClass
    {
        public int roomId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool checkedIn { get; set; }

        public string checkedInString { get; set; }
        public string notes { get; set; }
        public int bookingId { get; set; }

    }


}
