using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLister.CBP
{
    public class UIHandler : IUIHandler
    {
        private readonly IArgumentHandler _argumentHandler;
        private readonly IIOHandler _ioHandler;
        private readonly INetworkCommunicationHandler _networkCommunicationHandler;
        private readonly IHTTPOutputInterpreter _httpOutputInterpreter;

        private IEnumerable<string> _filterCriterias;

        public UIHandler(IArgumentHandler argumentHandler, IIOHandler ioHandler, INetworkCommunicationHandler networkCommunicationHandler, IHTTPOutputInterpreter httpOutputInterpreter)
        {
            this._argumentHandler = argumentHandler;
            this._ioHandler = ioHandler;
            this._networkCommunicationHandler = networkCommunicationHandler;
            this._httpOutputInterpreter = httpOutputInterpreter;

            this._filterCriterias = argumentHandler.FilterCriterias.ToList();
        }

        public async Task InitAsync()
        {
            if (this._argumentHandler.IsInteractive)
            {
                _ioHandler.WriteLine("welcome to interactive mode.");
                _ioHandler.WriteLine("you can: list, filter or exit");
            }

            await ProcessInteractiveLoopAsync();
        }

        private async Task ProcessInteractiveLoopAsync()
        {
            var shouldContinue = true;

            do
            {
                var command = this.RequestCommand();
                switch (command)
                {
                    case "list":
                        await ProcessListAsync();
                        break;
                    case "exit":
                        shouldContinue = false;
                        break;
                    case "filter":
                        ProcessFilter();
                        break;
                    default:
                        WriteError();
                        break;
                }
                
            } while (shouldContinue);
        }

        private async Task ProcessListAsync()
        {
            try
            {
                List<String> formattedEntries =
                    _httpOutputInterpreter.InterpretEventPage(
                        await _networkCommunicationHandler.GetHttpContentAsync(
                            _argumentHandler.EventPageUrl));

                var formattedFilteredEntries = formattedEntries
                    .Where(entry => _filterCriterias.All(criteria => entry.Contains(criteria)))
                    .ToList();

                this._ioHandler.WriteLine($"found {formattedFilteredEntries.Count()} dates:");
                formattedFilteredEntries.ForEach(x => this._ioHandler.WriteLine("- " + x));
            } catch(Exception exc)
            {
                _ioHandler.WriteLine("an error occured");
                _ioHandler.WriteLine(exc.Message);
            }
        }
    

        private void ProcessFilter()
        {
            _ioHandler.Write("write criterias as csv: ");
            this._filterCriterias = (_ioHandler.ReadLine()?.Split(',')?.Select(x => x.Trim())) ?? new string[0];
        }

        private void WriteError()
        {
            _ioHandler.WriteLine("bad input");
            _ioHandler.WriteLine("you can: list, filter or exit");
        }

        private string RequestCommand()
        {
            if (this._argumentHandler.IsInteractive)
            {
                _ioHandler.WriteLine(string.Empty);
                _ioHandler.Write("$> ");
                return _ioHandler.ReadLine();
            }
            else
            {
                return "list";
            }
        }
    }
}
