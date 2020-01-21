using System.Collections.ObjectModel;
using Windows.Devices.WiFi;
using NetworkManager.Core.Services;
using NetworkManager.Services;
using Prism.Windows.Mvvm;
using NetworkManager.Extensions;
using System.Linq;

namespace NetworkManager.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INetworkService _networkService;
        private readonly IDeviceService _deviceService;

        public ObservableCollection<WiFiAvailableNetwork> AvailableNetworks = new ObservableCollection<WiFiAvailableNetwork>();

        public MainViewModel(INetworkService networkService, IDeviceService deviceService)
        {
            _networkService = networkService;
            _deviceService = deviceService;
        }

        public async void Scan()
        {
            if (await _deviceService.HasWiFiAdapter())
            {
                var adapter = await _deviceService.GetWiFiAdapter();
                var report = await _networkService.Scan(adapter);
                await _networkService.Connect(report.AvailableNetworks.First(), adapter);
            }
        }
    }
}
