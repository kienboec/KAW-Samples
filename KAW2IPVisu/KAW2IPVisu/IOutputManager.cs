using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KAW2IPVisu
{
    public interface IOutputManager
    {
        void Write(List<NetworkData> data);
    }
}