using System;
using System.Collections.Generic;
using System.Text;
using iptool.ArgumentHandling;
using iptool.NetworkHandling;
using iptool.Rendering;
using Moq;
using NUnit.Framework;

namespace iptool.Tests.Rendering
{
    [TestFixture]
    public class RendererComponentTests
    {
        [Test]
        public void Render_IPv4AddressOnly()
        {
            // arrange
            var argumentHandlerComponentMock = new Mock<ArgumentHandlerComponent>();
            argumentHandlerComponentMock.Setup(x => x.IPV4Only).Returns(true);
            ArgumentHandlerComponent argumentHandlerComponent = argumentHandlerComponentMock.Object;
            
            RendererComponent rendererComponent = new RendererComponent(argumentHandlerComponent);
            List<NetworkData> data = new List<NetworkData>()
            {
                new NetworkData(){IP="10.1.2.3", SubnetPrefix = 8}
            };

            // act
            var output = rendererComponent.Render(data);

            // assert
            Assert.AreEqual("10.1.2.3" + Environment.NewLine, output, "ip address not rendered successfully");
        }

        [Test]
        public void Render_IPv4AddressWithSubnetMask()
        {
            // arrange
            var argumentHandlerComponentMock = new Mock<ArgumentHandlerComponent>();
            argumentHandlerComponentMock.Setup(x => x.IPV4Only).Returns(true);
            argumentHandlerComponentMock.Setup(x => x.RenderSubnetMask).Returns(true);
            ArgumentHandlerComponent argumentHandlerComponent = argumentHandlerComponentMock.Object;

            RendererComponent rendererComponent = new RendererComponent(argumentHandlerComponent);
            List<NetworkData> data = new List<NetworkData>()
            {
                new NetworkData(){IP="10.1.2.3", SubnetPrefix = 8}
            };

            // act
            var output = rendererComponent.Render(data);

            // assert
            Assert.AreEqual("10.1.2.3/8" + Environment.NewLine, output, "ip address not rendered successfully");
        }

        [Test]
        public void Render_EmptyListOfNetworkData()
        {
            // arrange
            var argumentHandlerComponentMock = new Mock<ArgumentHandlerComponent>();
            argumentHandlerComponentMock.Setup(x => x.IPV4Only).Returns(true);
            argumentHandlerComponentMock.Setup(x => x.RenderSubnetMask).Returns(true);
            argumentHandlerComponentMock.Setup(x => x.WlanOnly).Returns(true);
            ArgumentHandlerComponent argumentHandlerComponent = argumentHandlerComponentMock.Object;

            RendererComponent rendererComponent = new RendererComponent(argumentHandlerComponent);
            List<NetworkData> data = new List<NetworkData>() {};

            // act
            var output = rendererComponent.Render(data);

            // assert
            Assert.AreEqual(string.Empty, output, "ip address not rendered successfully");
        }

        [Test]
        public void Render_NullListOfNetworkData()
        {
            // arrange
            var argumentHandlerComponentMock = new Mock<ArgumentHandlerComponent>();
            argumentHandlerComponentMock.Setup(x => x.IPV4Only).Returns(true);
            argumentHandlerComponentMock.Setup(x => x.RenderSubnetMask).Returns(true);
            argumentHandlerComponentMock.Setup(x => x.WlanOnly).Returns(true);
            ArgumentHandlerComponent argumentHandlerComponent = argumentHandlerComponentMock.Object;

            RendererComponent rendererComponent = new RendererComponent(argumentHandlerComponent);
            List<NetworkData> data = null;

            // act
            var output = rendererComponent.Render(data);

            // assert
            Assert.AreEqual(string.Empty, output, "ip address not rendered successfully");
        }
    }
}
