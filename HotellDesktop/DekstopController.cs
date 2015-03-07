using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellDesktop
{
    class DekstopController
    {
        HotellDLL.DatabaseDataContext database;
        public void init()
        {
            database = new HotellDLL.DatabaseDataContext();
        }
        public Table<HotellDLL.Booking> getBooking()
        {
            return database.Bookings;
        }
    }
}
