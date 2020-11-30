using Lidgren.Network;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Messages.Headers;

namespace SN.Messages.Zone.Authentification
{
    public class SelectCharacterMessageData : SNMessageData
    {

        public int CharacterId { get; set; }

        public SelectCharacterMessageData()
        {
            DataHeader = (short)ZoneMessageTypes.SelectCharacter;
        }

        public override void Encode(NetOutgoingMessage outgoing)
        {
            base.Encode(outgoing);

            outgoing.Write(CharacterId);
        }

        public override void Decode(NetIncomingMessage incoming)
        {
            base.Decode(incoming);

            CharacterId = incoming.ReadInt32();
        }

    }
}
