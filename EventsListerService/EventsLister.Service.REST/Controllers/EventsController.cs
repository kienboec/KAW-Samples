using System.Collections.Generic;
using System.Threading.Tasks;
using EventsLister.CBP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventsLister.Service.REST.Controllers
{
    /// <summary>
    /// This controller is responsible for all event-driven actions.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;

        private readonly IArgumentHandler _argumentHandler;
        private readonly INetworkCommunicationHandler _networkCommunicationHandler;
        private readonly IHTTPOutputInterpreter _httpOutputInterpreter;

        /// <summary>
        /// Creates an instance of the events controller.
        /// </summary>
        /// <param name="logger">To log actions.</param>
        /// <param name="argumentHandler">Injects all user-driven input from outside.</param>
        /// <param name="networkCommunicationHandler">Communicates with other systems.</param>
        /// <param name="httpOutputInterpreter">Processes output to an understandable internal format.</param>
        public EventsController(ILogger<EventsController> logger, 
            IArgumentHandler argumentHandler, 
            INetworkCommunicationHandler networkCommunicationHandler, 
            IHTTPOutputInterpreter httpOutputInterpreter)
        {
            _logger = logger;

            this._argumentHandler = argumentHandler;
            this._networkCommunicationHandler = networkCommunicationHandler;
            this._httpOutputInterpreter = httpOutputInterpreter;
        }

        /// <summary>
        /// Gets all events by utilizing the communication handler and interpret the result using the <see cref="IHTTPOutputInterpreter"/> component.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     GET /api/events
        /// </remarks>
        /// <returns>The events as plain string with dates.</returns>
        /// <response code="200">Successfully returning the data (cached).</response>
        /// <response code="500">Internal error occured.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IEnumerable<string>> GetAllEvents()
        {
            return _httpOutputInterpreter.InterpretEventPage(await _networkCommunicationHandler.GetHttpContentAsync(_argumentHandler.EventPageUrl));
        }
    }
}