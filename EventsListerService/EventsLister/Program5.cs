using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace EventsLister
{
    // https://dotnetcoretutorials.com/2018/02/27/loading-parsing-web-page-net-core/

    class Program5
    {
        public static async Task MainAsync(string[] args)
        {
            const string eventUrl = "https://www.technikum-wien.at/newsroom/veranstaltungen/";

            bool isInteractive = !args.Contains("/nointeractive");

            var filterCriterias = args.Where(x => x != "/nointeractive");
            List<String> formattedEntries = null;
            string command = "";

            if (isInteractive)
            {
                Console.WriteLine("welcome to interactive mode.");
                Console.WriteLine("you can: list, filter or exit");
            }

            do
            {
                if (isInteractive)
                {
                    Console.WriteLine();
                    Console.Write("$> ");
                    command = Console.ReadLine();
                }

                if (command == "exit")
                {
                    break;
                }

                if (command == "filter")
                {
                    Console.Write("write criterias as csv: ");
                    filterCriterias = (Console.ReadLine()?.Split(',')?.Select(x => x.Trim())) ?? new string[0];
                    continue;
                }

                if (command != "list")
                {
                    Console.WriteLine("bad input");
                    Console.WriteLine("you can: list, filter or exit");
                    continue;
                }

                if (formattedEntries == null)
                {
                    HtmlDocument pageDocument = null;
                    using (var client = new HttpClient())
                    {
                        var response = await client.GetAsync(eventUrl);
                        var pageContent = await response.Content.ReadAsStringAsync();

                        pageDocument = new HtmlDocument();
                        pageDocument.LoadHtml(pageContent);
                    }

                    formattedEntries = pageDocument
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

                var formattedFilteredEntries = formattedEntries
                    .Where(entry => filterCriterias.All(criteria => entry.Contains(criteria)))
                    .ToList();

                Console.WriteLine($"found {formattedFilteredEntries.Count()} dates:");
                formattedFilteredEntries.ForEach(x => Console.WriteLine("- " + x));
            } while (command != "exit");
        }
    }
}
