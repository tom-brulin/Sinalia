using SN.GlobalAbstractions.Logging;
using SN.ProtocolAbstractions.Messages.Headers;

namespace SN.ProtocolAbstractions.Messages.Factories
{
    public class ClientMessageFactory : IMessageFactory
    {

        private readonly ILoggingService loggingService;

        public ClientMessageFactory(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        public SNMessageData GetMessageData(short type)
        {
            ClientMessageTypes header = (ClientMessageTypes)type;
            SNMessageData message = null;

            switch (header)
            {
                default:
                    loggingService.Log($"Cannot build client message {header.ToString()}", LogMessageType.WARNING);
                    break;
            }

            return message;
        }

    }
}
