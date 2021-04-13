using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KAW2IPVisu
{
    public class ConsoleOutputManager : IOutputManager
    {
        public void Write(TextWriter writer, List<NetworkData> data)
        {
            foreach (var item in data)
            {
                writer.WriteLine(item.ToString());
            }
        }

        public void Write(List<NetworkData> data)
        {
            Write(Console.Out, data);
        }
    }
}