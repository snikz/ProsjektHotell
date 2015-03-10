using System.Data.Linq;
using System.Linq;
using System.Windows;

namespace HotellDesktop
{

    /// <summary>
    /// Interaction logic for RoomView.xaml
    /// </summary>
    public partial class RoomView : Window
    {

        DesktopController desktopController;

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
                var roomsWithService = from room in roomTable
                                       where room.roomId == selectedRoomId
                                       join service in serviceTable on room.roomId equals service.roomId
                                       into joined
                                       from j in joined.DefaultIfEmpty()
                                      
                                       select new roomListViewClass() { roomId = room.roomId, note = j == null ? string.Empty : j.note, status = j == null ? false : j.status };

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
            service.status = false;
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
            this.Close();
        }
    }

    /// <summary>
    /// klasse for listview i RoomView
    /// </summary>
    public class roomListViewClass{
        public int roomId { get; set; }
        public bool status { get; set; }
        public string note { get; set; }
 }

}
