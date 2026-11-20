using EduHub.Application.Events;
using EduHub.Application.Notifications;
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;
using Moq;

namespace EduHub.Tests.Events;

public class EventServiceTests
{
    [Fact]
    public void CreateEvent_WithValidData_CallsAddAndSendOnce()
    {
        // Arrange
        var mockRepo = new Mock<IEventRepository>();
        var mockNotifier = new Mock<INotificationService>();
        var mockFactory = new Mock<INotificationServiceFactory>();
        mockFactory.Setup(f => f.Create("email")).Returns(mockNotifier.Object);
        var service = new EventService(mockRepo.Object, mockFactory.Object);

        // Act
        service.CreateEvent(Guid.NewGuid(), "Workshop C#", DateTime.UtcNow.AddDays(7), 50, "email");

        // Assert
        mockRepo.Verify(r => r.Add(It.IsAny<Event>()), Times.Once);
        mockNotifier.Verify(n => n.Send(It.IsAny<string>()), Times.Once);
    }
}