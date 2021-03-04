using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EventsLister.CBP
{
    public class ArgumentHandler : IArgumentHandler
    {
        public bool IsInteractive { get; }
        public IEnumerable<string> FilterCriterias { get; }

        public string EventPageUrl { get; }= "https://www.technikum-wien.at/newsroom/veranstaltungen/";

        public ArgumentHandler(string[] args)
        {
            this.IsInteractive = !args.Contains("/nointeractive");
            this.FilterCriterias = args.Where(x => x != "/nointeractive");
        }
    }
}
