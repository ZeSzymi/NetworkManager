using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Windows.Devices.WiFi;
using Microsoft.Toolkit.Uwp.UI.Controls;
using NetworkManager.Core.Services;
using NetworkManager.Services;
using Prism.Windows.Mvvm;
using Prism.Commands;
using NetworkManager.Extensions;

namespace NetworkManager.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INetworkService _networkService;
        private readonly IDeviceService _deviceService;

        private bool _isDetailsPanelOpened;

        public bool IsDetailsPanelOpened
        {
            get => _isDetailsPanelOpened;
            set => SetProperty(ref _isDetailsPanelOpened, value);
        }

        private WiFiAvailableNetwork _selectedNetwork;

        public WiFiAvailableNetwork SelectedNetwork
        {
            get => _selectedNetwork;
            set
            {
                SetProperty(ref _selectedNetwork, value);
                Ssid = _selectedNetwork.Ssid;
                IsSecured = _selectedNetwork.SecuritySettings.NetworkAuthenticationType.IsSecuredWithPassword();
            }
        }

        private string _ssid;

        public string Ssid
        {
            get => _ssid;
            set => SetProperty(ref _ssid, value);
        }

        private bool _isSecured;

        public bool IsSecured
        {
            get => _isSecured;
            set => SetProperty(ref _isSecured, value);
        }

        private ObservableCollection<WiFiAvailableNetwork> _availableNetworks
            = new ObservableCollection<WiFiAvailableNetwork>();

        public ObservableCollection<WiFiAvailableNetwork> AvailableNetworks
        {
            get => _availableNetworks;
            set => SetProperty(ref _availableNetworks, value);
        }

        public DelegateCommand<object> SelectionCommand { get; private set; }

        public MainViewModel(INetworkService networkService, IDeviceService deviceService)
        {
            _networkService = networkService;
            _deviceService = deviceService;

            SelectionCommand = new DelegateCommand<object>(OnNetworkSelected, CanExecuteCommand);

            Scan();
        }

        private async void Scan()
        {
            if (!await _deviceService.HasWiFiAdapter()) return;
            var scanResult = await _networkService.Scan();

            // We have to create new instance to invoke databinding
            // This can be replaced by a model class with implemented INotifyProperty interface 
            AvailableNetworks = new ObservableCollection<WiFiAvailableNetwork>(scanResult.AvailableNetworks);
        }

        void OnNetworkSelected(object parameter)
        {
            var args = parameter as OrbitViewItemClickedEventArgs;
            var dataItem = args?.Item as OrbitViewDataItem;
            SelectedNetwork = dataItem.Item as WiFiAvailableNetwork;
            IsDetailsPanelOpened = true;
        }

        bool CanExecuteCommand(object parameter) => true;
    }
}
