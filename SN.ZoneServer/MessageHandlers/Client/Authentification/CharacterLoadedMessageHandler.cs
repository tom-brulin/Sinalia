using Lidgren.Network;
using SN.ProtocolAbstractions.Messages;
using SN.ZoneServer.Entities;

namespace SN.ZoneServer.MessageHandlers.Client.Authentification
{
    public class CharacterLoadedMessageHandler : IMessageHandler
    {
        private readonly EntitiesProcessor entitiesProcessor;

        public CharacterLoadedMessageHandler(EntitiesProcessor entitiesProcessor)
        {
            this.entitiesProcessor = entitiesProcessor;
        }

        public void Handle(NetConnection sender, SNMessageData messageData)
        {
            if (!(sender.Tag is Player))
                return;

            entitiesProcessor.NewPlayerConnected((Player)sender.Tag);
        }
    }
}
