using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarked.PubSub.Tests
{
    [TestClass]
    public class EventHubTests : TestBase
    {
        [TestMethod]
        public void EmptyEventPubSub()
        {
            // Arrange
            var count = 0;
            var hub = CreateHub();
            hub.Event("Test").Subscribe(() => ++count);
            // Act
            hub.Event("Test").Publish();
            hub.Event("Test").Publish();
            hub.Event("Test").Publish();
            hub.Event("Test").Publish();
            hub.Event("Test").Publish();
            // Assert
            Assert.AreEqual(5, count);
            Assert.AreEqual(1, hub.Count());
        }
        [TestMethod]
        public void EmptyEventInGroupPubSub()
        {
            // Arrange
            var count = 0;
            var hub = CreateHub();
            hub.Group("Tests").Event("Test").Subscribe(() => ++count);
            // Act
            hub.Group("Tests").Event("Test").Publish();
            hub.Group("Tests").Event("Test").Publish();
            hub.Group("Tests").Event("Test").Publish();
            hub.Group("Tests").Event("Test").Publish();
            hub.Group("Tests").Event("Test").Publish();
            // Assert
            Assert.AreEqual(5, count);
            Assert.AreEqual(1, hub.Count());
        }
        [TestMethod]
        public void EmptyEventInGroupNotSharedWithGlobalPubSub()
        {
            // Arrange
            var count = 0;
            var hub = CreateHub();
            hub.Event("Test").Subscribe(() => ++count);
            // Act
            hub.Group("Tests").Event("Test").Publish();
            hub.Group("Tests").Event("Test").Publish();
            hub.Group("Tests").Event("Test").Publish();
            hub.Group("Tests").Event("Test").Publish();
            hub.Group("Tests").Event("Test").Publish();
            // Assert
            Assert.AreEqual(0, count);
            Assert.AreEqual(2, hub.Count());
            Assert.AreEqual("Test", hub.First().FullName);
            Assert.AreEqual("Tests.Test", hub.Last().FullName);
        }
    }
}
