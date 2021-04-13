using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KAW2IPVisu
{
    public interface INetworkDataManager
    {
        List<NetworkData> GatherNetworkData();
    }
}