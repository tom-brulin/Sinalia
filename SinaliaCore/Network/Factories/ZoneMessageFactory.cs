using SinaliaCore.Logging;
using SinaliaCore.Network.Messages;
using SinaliaCore.Network.Messages.Headers;

namespace SinaliaCore.Network.Factories
{
    public class ZoneMessageFactory : IMessageFactory
    {

        private readonly ILoggingService _loggingService;

        public ZoneMessageFactory(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public SNMessageData GetMessageData(ushort type)
        {
            ZoneMessageTypes header = (ZoneMessageTypes)type;
            SNMessageData message = null;

            switch (header)
            {
                default:
                    _loggingService.Log($"Cannot build zone message {header.ToString()}", LogMessageType.WARNING);
                    break;
            }

            return message;
        }

    }
}
