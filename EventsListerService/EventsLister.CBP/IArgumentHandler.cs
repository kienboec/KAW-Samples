using System.Collections.Generic;

namespace EventsLister.CBP
{
    public interface IArgumentHandler
    {
        bool IsInteractive { get; }
        IEnumerable<string> FilterCriterias { get; }
        string EventPageUrl { get; }
    }
}