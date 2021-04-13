using NUnit.Framework;

namespace KAW2IPVisu.Tests
{
    public class CommandLineArgumentSettingsHandlerTest
    {
        [Test]
        public void LoadSettingsWithEmptyArgsTest()
        {
            // arrange
            ISettingsHandler handler = new CommandLineArgumentSettingsHandler();
            string[] args = {};

            // act
            handler.LoadSettings(args);
            
            // assert
            Assert.IsFalse(handler.WlanOnly);
        }

        [Test]
        public void LoadSettingsWithWlanOnlyArgsTest()
        {
            // arrange
            ISettingsHandler handler = new CommandLineArgumentSettingsHandler();
            string[] args = { "/wlan" };

            // act
            handler.LoadSettings(args);

            // assert
            Assert.IsTrue(handler.WlanOnly);
        }
    }
}