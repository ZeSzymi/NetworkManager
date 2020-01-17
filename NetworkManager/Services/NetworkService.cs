using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using NetworkManager.Models;
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

        public IObservable<WiFiNetworkReport> Scan()
        {
            return Observable.Create<WiFiNetworkReport>(async o =>
            {
                var adapter = await _deviceService.GetWiFiAdapter();
                await adapter.ScanAsync();
                o.OnNext(adapter.NetworkReport);
            });
        }
    }
}
