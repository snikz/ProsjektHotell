using System;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Documents;
using HotellDesktop;

namespace HotellDesktop
{
    public class selectedRoom
    {
        public int roomId { get; set; }
        public int bed { get; set; }
        public int price { get; set; }
    }
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NyReservasjon : Window
    {
        
        DateTime checkIn;
        DateTime checkOut;
        DesktopController controller;
        public NyReservasjon()
        {
            InitializeComponent();
            checkIn = new DateTime();
            checkOut = new DateTime();
            controller = new DesktopController();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            checkIn = checkInDate.DisplayDate;
        }

        private void DatePicker_SelectedDateChanged_1(object sender, SelectionChangedEventArgs e)
        {
            checkOut = checkOutDate.DisplayDate;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Table<HotellDLL.Room> roomTable = controller.getRoom();
            Debug.Print("HEI");
            Table<HotellDLL.Booking> bookingTable = controller.getBooking();
            var viewData = roomTable.Select(room => new selectedRoom() { roomId=(int)room.roomId, bed=(int)room.bed, price=(int)room.price });
                //.Join(bookingTable, room => room.roomId, booking => booking.roomId, (room, booking)
                //=> new { room.roomId, room.bed, room.price, booking.checkInDate, booking.checkOutDate })
                //.Where(room => room.checkInDate > checkOut)
                //.Where(room => room.checkOutDate < checkIn);
            RoomView.DataContext = viewData;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            HotellDLL.Guest newGuest = new HotellDLL.Guest();
            newGuest.firstName = firstName.Text;
            newGuest.lastName = lastName.Text;
            newGuest.email = ePost.Text;
            newGuest.password = password.Text;
            controller.addUser(newGuest);

            selectedRoom item = (selectedRoom)RoomView.SelectedItems[0];

            HotellDLL.Booking newBooking = new HotellDLL.Booking();
            newBooking.checkedIn = false;
            newBooking.checkedOut = false;
            newBooking.checkInDate = checkInDate.DisplayDate;
            newBooking.checkOutDate = checkOutDate.DisplayDate;
            newBooking.guestId = newGuest.guestId;
            newBooking.roomId = item.roomId;
            controller.addReservation(newBooking);

            this.Close();
        }
    }
}
