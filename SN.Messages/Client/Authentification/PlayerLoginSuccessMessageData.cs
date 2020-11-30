using Lidgren.Network;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Messages.Headers;

namespace SN.Messages.Client.Authentification
{
    public class PlayerLoginSuccessMessageData : SNMessageData
    {

        public string Token { get; set; }

        public PlayerLoginSuccessMessageData()
        {
            DataHeader = (short)ClientMessageTypes.PlayerLoginSuccess;
        }

        public override void Encode(NetOutgoingMessage outgoing)
        {
            base.Encode(outgoing);

            outgoing.Write(Token);
        }

        public override void Decode(NetIncomingMessage incoming)
        {
            base.Decode(incoming);

            Token = incoming.ReadString();
        }

    }
}
