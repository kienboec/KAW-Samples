using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace KAW2IPVisu
{
    public class NetworkDataManager : INetworkDataManager
    {
        private readonly ISettingsHandler _settingsHandler;

        public NetworkDataManager(ISettingsHandler settingsHandler)
        {
            _settingsHandler = settingsHandler;
        }

        public List<NetworkData> GatherNetworkData()
        {
            return 
                NetworkInterface.GetAllNetworkInterfaces()
                    .Where(x => x.OperationalStatus == OperationalStatus.Up)
                    .Where(x => x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || (!_settingsHandler.WlanOnly))
                    .Select(x => 
                        new NetworkData()
                        {
                            Name = x.Name,
                            IP = GetIP(x)
                        })
                    .ToList();
        }

        private string GetIP(NetworkInterface networkInterface)
        {
            var addresses = networkInterface.GetIPProperties().UnicastAddresses;
            foreach (var address in addresses)
            {
                if (address.Address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return address.Address.ToString();
                }
            }

            return string.Empty;
        }
    }
}