using System;

namespace KAW2IPVisu
{
    class Program
    {
        private static ISettingsHandler _settingsHandler = null;
        private static INetworkDataManager _dataManager = null;
        private static IOutputManager _outputManager = null;

        static void Main(string[] args)
        {
            _settingsHandler = new CommandLineArgumentSettingsHandler();
            _dataManager = new NetworkDataManager(_settingsHandler);
            _outputManager = new ConsoleOutputManager();

            _settingsHandler.LoadSettings(args);
            var data = _dataManager.GatherNetworkData();
            _outputManager.Write(data);
        }
    }
}
