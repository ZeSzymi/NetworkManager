using System.Threading.Tasks;
using Windows.Devices.WiFi;
using Windows.Security.Credentials;

namespace NetworkManager.Core.Services
{
    public interface INetworkService
    {
        Task<WiFiNetworkReport> Scan(WiFiAdapter adapter);
        Task<WiFiConnectionResult> Connect(WiFiAvailableNetwork availableNetwork, WiFiAdapter adapter);
        Task<WiFiConnectionResult> Connect(WiFiAvailableNetwork availableNetwork, WiFiAdapter adapter, PasswordCredential creditentials);
    }
}
