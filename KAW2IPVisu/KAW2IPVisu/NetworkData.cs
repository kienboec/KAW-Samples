using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAW2IPVisu
{
    public class NetworkData
    {
        public string Name{ get; set; }
        public string IP { get; set; }

        public override string ToString()
        {
            return $"NIC: {Name} ({IP})";
        }
    }
}
