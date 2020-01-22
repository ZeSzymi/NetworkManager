using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Windows.Devices.WiFi;
using Microsoft.Toolkit.Uwp.UI.Controls;
using NetworkManager.Core.Services;
using NetworkManager.Services;
using Prism.Windows.Mvvm;
using NetworkManager.Extensions;

namespace NetworkManager.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INetworkService _networkService;
        private readonly IDeviceService _deviceService;

        public ObservableCollection<WiFiAvailableNetwork> AvailableNetworks { get; set; } = new ObservableCollection<WiFiAvailableNetwork>();

        public MainViewModel(INetworkService networkService, IDeviceService deviceService)
        {
            _networkService = networkService;
            _deviceService = deviceService;

            Scan();
        }

        public async void Scan()
        {
            if (await _deviceService.HasWiFiAdapter())
            {
                var scanResult = await _networkService.Scan();
                AvailableNetworks.AddRange(scanResult.AvailableNetworks);
            }
        }
    }
}
