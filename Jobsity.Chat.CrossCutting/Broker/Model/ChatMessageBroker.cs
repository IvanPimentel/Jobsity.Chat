using System;

namespace Jobsity.Chat.CrossCutting.Broker.Model
{
    public class ChatMessageBroker
    {
        public string Message { get; set; }
        public Guid ChatRoomId { get; set; }


        public ChatMessageBroker(string message, Guid chatRoomId)
        {
            Message = message;
            ChatRoomId = chatRoomId;
        }
    }
}
