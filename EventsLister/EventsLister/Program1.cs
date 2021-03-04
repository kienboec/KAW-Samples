using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace EventsLister
{
    class Program1
    {
        public static async Task MainAsync(string[] args)
        {
            const string eventUrl = "https://www.technikum-wien.at/newsroom/veranstaltungen/";

            using var client = new HttpClient();
            var response = await client.GetAsync(eventUrl);
            var pageContent = await response.Content.ReadAsStringAsync();

            var pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContent);

            pageDocument
                    .DocumentNode
                    .SelectNodes("//div[contains(@class,'node-news')]")
                    .Select(node =>
                            node?.SelectSingleNode(".//div[contains(@class,'title')]//h3[1]")
                                 ?.InnerText
                                 ?.Trim() ??
                             "<no text>")
                    .ToList()
                    .ForEach(x => Console.WriteLine("- " + x));
        }
    }
}
