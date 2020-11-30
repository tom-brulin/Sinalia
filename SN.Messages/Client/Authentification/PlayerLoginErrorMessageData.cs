using Lidgren.Network;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Messages.Headers;

namespace SN.Messages.Client.Authentification
{
    public class PlayerLoginErrorMessageData : SNMessageData
    {

        public PlayerLoginErrorMessageData()
        {
            DataHeader = (short)ClientMessageTypes.PlayerLoginError;
        }

        public override void Encode(NetOutgoingMessage outgoing)
        {
            base.Encode(outgoing);
        }

        public override void Decode(NetIncomingMessage incoming)
        {
            base.Decode(incoming);
        }

    }
}
