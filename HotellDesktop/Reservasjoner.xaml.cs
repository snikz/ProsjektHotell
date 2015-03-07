using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using HotellDesktop;

namespace HotellDesktop
{
    /// <summary>
    /// Interaction logic for Reservasjoner.xaml
    /// </summary>
    public partial class Reservasjoner : Window
    {
        DesktopController controller;
        
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
                    => new { bookings.guestId, bookings.roomId, bookings.checkInDate, bookings.checkOutDate })
                    .Join(guestList, bookings => bookings.guestId, guests => guests.guestId, (bookings, guests)
                        => new { bookings.roomId, bookings.checkInDate, bookings.checkOutDate, guests.firstName, guests.lastName });
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
            //var test = dx.students.Select(stud => new { stud.studentname, stud.id })
            //    .Join(dx.grades, stud => stud.id, gr => gr.studentid, (stud, gr) => new { stud.studentname, gr.grade1, gr.coursecode })
            //    .Where(gr => gr.grade1.CompareTo(temp.grade1) <= 0)
            //    .Join(dx.courses, gr => gr.coursecode, course => course.coursecode, (gr, course) => new { gr.studentname, gr.grade1, course.coursename });

            //Room, firstname, lastname, checkin, checkout
            //GridReservasjoner
        }
    }
}
