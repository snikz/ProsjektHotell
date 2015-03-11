using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace MobileApp
{
    public partial class ChangeService : PhoneApplicationPage
    {
        HttpClient client = new HttpClient();
        public ChangeService()
        {
            InitializeComponent();
        }
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            statusListBox.SelectedIndex = App.CurrentServiceStatus;
            noteTextBox.Text = App.CurrentServiceNote;
        }

        private async void Change_Click(object sender, RoutedEventArgs e)
        {
            int selectedStatus = statusListBox.SelectedIndex;
            string note = (noteTextBox.Text.Equals("")) ? "empty" : noteTextBox.Text;
            string result = await client.GetStringAsync(new Uri("http://localhost:57372/HotelServices.svc/" +
                App.CurrentServiceId + "/" + selectedStatus + "/" + note));
            DataContractJsonSerializer JSONSerializer = new DataContractJsonSerializer(typeof(bool));
            bool succesfullyChanged = (bool)JSONSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(result)));
            if (succesfullyChanged)
            {
                string uri = string.Format("/ServicePage.xaml");
                NavigationService.Navigate(new Uri(uri, UriKind.Relative));
            }
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string uri = string.Format("/ServicePage.xaml");
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }


    }
}