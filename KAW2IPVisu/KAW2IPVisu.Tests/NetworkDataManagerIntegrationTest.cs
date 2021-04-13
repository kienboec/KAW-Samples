using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace KAW2IPVisu.Tests
{
    public class NetworkDataManagerIntegrationTest
    {
        public class FakeSettingsHandler : ISettingsHandler
        {
            public bool WlanOnly { get; } = false;
            public void LoadSettings(string[] args) {}
        }

        [Test]
        public void BasicUsage()
        {
            // arrange
            ISettingsHandler settingsHandler = new FakeSettingsHandler();
            INetworkDataManager manager = new NetworkDataManager(settingsHandler);

            // act
            var actual = manager.GatherNetworkData();

            // assert
            CollectionAssert.AllItemsAreInstancesOfType(actual, typeof(NetworkData));
            CollectionAssert.AllItemsAreNotNull(actual);
            CollectionAssert.AllItemsAreUnique(actual);
            CollectionAssert.IsNotEmpty(actual);
        }

        [Test]
        public void CheckForLoopBackAddressExists()
        {
            // arrange
            ISettingsHandler settingsHandler = new FakeSettingsHandler();
            INetworkDataManager manager = new NetworkDataManager(settingsHandler);

            // act
            var actual = manager.GatherNetworkData();

            // assert
            Assert.IsTrue(
                actual.Where(
                    x => x.Name == "Loopback Pseudo-Interface 1" && x.IP == "127.0.0.1").Count() > 0);
        }
    }
}
