using SinaliaCore.Logging;
using SinaliaCore.Network.Messages;
using SinaliaCore.Network.Messages.Headers;

namespace SinaliaCore.Network.Factories
{
    public class ChatMessageFactory : IMessageFactory
    {

        private readonly ILoggingService _loggingService;

        public ChatMessageFactory(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public SNMessageData GetMessageData(ushort type)
        {
            ChatMessageTypes header = (ChatMessageTypes) type;
            SNMessageData message = null;

            switch (header)
            {
                default:
                    _loggingService.Log($"Cannot build chat message {header.ToString()}", LogMessageType.WARNING);
                    break;
            }

            return message;
        }

    }
}
