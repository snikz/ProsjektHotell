using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MobileApp.Resources;

namespace MobileApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int buttonTag = Convert.ToInt32(button.Tag);
            switch (buttonTag)
            {
                case 0:
                    App.CurrentServiceType = 0;
                    App.CurrentStringType ="Cleaning";
                    break;
                case 1:
                    App.CurrentServiceType = 1;
                    App.CurrentStringType = "Maintainance";
                    break;
                case 2:
                    App.CurrentServiceType = 2;
                    App.CurrentStringType = "Service";
                    break;
                default:
                    break;
            }
            string uri = "/ServicePage.xaml";
            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }
    }
}