using Microsoft.Toolkit.Uwp.UI.Controls;
using NetworkManager.Core.Services;
using NetworkManager.Extensions;
using NetworkManager.Services;
using Prism.Commands;
using Prism.Windows.Mvvm;
using System.Collections.ObjectModel;
using Windows.Devices.WiFi;

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

        private bool _isConnected;

        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }

        private bool _isProcessing;

        public bool IsProcessing
        {
            get => _isProcessing;
            set => SetProperty(ref _isProcessing, value);
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

        private string _password;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private ObservableCollection<WiFiAvailableNetwork> _availableNetworks
            = new ObservableCollection<WiFiAvailableNetwork>();

        public ObservableCollection<WiFiAvailableNetwork> AvailableNetworks
        {
            get => _availableNetworks;
            set => SetProperty(ref _availableNetworks, value);
        }

        public DelegateCommand<object> SelectionCommand { get; private set; }
        public DelegateCommand<object> ConnectCommand { get; private set; }
        public DelegateCommand<object> DisconnnectCommand { get; private set; }
        public DelegateCommand<object> RefreshCommand { get; private set; }

        public MainViewModel(INetworkService networkService, IDeviceService deviceService, ISpeedTestService speedTestService)
        {
            _networkService = networkService;
            _deviceService = deviceService;

            SelectionCommand = new DelegateCommand<object>(OnNetworkSelected, CanExecuteCommand);
            ConnectCommand = new DelegateCommand<object>(Connect, CanExecuteCommand);
            DisconnnectCommand = new DelegateCommand<object>(Disconnect, CanExecuteCommand);
            RefreshCommand = new DelegateCommand<object>(Refresh, CanExecuteCommand);

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

        private void OnNetworkSelected(object parameter)
        {
            var args = parameter as OrbitViewItemClickedEventArgs;
            var dataItem = args?.Item as OrbitViewDataItem;
            SelectedNetwork = dataItem.Item as WiFiAvailableNetwork;
            IsDetailsPanelOpened = true;
        }

        private void Connect(object parameter)
        {
            // connect with SelectedNetwork and Password property
            IsConnected = true;
        }

        private void Disconnect(object parameter)
        {
            IsProcessing = true;
        }

        private void Refresh(object parameter)
        {
            Scan();
        }

        private bool CanExecuteCommand(object parameter) => true;
    }
}
