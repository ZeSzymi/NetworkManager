using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.WiFi;

namespace NetworkManager.Services
{
    public class DeviceService : IDeviceService
    {
        public async Task<bool> HasWiFiAdapter()
        {
            var networkDevices = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector());
            return networkDevices.Count > 0;
        }

        public async Task<WiFiAdapter> GetWiFiAdapter()
        {
            var networkDevices = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector());
            return await WiFiAdapter.FromIdAsync(networkDevices.First().Id);
        }
    }
}
