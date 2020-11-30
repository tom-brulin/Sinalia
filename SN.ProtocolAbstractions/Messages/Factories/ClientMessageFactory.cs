using SN.GlobalAbstractions.Logging;
using SN.Messages.Client.Authentification;
using SN.Messages.Client.Entities;
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
                #region Authentification
                case ClientMessageTypes.PlayerLoginSuccess:
                    message = new PlayerLoginSuccessMessageData();
                    break;

                case ClientMessageTypes.PlayerLoginError:
                    message = new PlayerLoginErrorMessageData();
                    break;

                case ClientMessageTypes.SendCharacters:
                    message = new SendCharactersMessageData();
                    break;

                case ClientMessageTypes.CharacterSelected:
                    message = new CharacterSelectedMessageData();
                    break;
                #endregion

                #region Entities
                case ClientMessageTypes.EntityPosition:
                    message = new EntityPositionMessageData();
                    break;

                case ClientMessageTypes.CharacterDisconnected:
                    message = new CharacterDisconnectedMessageData();
                    break;
                #endregion

                default:
                    loggingService.Log($"Cannot build client message {header.ToString()}", LogMessageType.WARNING);
                    break;
            }

            return message;
        }

    }
}
