using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KAW2IPVisu.Tests
{
    public class ConsoleOutputManagerTest
    {
        [Test]
        public void BasicUsage()
        {
            // arrange
            ConsoleOutputManager manager = new ConsoleOutputManager();
            List<NetworkData> data = new List<NetworkData>()
            {
                new NetworkData()
                {
                    Name = "NIC1",
                    IP = "1.2.3.4"
                }
            };
            var stream = new MemoryStream();
            TextWriter writer = new StreamWriter(stream);

            // act
            manager.Write(writer, data);
            writer.Flush();
            string actual = Encoding.UTF8.GetString(stream.ToArray());

            // assert
            StringAssert.AreEqualIgnoringCase("NIC: NIC1 (1.2.3.4)\r\n", actual);
        }

        [Test]
        public void ComplexUsage()
        {
            // arrange
            ConsoleOutputManager manager = new ConsoleOutputManager();
            List<NetworkData> data = new List<NetworkData>()
            {
                new NetworkData()
                {
                    Name = "NIC1",
                    IP = "1.2.3.4"
                },
                new NetworkData()
                {
                    Name = "NIC2",
                    IP = "1.2.3.4"
                },
                new NetworkData()
                {
                    Name = "NIC3",
                    IP = "1.2.3.4"
                }
            };
            var stream = new MemoryStream();
            TextWriter writer = new StreamWriter(stream);

            // act
            manager.Write(writer, data);
            writer.Flush();
            string actual = Encoding.UTF8.GetString(stream.ToArray());

            // assert
            StringAssert.AreEqualIgnoringCase(
                @"NIC: NIC1 (1.2.3.4)
NIC: NIC2 (1.2.3.4)
NIC: NIC3 (1.2.3.4)
", actual);
        }

        [Test]
        public void EmptyUsage()
        {
            // arrange
            ConsoleOutputManager manager = new ConsoleOutputManager();
            List<NetworkData> data = new List<NetworkData>() { };
            var stream = new MemoryStream();
            TextWriter writer = new StreamWriter(stream);

            // act
            manager.Write(writer, data);
            writer.Flush();
            string actual = Encoding.UTF8.GetString(stream.ToArray());

            // assert
            StringAssert.AreEqualIgnoringCase("", actual);
        }
    }
}
