using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetworkManager.Services;
using Windows.Devices.WiFi;
using Windows.Security.Credentials;

namespace NetworkManager.Core.Services
{
    public class NetworkService : INetworkService
    {

        public async Task<WiFiNetworkReport> Scan(WiFiAdapter adapter)
        {
            await adapter.ScanAsync();
            return adapter.NetworkReport;
        }
        public WiFiAvailableNetwork GetAvailableNetworkBySSID(string ssid, List<WiFiAvailableNetwork> availableNetworks) => availableNetworks.First(a => a.Ssid == ssid);
        public async Task<WiFiConnectionResult> Connect(WiFiAvailableNetwork availableNetwork, WiFiAdapter adapter) => await adapter.ConnectAsync(availableNetwork, WiFiReconnectionKind.Automatic);
        public async Task<WiFiConnectionResult> Connect(WiFiAvailableNetwork availableNetwork, WiFiAdapter adapter, PasswordCredential creditentials) => await adapter.ConnectAsync(availableNetwork, WiFiReconnectionKind.Automatic, creditentials);

    }
}
