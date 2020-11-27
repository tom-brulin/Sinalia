using SN.GlobalAbstractions.Logging;
using SN.ProtocolAbstractions.Messages.Headers;

namespace SN.ProtocolAbstractions.Messages.Factories
{
    public class MasterMessageFactory : IMessageFactory
    {

        private readonly ILoggingService loggingService;

        public MasterMessageFactory(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }

        public SNMessageData GetMessageData(short type)
        {
            MasterMessageTypes header = (MasterMessageTypes)type;
            SNMessageData message = null;

            switch (header)
            {
                default:
                    loggingService.Log($"Cannot build master message {header.ToString()}", LogMessageType.WARNING);
                    break;
            }

            return message;
        }

    }
}
