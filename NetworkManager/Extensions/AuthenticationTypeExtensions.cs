using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace NetworkManager.Extensions
{
    static class AuthenticationTypeExtensions
    {
        public static bool IsSecuredWithPassword(this NetworkAuthenticationType authenticationType)
            => authenticationType != NetworkAuthenticationType.None;
    }
}
