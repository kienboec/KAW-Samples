using System;
using System.Collections.Generic;
using System.Text;

namespace EventsLister.CBP
{
    public class IOHandler : IIOHandler
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
