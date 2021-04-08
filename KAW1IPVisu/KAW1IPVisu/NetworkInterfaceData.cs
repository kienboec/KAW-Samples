using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAW1IPVisu
{
    public class NetworkInterfaceData
    {
        public string Name { get; set; }
        public string IP { get; set; }

        public override string ToString()
        {
            return $"Network-Interface: {Name} ({IP})";
        }
    }
}
