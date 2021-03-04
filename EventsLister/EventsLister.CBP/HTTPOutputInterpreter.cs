using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace EventsLister.CBP
{
    public class HTTPOutputInterpreter : IHTTPOutputInterpreter
    {
        private List<string> cachedOutput = null;

        public virtual List<string> InterpretEventPage(string content)
        {
            if (cachedOutput != null)
            {
                return cachedOutput;
            }

            HtmlDocument pageDocument = null;
            using (var client = new HttpClient())
            {
                pageDocument = new HtmlDocument();
                pageDocument.LoadHtml(content);
            }

            return pageDocument
                .DocumentNode
                .SelectNodes("//div[contains(@class,'node-news')]")
                .Select(node =>
                    string.Format("{0}.{1}: {2}",
                        ((node?.SelectSingleNode(".//div[contains(@class,'date-huge')]//div[contains(@class,'day')][1]")
                              ?.InnerText
                              ?.Trim() ?? "") + "  ").Substring(0, 2),
                        (node?.SelectSingleNode(".//div[contains(@class,'date-huge')]//div[contains(@class,'month')][1]")
                             ?.InnerText
                             ?.Trim() ?? "___"),
                        (node?.SelectSingleNode(".//div[contains(@class,'title')]//h3[1]")
                             ?.InnerText
                             ?.Trim() ??
                             "<no text>")))
                .ToList()
                ;
        }
    }
}
