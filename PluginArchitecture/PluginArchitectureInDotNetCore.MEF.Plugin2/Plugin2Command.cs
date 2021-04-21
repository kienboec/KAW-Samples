using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;
using PluginArchitectureInDotNetCore.MEF.Common;

namespace PluginArchitectureInDotNetCore.MEF.Plugin2
{
    [Export(typeof(ICommand))]
    public class Plugin2Command : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Plugin2 calling...");
        }
    }
}
