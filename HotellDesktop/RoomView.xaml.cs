using System.Data.Linq;
using System.Linq;
using System.Windows;

namespace HotellDesktop
{
    public delegate void update();
    /// <summary>
    /// Interaction logic for RoomView.xaml
    /// </summary>
    public partial class RoomView : Window
    {

        DesktopController desktopController;

        public event update changes;

        public RoomView()
        {
            InitializeComponent();

        }
        /// <summary>
        /// Constructor that takes a roomId as a parameter and construkts a roomView based on the roomId
        /// </summary>
        /// <param name="roomId">The id of the room</param>
        public RoomView(int roomId)
        {
            desktopController = new DesktopController();
            InitializeComponent();
            updateView(roomId);
        }

        /// <summary>
        /// Updates the roomView for a spesific room
        /// </summary>
        /// <param name="selectedRoomId">the room selected</param>
        private void updateView(int selectedRoomId)
        {
            Table<HotellDLL.Service> serviceTable = desktopController.getService();
            Table<HotellDLL.Room> roomTable = desktopController.getRoom();

            if (roomTable != null)
            {
                var roomsWithService = (from room in roomTable
                                        where room.roomId == selectedRoomId
                                        join service in serviceTable on room.roomId equals service.roomId
                                        into joined
                                        from j in joined.DefaultIfEmpty()

                                        select new roomListViewClass()
                                        {

                                            roomId = room.roomId,
                                            note = j == null ? string.Empty : j.note,
                                            stringStatus = j.status== 0 ? "New" : j.status==1 ? "In progress" : "Finished",
                                            intStatus = j == null ? 0 : (j.status),
                                            serviceId = j == null ? 0 : j.id
                                        });

                listView.DataContext = roomsWithService;

            }

        }



        /// <summary>
        /// Cleares the textbox on focus so users can type their text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoteTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            NoteTextbox.Text = "";
        }

        /// <summary>
        /// Method for adding a note to a room
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addNoteButton_Click(object sender, RoutedEventArgs e)
        {

            roomListViewClass selectedRoom = (roomListViewClass)listView.Items[0];

            HotellDLL.Service service = new HotellDLL.Service();
            service.note = NoteTextbox.Text;
            service.status = 0;
            service.roomId = selectedRoom.roomId;

            if ((bool)cleaning.IsChecked)
            {
                service.type = 0;
            }
            else if ((bool)maintenance.IsChecked)
            {
                service.type = 1;
            }
            else
            {
                service.type = 2;
            }

            desktopController.addService(service);
            onChanged();
            this.Close();
        }

        private void deleteNoteButton_Click(object sender, RoutedEventArgs e)
        {
            roomListViewClass selectedRoom = (roomListViewClass)listView.SelectedItem;
            if (selectedRoom != null && selectedRoom.serviceId != 0)
            {
                desktopController.deleteService(selectedRoom.serviceId);
                onChanged();
                this.Close();
            }
        }

        /// <summary>
        /// fyrer event når endringer blir gjort
        /// </summary>
        public void onChanged()
        {
            if (changes != null)
            {
                changes();
            }
        }

    }

    /// <summary>
    /// klasse for listview i RoomView
    /// </summary>
    public class roomListViewClass
    {
        public int roomId { get; set; }
        public string stringStatus { get; set; }
        public int intStatus { get; set; }
        public string note { get; set; }
        public int serviceId { get; set; }

    }

}
