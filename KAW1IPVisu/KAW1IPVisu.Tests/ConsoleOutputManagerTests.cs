using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KAW1IPVisu.Tests
{
    public class ConsoleOutputManagerTests
    {
        [Test]
        public void BasicExecutionTest()
        {
            // arrange
            IOutputManager outputManager = new ConsoleOutputManager();
            var data = new List<NetworkInterfaceData>()
            {
                new NetworkInterfaceData(){ Name = "TestInterface", IP = "127.0.0.1"}
            };
            MemoryStream stream = new MemoryStream();
            TextWriter writer = new StreamWriter(stream);

            // act
            outputManager.WriteOutput(writer, data);
            writer.Flush();
            var actual = Encoding.ASCII.GetString(stream.ToArray());

            // assert
            StringAssert.AreEqualIgnoringCase("Network-Interface: TestInterface (127.0.0.1)" + Environment.NewLine, actual);
        }
    }
}
