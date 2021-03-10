using System.Threading.Tasks;

namespace EventsLister.CBP
{
    // https://dotnetcoretutorials.com/2018/02/27/loading-parsing-web-page-net-core/
    class Program
    {
        static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        private static IUIHandler _uiHandler = null;

        static async Task MainAsync(string[] args)
        {
            // bootstrap dependencies
            IIOHandler ioHandler = new IOHandler();
            IHTTPOutputInterpreter httpOutputInterpreter = new HTTPOutputInterpreter();
            IArgumentHandler argumentHandler = new ArgumentHandler(args);
            INetworkCommunicationHandler networkCommunicationHandler = new NetworkCommunicationHandler();

            // start application
            _uiHandler = new UIHandler(argumentHandler, ioHandler, networkCommunicationHandler, httpOutputInterpreter);
            await _uiHandler.InitAsync();
        }
    }
}
