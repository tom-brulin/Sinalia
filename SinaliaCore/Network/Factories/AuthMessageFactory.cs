using SinaliaCore.Logging;
using SinaliaCore.Network.Messages;
using SinaliaCore.Network.Messages.Auth;
using SinaliaCore.Network.Messages.Headers;

namespace SinaliaCore.Network.Factories
{
    public class AuthMessageFactory : IMessageFactory
    {

        private readonly ILoggingService _loggingService;

        public AuthMessageFactory(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public SNMessageData GetMessageData(ushort type)
        {
            AuthMessageTypes header = (AuthMessageTypes) type;
            SNMessageData message = null;

            switch (header)
            {
                case AuthMessageTypes.Login:
                    message = new LoginMessageData();
                    break;

                default:
                    _loggingService.Log($"Cannot build auth message {header.ToString()}", LogMessageType.WARNING);
                    break;
            }

            return message;
        }

    }
}
