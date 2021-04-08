using System;

namespace KAW1IPVisu
{
    class Program
    {
        private static INetworkInterfaceManager _networkInterfaceManager;
        private static IOutputManager _outputManager;
        private static IConfigurationHandler _configurationHandler;

        static void Main(string[] args)
        {
            // bootstrap components
            _configurationHandler = new CommandLineArgumentConfigurationHandler();
            _networkInterfaceManager = new NetworkInterfaceManager(_configurationHandler);
            _outputManager = new ConsoleOutputManager();

            // start the engines
            _configurationHandler.LoadConfig(args);
            var data = _networkInterfaceManager.GatherNetworkData();
            _outputManager.WriteOutput(Console.Out, data);
        }
    }
}
