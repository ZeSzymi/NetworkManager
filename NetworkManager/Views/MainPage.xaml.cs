using System;

using NetworkManager.ViewModels;

using Windows.UI.Xaml.Controls;

namespace NetworkManager.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel => DataContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
