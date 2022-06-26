using FluentAssertions;
using Jobsity.Chat.Domain.Class;
using Jobsity.Chat.Domain.Interfaces.Repositories;
using Jobsity.Chat.Domain.Models;
using Jobsity.Chat.Domain.Services;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Jobsity.Chat.Domain.Tests
{
    public class ChatRoomServiceUnitTests
    {
        [Fact(DisplayName = "Create Invalid Chat Room")]
        [Trait("Category", "Chat Room")]
        public async void ChatRoomService_Create_ChatRoomWithoutNameReturnDomainExpeption()
        {
            // Arrange
            var chatRoom = new ChatRoom("");
            var mocker = new AutoMocker();
            var chatRoomService = mocker.CreateInstance<ChatRoomService>();
            // Act
            var result = await Assert.ThrowsAsync<DomainExeption>(() => chatRoomService.Create(chatRoom));
            // Assert
            chatRoom.IsValid().Should().BeFalse("Name empty should be invalid");
            result.Message.Should().Be("Name of Chat Room must be greater than 3 characters and less than 30 characters");
            mocker.GetMock<IChatRoomRepository>().Verify(m => m.Create(chatRoom), Times.Never, "Chat room is invalid it shouldn't be able to call the repository");
        }


        [Fact(DisplayName = "Create Valid Chat Room")]
        [Trait("Category", "Chat Room")]
        public async void ChatRoomService_Create_CreateValidChatRoom()
        {
            // Arrange
            var chatRoom = new ChatRoom("New chat room");
            var mocker = new AutoMocker();
            var chatRoomService = mocker.CreateInstance<ChatRoomService>();
            // Act
            await chatRoomService.Create(chatRoom);
            // Assert
            chatRoom.IsValid().Should().BeTrue("The chat room in this test should be valid");
            mocker.GetMock<IChatRoomRepository>().Verify(m => m.Create(chatRoom), Times.Once, "Chat room is valid it should be able to call the repository");
        }
    }
}
