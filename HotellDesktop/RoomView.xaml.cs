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
        /// Construktor that takes a roomId as a parameter and construkts a roomView based on the roomId
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
                                      
                                       select new { room.roomId };

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

        }
    }
}
