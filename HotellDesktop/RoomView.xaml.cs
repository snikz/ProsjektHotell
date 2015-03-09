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

        public RoomView(int roomId)
        {
            desktopController = new DesktopController();
            InitializeComponent();
            updateView(roomId);
        }

        private void updateView(int selectedRoomId)
        {
            Table<HotellDLL.Service> serviceTable = desktopController.getService();
            
            if(serviceTable != null){
                var services = from serv in serviceTable
                               where serv.roomId == selectedRoomId
                               select new { serv.roomId, serv.status, serv.note };
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
