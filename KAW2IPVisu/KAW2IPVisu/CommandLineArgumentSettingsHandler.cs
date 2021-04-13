using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KAW2IPVisu
{
    public class CommandLineArgumentSettingsHandler : ISettingsHandler
    {
        public bool WlanOnly { get; private set; } = false;

        public void LoadSettings(string[] args)
        {
            WlanOnly = args.Contains("/wlan");
        }
    }
}