using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KAW1IPVisu
{
    public class ConsoleOutputManager : IOutputManager
    {
        public void WriteOutput(TextWriter writer, List<NetworkInterfaceData> networkInterfaces)
        {
            foreach (var networkInterface in networkInterfaces)
            {
                writer.WriteLine(networkInterface.ToString());
            }
        }
    }
}