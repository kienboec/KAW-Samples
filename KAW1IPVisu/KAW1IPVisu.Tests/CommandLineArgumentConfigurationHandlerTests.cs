using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KAW1IPVisu.Tests
{
    // arrange
    // act
    // assert
    public class CommandLineArgumentConfigurationHandlerTests
    {
        [Test]
        public void CheckSettingsOnNoArguments()
        {
            // arrange
            IConfigurationHandler handler = new CommandLineArgumentConfigurationHandler();
            string[] args = { };

            // act
            handler.LoadConfig(args);

            // assert
            Assert.IsFalse(handler.WlanOnly);
        }

        [Test]
        public void CheckSettingsIfWLANOnlyArgumentSet()
        {
            // arrange
            IConfigurationHandler handler = new CommandLineArgumentConfigurationHandler();
            string[] args = { "/wlan"};

            // act
            handler.LoadConfig(args);

            // assert
            Assert.IsTrue(handler.WlanOnly);
        }
    }
}
