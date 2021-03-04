using System.Collections.Generic;

namespace EventsLister.CBP
{
    public interface IHTTPOutputInterpreter
    {
        List<string> InterpretEventPage(string content);
    }
}