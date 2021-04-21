using System;
using PluginArchitectureInDotNetCore.MEF.Plugin2;

namespace PluginArchitectureInDotNetCore.MEF.Plugin2
{
    class Program
    {
        static void Main(string[] args)
        {
            new Plugin2Command().Execute();
        }
    }
}
