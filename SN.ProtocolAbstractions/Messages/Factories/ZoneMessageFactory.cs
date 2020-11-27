using SN.GlobalAbstractions.Logging;
using SN.ProtocolAbstractions.Messages.Headers;

namespace SN.ProtocolAbstractions.Messages.Factories
{
    public class ZoneMessageFactory : IMessageFactory
    {

        private readonly ILoggingService loggingService;

        public ZoneMessageFactory(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        public SNMessageData GetMessageData(short type)
        {
            ZoneMessageTypes header = (ZoneMessageTypes)type;
            SNMessageData message = null;

            switch (header)
            {
                default:
                    loggingService.Log($"Cannot build zone message {header.ToString()}", LogMessageType.WARNING);
                    break;
            }

            return message;
        }

    }
}
