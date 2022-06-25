using FluentAssertions;
using Jobsity.Chat.Domain.Class;
using Jobsity.Chat.Domain.Interfaces.Repositories;
using Jobsity.Chat.Domain.Models;
using Jobsity.Chat.Domain.Services;
using Moq;
using Moq.AutoMock;
using System;
using Xunit;

namespace Jobsity.Chat.Domain.Tests
{
    public class ChatRoomMessageServiceUnitTests
    {
        [Fact(DisplayName = "Create Invalid Message - Without Content")]
        [Trait("Category", "Chat Room Message")]
        public async void ChatRoomMessageService_Create_ChatRoomMessageWithoutContentReturnDomainExpeption()
        {
            // Arrange
            var model = new ChatRoomMessage("", Guid.NewGuid(), Guid.NewGuid());
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<ChatRoomMessageService>();
            // Act
            var result = await Assert.ThrowsAsync<DomainExeption>(() => service.Create(model));
            // Assert
            model.IsValid().Should().BeFalse("Message content empty should be invalid");
            result.Message.Should().Be("Message must be greater than 0 characters and less than 100 characters");
            mocker.GetMock<IChatRoomMessageRepository>().Verify(m => m.Create(model), Times.Never, "Message is invalid it shouldn't be able to call the repository");
        }


        [Fact(DisplayName = "Create Invalid Message - Without ChatRoomId")]
        [Trait("Category", "Chat Room Message")]
        public async void ChatRoomMessageService_Create_ChatRoomMessageWithoutChatRoomIdReturnDomainExpeption()
        {
            // Arrange
            var model = new ChatRoomMessage("Valid Content", Guid.NewGuid(), Guid.Empty);
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<ChatRoomMessageService>();
            // Act
            var result = await Assert.ThrowsAsync<DomainExeption>(() => service.Create(model));
            // Assert
            model.IsValid().Should().BeFalse("ChatRoom Id empty should be invalid");
            result.Message.Should().Be("ChatRoomId is required");
            mocker.GetMock<IChatRoomMessageRepository>().Verify(m => m.Create(model), Times.Never, "Message is invalid it shouldn't be able to call the repository");
        }


        [Fact(DisplayName = "Create Invalid Message - Without UserId and NOT an Integration Message")]
        [Trait("Category", "Chat Room Message")]
        public async void ChatRoomMessageService_Create_ChatRoomMessageWithoutUserIdAndNotIntegrationReturnDomainExpeption()
        {
            // Arrange
            var model = new ChatRoomMessage("Valid Content", Guid.Empty, Guid.NewGuid());
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<ChatRoomMessageService>();
            // Act
            var result = await Assert.ThrowsAsync<DomainExeption>(() => service.Create(model));
            // Assert
            model.IsValid().Should().BeFalse("Message content empty should be invalid");
            result.Message.Should().Be("UserId is required");
            mocker.GetMock<IChatRoomMessageRepository>().Verify(m => m.Create(model), Times.Never, "Message is invalid it shouldn't be able to call the repository");
        }


        [Fact(DisplayName = "Create Valid Integration Message")]
        [Trait("Category", "Chat Room Message")]
        public async void ChatRoomMessageService_Create_ChatRoomMessageWithoutUserIdAndIntegration()
        {
            // Arrange
            var model = new ChatRoomMessage("Valid Content", Guid.Empty, Guid.NewGuid(), true);
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<ChatRoomMessageService>();
            // Act
            await service.Create(model);
            // Assert
            model.IsValid().Should().BeTrue("The chat message in this test should be valid");

            mocker.GetMock<IChatRoomMessageRepository>().Verify(m => m.Create(model), Times.Once, "Chat message is valid it should be able to call the repository");
        }


        [Fact(DisplayName = "Create Valid Chat Room Message")]
        [Trait("Category", "Chat Room Message")]
        public async void ChatRoomMessageService_Create_CreateValidChatRoomMessage()
        {
            // Arrange
            var model = new ChatRoomMessage("Valid Content", Guid.NewGuid(), Guid.NewGuid());
            var mocker = new AutoMocker();
            var service = mocker.CreateInstance<ChatRoomMessageService>();
            // Act
            await service.Create(model);
            // Assert
            model.IsValid().Should().BeTrue("The chat message in this test should be valid");
            mocker.GetMock<IChatRoomMessageRepository>().Verify(m => m.Create(model), Times.Once, "Chat message is valid it should be able to call the repository");
        }


        [Fact(DisplayName = "Create Valid Stock Code Command")]
        [Trait("Category", "Chat Room Message")]
        public void ChatRoomMessage_IsStockCode_CreateValidStockCodeCommand()
        {
            // Arrange
            var model = new ChatRoomMessage("/stock=AAPL", Guid.NewGuid(), Guid.NewGuid());
            // Act && Assert
            model.IsValid().Should().BeTrue("The chat message in this test should be valid");
            model.IsStockCode().Should().BeTrue("The chat message in this test should be a stock code command");
        }

        [Fact(DisplayName = "Create Invalid Stock Code Command")]
        [Trait("Category", "Chat Room Message")]
        public void ChatRoomMessage_IsStockCode_CreateInvalidStockCodeCommand()
        {
            // Arrange
            var model = new ChatRoomMessage("/another=AAPL", Guid.NewGuid(), Guid.NewGuid());
            // Act && Assert
            model.IsValid().Should().BeTrue("The chat message in this test should be valid");
            model.IsStockCode().Should().BeFalse("The correct command should be /stock=");
        }
    }
}
