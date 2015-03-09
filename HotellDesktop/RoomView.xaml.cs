using System.Windows;

namespace HotellDesktop
{
    /// <summary>
    /// Interaction logic for RoomView.xaml
    /// </summary>
    public partial class RoomView : Window
    {
        public RoomView()
        {
            InitializeComponent();
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
