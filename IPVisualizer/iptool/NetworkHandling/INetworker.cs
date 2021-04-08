using System;
using System.Collections.Generic;
using System.Text;

namespace iptool.NetworkHandling
{
    public interface INetworker
    {
        List<NetworkData> GetAddressData();
    }
}
