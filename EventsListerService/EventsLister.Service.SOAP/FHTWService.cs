using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsLister.CBP;
using Microsoft.Extensions.Logging;

namespace EventsLister.Service.SOAP
{
    public class FHTWService : IFHTWService
    {
        private readonly ILogger<FHTWService> _logger;

        private readonly IArgumentHandler _argumentHandler;
        private readonly INetworkCommunicationHandler _networkCommunicationHandler;
        private readonly IHTTPOutputInterpreter _httpOutputInterpreter;

        private bool _wasCalledOnce = false;

        public FHTWService(ILogger<FHTWService> logger,
            IArgumentHandler argumentHandler,
            INetworkCommunicationHandler networkCommunicationHandler,
            IHTTPOutputInterpreter httpOutputInterpreter)
        {
            _logger = logger;

            this._argumentHandler = argumentHandler;
            this._networkCommunicationHandler = networkCommunicationHandler;
            this._httpOutputInterpreter = httpOutputInterpreter;
        }

        public bool HasData()
        {
            return _wasCalledOnce;
        }

        public async Task<IEnumerable<FHTWEvent>> GetAllEvents()
        {
            try
            {
                var getEventsAsString = _httpOutputInterpreter.InterpretEventPage(
                    await _networkCommunicationHandler.GetHttpContentAsync(
                        _argumentHandler.EventPageUrl));
                var events = getEventsAsString.Select(x =>
                {
                    var parts = x.Split(new[] {':'}, 2, StringSplitOptions.None);
                    return new FHTWEvent() {Date = parts[0], Name = parts[1]};
                });
                return events.ToList();
            }
            finally
            {
                _wasCalledOnce = true;
            }
        }
    }
}
