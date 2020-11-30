using Lidgren.Network;
using Nez;
using SN.Client.Scenes;
using SN.Messages.Client.Entities;
using SN.ProtocolAbstractions.Messages;

namespace SN.Client.Network.MessageHandlers.Zone.Entities
{
    public class CharacterDisconnectedMessageHandler : IMessageHandler
    {

        public CharacterDisconnectedMessageHandler()
        {

        }

        public void Handle(NetConnection sender, SNMessageData messageData)
        {
            if (!(Core.Scene is GameScene))
                return;

            System.Console.WriteLine("Character Disconnected");

            var gameScene = (GameScene)Core.Scene;
            var characterDisconnectedMessageData = (CharacterDisconnectedMessageData)messageData;

            var entity = gameScene.FindEntity(characterDisconnectedMessageData.Character.AccountPersistentId);

            System.Console.WriteLine(entity);

            if (entity != null)
                entity.Destroy();
        }

    }
}
