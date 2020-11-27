using SinaliaCore.Logging;
using SinaliaCore.Network.Messages;
using SinaliaCore.Network.Messages.Headers;

namespace SinaliaCore.Network.Factories
{
    public class MasterMessageFactory : IMessageFactory
    {

        private readonly ILoggingService _loggingService;

        public MasterMessageFactory(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public SNMessageData GetMessageData(ushort type)
        {
            MasterMessageTypes header = (MasterMessageTypes) type;
            SNMessageData message = null;

            switch(header)
            {
                default:
                    _loggingService.Log($"Cannot build world message {header.ToString()}", LogMessageType.WARNING);
                    break;
            }

            return message;
        }

    }
}
