using Lidgren.Network;
using Nez;
using SN.Client.Scenes;
using SN.Messages.Client.Authentification;
using SN.ProtocolAbstractions.Messages;

namespace SN.Client.Network.MessageHandlers.Zone.Authentification
{
    public class SendCharactersMessageHandler : IMessageHandler
    {

        public SendCharactersMessageHandler()
        {

        }

        public void Handle(NetConnection sender, SNMessageData messageData)
        {
            if (!(Core.Scene is CharacterSelectionScene))
                return;

            var sendCharactersMessageData = (SendCharactersMessageData)messageData;
            ((CharacterSelectionScene)Core.Scene).LoadCharacters(sendCharactersMessageData.Characters);
        }

    }
}
