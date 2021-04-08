using System.Linq;
using System.Net.NetworkInformation;
using NUnit.Framework;



namespace KAW1IPVisu.Tests
{
    // Integration because of its dependencies to the hardware below (requires network interfaces to work)
    public class GatherNetworkDataIntegrationTests
    {
        public class DummyConfigHandler : IConfigurationHandler
        {
            public bool WlanOnly { get; } = false;
            public void LoadConfig(string[] args) { }
        }

        [Test]
        public void BasicExecutionTest()
        {
            // arrange
            INetworkInterfaceManager manager = new NetworkInterfaceManager(new DummyConfigHandler());

            // act
            var actual = manager.GatherNetworkData();

            // assert
            Assert.IsNotNull(actual);
        }

        [Test]
        public void CheckIfAtLeastOneIPWasReceived()
        {
            // arrange
            INetworkInterfaceManager manager = new NetworkInterfaceManager(new DummyConfigHandler());

            // act
            var actual = manager.GatherNetworkData();

            // assert
            CollectionAssert.AllItemsAreUnique(actual);
            CollectionAssert.AllItemsAreNotNull(actual);
            CollectionAssert.IsNotEmpty(actual);
            CollectionAssert.AllItemsAreInstancesOfType(actual, typeof(NetworkInterfaceData));
        }
    }
}