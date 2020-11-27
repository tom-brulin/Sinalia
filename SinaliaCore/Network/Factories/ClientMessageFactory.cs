using SinaliaCore.Logging;
using SinaliaCore.Network.Messages;
using SinaliaCore.Network.Messages.Client.Auth;
using SinaliaCore.Network.Messages.Headers;

namespace SinaliaCore.Network.Factories
{
    public class ClientMessageFactory : IMessageFactory
    {

        private readonly ILoggingService _loggingService;

        public ClientMessageFactory(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public SNMessageData GetMessageData(ushort type)
        {
            ClientMessageTypes header = (ClientMessageTypes)type;
            SNMessageData message = null;

            switch (header)
            {
                case ClientMessageTypes.LoginError:
                    message = new LoginErrorMessageData();
                    break;

                case ClientMessageTypes.LoginSuccess:
                    message = new LoginSuccessMessageData();
                    break;

                default:
                    _loggingService.Log($"Cannot build client message {header.ToString()}", LogMessageType.WARNING);
                    break;
            }

            return message;
        }

    }
}
