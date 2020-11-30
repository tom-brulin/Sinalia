using Lidgren.Network;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Messages.Headers;

namespace SN.Messages.Zone.Authentification
{
    public class PlayerLoginMessageData : SNMessageData
    {

        public string Email { get; set; }
        public string Password { get; set; }

        public PlayerLoginMessageData()
        {
            DataHeader = (short)ZoneMessageTypes.PlayerLogin;
        }

        public override void Encode(NetOutgoingMessage outgoing)
        {
            base.Encode(outgoing);

            outgoing.Write(Email);
            outgoing.Write(Password);
        }

        public override void Decode(NetIncomingMessage incoming)
        {
            base.Decode(incoming);

            Email = incoming.ReadString();
            Password = incoming.ReadString();
        }

    }
}
