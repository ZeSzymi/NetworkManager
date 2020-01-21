using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.WiFi;

namespace NetworkManager.Core.Services
{
    public interface INetworkService
    {
        Task<WiFiNetworkReport> Scan(WiFiAdapter adapter);
        Task<WiFiConnectionResult> Connect(WiFiAvailableNetwork availableNetwork, WiFiAdapter adapter);
    }
}
