using System.Net;
using Lidgren.Network;
using SN.ClientProtocol.Peers;
using SN.Global;
using SN.Messages.Client.Authentification;
using SN.ProtocolAbstractions.Messages;

namespace SN.Client.Network.MessageHandlers.Zone.Authentification
{
    public class PlayerLoginSuccessMessageHandler : IUnconnectedMessageHandler
    {
        private readonly ZoneClientNetPeer zoneClientNetPeer;

        public PlayerLoginSuccessMessageHandler(ZoneClientNetPeer zoneClientNetPeer)
        {
            this.zoneClientNetPeer = zoneClientNetPeer;
        }

        public void Handle(IPEndPoint sender, SNMessageData messageData)
        {
            var playerLoginSuccesMessageData = (PlayerLoginSuccessMessageData)messageData;

            NetOutgoingMessage approval = zoneClientNetPeer.CreateMessage();
            approval.Write(playerLoginSuccesMessageData.Token);
            zoneClientNetPeer.Connect(Constants.Host, Constants.ZoneServerPort, approval);
        }

    }
}
