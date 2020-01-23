using System.Collections.ObjectModel;
using Windows.Devices.WiFi;
using NetworkManager.Core.Services;
using NetworkManager.Services;
using Prism.Windows.Mvvm;
using NetworkManager.Extensions;
using System.Linq;
using System.Threading.Tasks;
using NetworkManager.Core.Constants;
using Microsoft.Practices.ObjectBuilder2;
using System.Collections.Generic;

namespace NetworkManager.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INetworkService _networkService;
        private readonly IDeviceService _deviceService;
        private readonly ISpeedTestService _speedTestService;
        public ObservableCollection<WiFiAvailableNetwork> AvailableNetworks = new ObservableCollection<WiFiAvailableNetwork>();

        public MainViewModel(INetworkService networkService, IDeviceService deviceService, ISpeedTestService speedTestService)
        {
            _networkService = networkService;
            _deviceService = deviceService;
            _speedTestService = speedTestService;
        }

        public async Task<string> Scan()
        {
            if (await _deviceService.HasWiFiAdapter())
            {
                var adapter = await _deviceService.GetWiFiAdapter();
                var report = await _networkService.Scan(adapter);
                await _networkService.Connect(report.AvailableNetworks.First(), adapter);
                var results = _speedTestService.GetTimeResultTasks(SpeedTestConsts.Addresses);
                await Task.WhenAll(results);
                return _speedTestService.GetAverageTimeSpan(results.Select(r => r.Result.Item2).ToList());
            }
            return string.Empty;
        }
    }
}
