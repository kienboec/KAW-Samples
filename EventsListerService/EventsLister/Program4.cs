﻿using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace EventsLister
{
    class Program4
    {
        public static async Task MainAsync(string[] args)
        {
            const string eventUrl = "https://www.technikum-wien.at/newsroom/veranstaltungen/";

            HtmlDocument pageDocument = null;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(eventUrl);
                var pageContent = await response.Content.ReadAsStringAsync();

                pageDocument = new HtmlDocument();
                pageDocument.LoadHtml(pageContent);
            }

            var formattedEntries = pageDocument
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
                    .Where(entry => args.All(criteria => entry.Contains(criteria)))
                    .ToList()
                ;

            Console.WriteLine($"found {formattedEntries.Count()} dates:");
            formattedEntries.ForEach(x => Console.WriteLine("- " + x));
        }
    }
}
