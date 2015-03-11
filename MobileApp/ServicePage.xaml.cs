using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MobileApp.HotelServiceReference;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace MobileApp
{
    public partial class ServicePage : PhoneApplicationPage
    {
        HttpClient client = new HttpClient(); 
        public ServicePage()
        {
            InitializeComponent();
        }

        private async void UpdateListBox(object sender, RoutedEventArgs e)
        {
            client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
            string result = await client.GetStringAsync(new Uri("http://localhost:57372/HotelServices.svc/" + App.CurrentServiceType));
            DataContractJsonSerializer JSONSerializer = new DataContractJsonSerializer(typeof(List<Service>));
            var services = (List<Service>)JSONSerializer.ReadObject(new MemoryStream(Encoding.Unicode.GetBytes(result)));
            var filter = from s in services
                         where s.status != 2
                         select new { s.id, s.roomId, s.status, s.note, Status = (s.status == 0) ? "New" : "In Progress", Type=App.CurrentStringType };
            listBox.DataContext = filter;
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedObject = (dynamic)listBox.SelectedItem;
            App.CurrentServiceStatus = selectedObject.status;
            App.CurrentServiceId = selectedObject.id;
            App.CurrentServiceNote = (selectedObject.note == null) ? "" : selectedObject.note;
            string uri = "/ChangeService.xaml";
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string uri = string.Format("/MainPage.xaml");
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }


    }
}