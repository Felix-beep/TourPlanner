using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL;
using TourPlanner.DAL;

namespace TourPlanner.Tests
{
    public class ConnectionModeFactoryTests
    {
        [Test]
        public void GetRepo_ReturnsOnlineRepo_WhenAppIsOnline()
        {
            // Arrange
            var onlineRepoMock = new Mock<APITourRepository>();
            onlineRepoMock.Setup(repo => repo.Connect(It.IsAny<Uri>())).Returns(true);

            var offlineRepoMock = new Mock<MemoryTourRepository>();

            var factory = new ConnectionModeFactory(onlineRepoMock.Object, offlineRepoMock.Object);
            factory.SwapMode(); // Set appIsOnline to true

            // Act
            var repo = factory.GetRepo();

            // Assert
            Assert.That(repo, Is.EqualTo(onlineRepoMock.Object));
        }

        [Test]
        public void GetRepo_ReturnsOfflineRepo_WhenAppIsOffline()
        {
            // Arrange
            var onlineRepoMock = new Mock<APITourRepository>();
            var offlineRepoMock = new Mock<MemoryTourRepository>();

            var factory = new ConnectionModeFactory(onlineRepoMock.Object, offlineRepoMock.Object);

            // Act
            var repo = factory.GetRepo();

            // Assert
            Assert.That(offlineRepoMock.Object, Is.EqualTo(repo));
        }

        [Test]
        public void GetRepo_ReturnsOnlineRepo_WhenAppIsSwappedTwice()
        {
            // Arrange
            var onlineRepoMock = new Mock<APITourRepository>();
            var offlineRepoMock = new Mock<MemoryTourRepository>();

            var factory = new ConnectionModeFactory(onlineRepoMock.Object, offlineRepoMock.Object);
            factory.SwapMode(); // Set appIsOnline to true
            factory.SwapMode(); // Set Offline to true

            // Act
            var repo = factory.GetRepo();

            // Assert
            Assert.That(offlineRepoMock.Object, Is.EqualTo(repo));
        }

        [Test]
        public void GetRepo_ReturnsOfflineRepo_WhenAPICantBeReached()
        {
            // Arrange
            var onlineRepoMock = new Mock<APITourRepository>();
            onlineRepoMock.Setup(repo => repo.Connect(It.IsAny<Uri>())).Returns(false);

            var offlineRepoMock = new Mock<MemoryTourRepository>();

            var factory = new ConnectionModeFactory(onlineRepoMock.Object, offlineRepoMock.Object);
            factory.SwapMode(); // Tries to set appIsOnline to true

            // Act
            var repo = factory.GetRepo();

            // Assert
            Assert.That(offlineRepoMock.Object, Is.EqualTo(repo));
        }
    }
}
