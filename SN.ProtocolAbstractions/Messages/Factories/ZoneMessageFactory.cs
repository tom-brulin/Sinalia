using SN.GlobalAbstractions.Logging;
using SN.Messages.Zone.Authentification;
using SN.Messages.Zone.Players;
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
                #region Authentification
                case ZoneMessageTypes.PlayerLogin:
                    message = new PlayerLoginMessageData();
                    break;

                case ZoneMessageTypes.RequestCharacters:
                    message = new RequestCharactersMessageData();
                    break;

                case ZoneMessageTypes.SelectCharacter:
                    message = new SelectCharacterMessageData();
                    break;

                case ZoneMessageTypes.CharacterLoaded:
                    message = new CharacterLoadedMessageData();
                    break;
                #endregion

                #region Players
                case ZoneMessageTypes.PlayerDirection:
                    message = new PlayerDirectionMessageData();
                    break;
                #endregion

                default:
                    loggingService.Log($"Cannot build zone message {header.ToString()}", LogMessageType.WARNING);
                    break;
            }

            return message;
        }

    }
}
