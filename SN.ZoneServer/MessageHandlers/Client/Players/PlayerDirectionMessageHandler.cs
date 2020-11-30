using Lidgren.Network;
using SN.Messages.Zone.Players;
using SN.ProtocolAbstractions.Messages;
using SN.ZoneServer.Entities;

namespace SN.ZoneServer.MessageHandlers.Client.Players
{
    public class PlayerDirectionMessageHandler : IMessageHandler
    {

        public PlayerDirectionMessageHandler()
        {

        }

        public void Handle(NetConnection sender, SNMessageData messageData)
        {
            if (!(sender.Tag is Player))
                return;

            var playerDirectionMessageData = (PlayerDirectionMessageData)messageData;
            var player = (Player)sender.Tag;

            player.Velocity.X = playerDirectionMessageData.X;
            player.Velocity.Y = playerDirectionMessageData.Y;
        }

    }
}
