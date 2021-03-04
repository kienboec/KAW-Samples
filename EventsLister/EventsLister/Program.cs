using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EventsLister
{
    class Program
    {
        public static void Main(string[] args) =>
            // write to console 
            // Program1.MainAsync(args).GetAwaiter().GetResult();

            // write with date
            //Program2.MainAsync(args).GetAwaiter().GetResult();

            // count headline
            // Program3.MainAsync(args).GetAwaiter().GetResult();

            // filtering
            // Program4.MainAsync(new string[] { "Info" }).GetAwaiter().GetResult();

            // interactive
            Program5.MainAsync(args).GetAwaiter().GetResult();

            // testing    
            // https://bit.ly/3uEN8CW
            // https://bit.ly/3rdzTag
    }
}
