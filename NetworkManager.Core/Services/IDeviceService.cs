using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.WiFi;

namespace NetworkManager.Services
{
    public interface IDeviceService
    {
        Task<bool> HasWiFiAdapter();
        Task<WiFiAdapter> GetWiFiAdapter();
    }
}
