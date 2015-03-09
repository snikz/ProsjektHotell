using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
