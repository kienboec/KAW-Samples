using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KAW1IPVisu
{
    public interface IOutputManager
    {
        void WriteOutput(TextWriter writer, List<NetworkInterfaceData> data);
    }
}