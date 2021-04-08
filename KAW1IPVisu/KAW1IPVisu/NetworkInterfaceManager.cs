using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace KAW1IPVisu
{
    public class NetworkInterfaceManager : INetworkInterfaceManager
    {
        private readonly IConfigurationHandler _configurationHandler;

        public NetworkInterfaceManager(IConfigurationHandler configurationHandler)
        {
            _configurationHandler = configurationHandler;
        }

        public List<NetworkInterfaceData> GatherNetworkData()
        {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(networkInterface => networkInterface.OperationalStatus == OperationalStatus.Up)
                .Where(networkInterface =>
                        (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211) ||
                        (! _configurationHandler.WlanOnly))
                .Where(networkInterface =>
                    networkInterface.GetIPProperties().UnicastAddresses.Where(
                        address => address.Address.AddressFamily == AddressFamily.InterNetwork).Count() > 0)
                .ToList()
                .Select(networkInterface => new NetworkInterfaceData()
                {
                    Name = networkInterface.Name,
                    IP = GetIP(networkInterface)
                })
                .ToList();
        }

        private string GetIP(NetworkInterface networkInterface)
        {
            return
                networkInterface.GetIPProperties().UnicastAddresses.Where(
                        address => address.Address.AddressFamily == AddressFamily.InterNetwork)
                    .FirstOrDefault()
                    .Address.ToString();
        }
    }
}