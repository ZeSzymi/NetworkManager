using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkManager.Services;
using Windows.Devices.WiFi;

namespace NetworkManager.Core.Services
{
    public class NetworkService : INetworkService
    {
        private readonly IDeviceService _deviceService;

        public NetworkService(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public async Task<WiFiNetworkReport> Scan()
        {
            var adapter = await _deviceService.GetWiFiAdapter();
            await adapter.ScanAsync();
            return adapter.NetworkReport;
        }
    }
}
