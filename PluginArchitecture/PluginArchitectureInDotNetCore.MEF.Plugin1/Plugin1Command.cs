using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;
using PluginArchitectureInDotNetCore.MEF.Common;

namespace PluginArchitectureInDotNetCore.MEF.Plugin1
{
    [Export(typeof(ICommand))]
    public class Plugin1Command : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Plugin1 calling...");
        }
    }
}
