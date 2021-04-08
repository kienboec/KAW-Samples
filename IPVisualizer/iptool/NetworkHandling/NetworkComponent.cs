using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using iptool.ArgumentHandling;

namespace iptool.NetworkHandling
{
    public class NetworkComponent : INetworker
    {
        private ArgumentHandlerComponent _argumentHandlerComponent;

        public NetworkComponent(ArgumentHandlerComponent argumentHandlerComponent)
        {
            this._argumentHandlerComponent = argumentHandlerComponent;
        }

        public List<NetworkData> GetAddressData()
        {
            List<NetworkInterface> nics = new List<NetworkInterface>();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up &&
                    (
                        (_argumentHandlerComponent.WlanOnly && nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211) ||
                        !_argumentHandlerComponent.WlanOnly))
                {
                    nics.Add(nic);
                }
            }

            List<NetworkData> returnValue = new List<NetworkData>();
            foreach (var nic in nics)
            {
                var ips = nic.GetIPProperties().UnicastAddresses;
                foreach (var ip in ips)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork || !_argumentHandlerComponent.IPV4Only)
                    {
                        returnValue.Add(new NetworkData()
                        {
                            IP = ip.Address.ToString(),
                            SubnetPrefix = ip.PrefixLength,
                        });

                    }
                }
            }

            return returnValue;
        }
    }
}