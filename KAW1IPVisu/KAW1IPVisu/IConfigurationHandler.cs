using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KAW1IPVisu
{
    public interface IConfigurationHandler
    {
        bool WlanOnly { get; }

        void LoadConfig(string[] args);
    }
}