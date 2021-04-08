using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KAW1IPVisu
{
    public class CommandLineArgumentConfigurationHandler : IConfigurationHandler
    {
        public bool WlanOnly { private set; get; }

        public void LoadConfig(string[] args)
        {
            WlanOnly = args.Contains("/wlan");
        }
    }
}