using System;

namespace PluginArchitectureInDotNetCore.MEF.Plugin1
{
    class Program
    {
        static void Main(string[] args)
        {
            new Plugin1Command().Execute();
        }
    }
}
