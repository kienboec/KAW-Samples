using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using iptool.ArgumentHandling;

namespace iptool.Tests.ArgumentHandling
{
    [TestFixture]
    public class ArgumentHandlerComponentTests
    {
        [Test]
        public void Constructor_SomeConfigsReadSuccessfully_wlan_v4_mask()
        {
            // arrange
            string[] args = new[] {"/wlan", "/v4", "/mask" };
            ArgumentHandlerComponent component = null;

            // act
            component = new ArgumentHandlerComponent(args);

            // assert
            Assert.AreEqual(true, component.IPV4Only, "ip v4 only not read correctly");
            Assert.AreEqual(true, component.WlanOnly, "wlan only not read correctly");
            Assert.AreEqual(true, component.RenderSubnetMask, "render subnet mask not read correctly");
            Assert.AreEqual(false, component.Reversed, "reversed not read correctly");
        }

        [Test]
        public void Constructor_SomeConfigsReadSuccessfully_v4_mask()
        {
            // arrange
            string[] args = new[] { "/v4", "/mask" };
            ArgumentHandlerComponent component = null;

            // act
            component = new ArgumentHandlerComponent(args);

            // assert
            Assert.AreEqual(true, component.IPV4Only, "ip v4 only not read correctly");
            Assert.AreEqual(false, component.WlanOnly, "wlan only not read correctly");
            Assert.AreEqual(true, component.RenderSubnetMask, "render subnet mask not read correctly");
            Assert.AreEqual(false, component.Reversed, "reversed not read correctly");
        }

        [Test]
        public void Constructor_ArgumentNull()
        {
            // arrange
            string[] args = null;
            ArgumentHandlerComponent component = null;
             
            // act
            component = new ArgumentHandlerComponent(args);

            // assert
            Assert.AreEqual(false, component.IPV4Only, "ip v4 only not read correctly");
            Assert.AreEqual(false, component.WlanOnly, "wlan only not read correctly");
            Assert.AreEqual(false, component.RenderSubnetMask, "render subnet mask not read correctly");
            Assert.AreEqual(false, component.Reversed, "reversed not read correctly");
        }

        [Test]
        public void Constructor_AllConfigsReadSuccessfully()
        {
            // arrange
            string[] args = new[] { "/wlan", "/v4", "/mask", "/reversed" };
            ArgumentHandlerComponent component = null;

            // act
            component = new ArgumentHandlerComponent(args);

            // assert
            Assert.AreEqual(true, component.IPV4Only, "ip v4 only not read correctly");
            Assert.AreEqual(true, component.WlanOnly, "wlan only not read correctly");
            Assert.AreEqual(true, component.RenderSubnetMask, "render subnet mask not read correctly");
            Assert.AreEqual(true, component.Reversed, "reversed not read correctly");
        }
    }
}
