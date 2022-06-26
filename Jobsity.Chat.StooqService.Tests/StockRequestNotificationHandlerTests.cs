using Jobsity.Chat.CrossCutting.Broker.Model;
using Jobsity.Chat.StooqService.Handlers.Notifications.StockNotification;
using MediatR;
using Moq;
using Moq.AutoMock;
using Newtonsoft.Json;
using System;
using System.Threading;
using Xunit;

namespace Jobsity.Chat.StooqService.Tests
{
    public class StockRequestNotificationHandlerTests
    {
        [Fact(DisplayName = "Get a valid Message and process")]
        [Trait("Category", "StockService")]
        public async void StockRequestNotificationHandler_Handle_GetValidMessage()
        {
            // Arrange
            var message = new ChatMessageBroker("/stock=AAPL.US", Guid.NewGuid());
            var notification = new StockRequestNotification(JsonConvert.SerializeObject(message));
            var mocker = new AutoMocker();
            var handler = mocker.CreateInstance<StockRequestNotificationHandler>();

            // Act
            await handler.Handle(notification, CancellationToken.None);

            // Assert
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Get a invalid Message and throw Exception")]
        [Trait("Category", "StockService")]
        public async void StockRequestNotificationHandler_Handle_GetInvalidMessage()
        {
            // Arrange
            var message = new ChatMessageBroker("AAPL.US", Guid.NewGuid());
            var notification = new StockRequestNotification(JsonConvert.SerializeObject(message));
            var mocker = new AutoMocker();
            var handler = mocker.CreateInstance<StockRequestNotificationHandler>();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(notification, CancellationToken.None));
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }
    }
}
