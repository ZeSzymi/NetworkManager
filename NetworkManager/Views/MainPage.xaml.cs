using System;
using System.Linq;
using NetworkManager.Core.Services;
using NetworkManager.Services;
using NetworkManager.ViewModels;
using Windows.UI.Xaml;
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

        private async void btnScan_Click(object sender, RoutedEventArgs e)
        {
            var list = await ViewModel.Scan();
            ContentDialog dialog = new ContentDialog
            {
                Title = list[0]
            };
            await dialog.ShowAsync();  
        }
    }
}
