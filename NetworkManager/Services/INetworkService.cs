using NetworkManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.WiFi;

namespace NetworkManager.Core.Services
{
    public interface INetworkService
    {
        IObservable<WiFiNetworkReport> Scan();
    }
}
