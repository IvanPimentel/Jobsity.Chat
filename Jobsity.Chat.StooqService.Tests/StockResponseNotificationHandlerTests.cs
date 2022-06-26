using Jobsity.Chat.CrossCutting.Broker;
using Jobsity.Chat.CrossCutting.Broker.Model;
using Jobsity.Chat.StooqService.Handlers.Notifications.StockNotification;
using Jobsity.Chat.StooqService.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading;
using Xunit;

namespace Jobsity.Chat.StooqService.Tests
{
    public class StockResponseNotificationHandlerTests
    {
        [Fact(DisplayName = "Send response to valid message")]
        [Trait("Category", "StockService")]
        public async void StockRequestNotificationHandler_Handle_SendValidMessage()
        {
            // Arrange
            var stock = new Stock("AAPL.US", DateTime.Now.ToString(), "00:00", "10.10", "10.50", "9.99", "10.25", "100000");
            var chatMessageBroker = new ChatMessageBroker("/stock=AAPL.US", Guid.NewGuid());
            var notification = new StockResponseNotification(stock, chatMessageBroker);
            var mocker = new AutoMocker();
            var handler = mocker.CreateInstance<StockResponseNotificationHandler>();
            // Act
            await handler.Handle(notification, CancellationToken.None);
            // Assert
            mocker.GetMock<IBroker>()
                .Verify(m => m.Send(It.IsAny<BrokerConfig>(), It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Send response to invalid message")]
        [Trait("Category", "StockService")]
        public async void StockRequestNotificationHandler_Handle_SendResponseOfError()
        {
            // Arrange
            var stock = new Stock("AAPL.US", DateTime.Now.ToString(), "00:00", "10.10", "10.50", "9.99", "N/D", "100000");
            var chatMessageBroker = new ChatMessageBroker("/stock=AAPL.US", Guid.NewGuid());
            var notification = new StockResponseNotification(stock, chatMessageBroker);
            var mocker = new AutoMocker();
            var handler = mocker.CreateInstance<StockResponseNotificationHandler>();
            // Act
            await handler.Handle(notification, CancellationToken.None);
            // Assert
            mocker.GetMock<IBroker>()
                .Verify(m => m.Send(It.IsAny<BrokerConfig>(), It.IsAny<string>()), Times.Once);
        }
    }
}
