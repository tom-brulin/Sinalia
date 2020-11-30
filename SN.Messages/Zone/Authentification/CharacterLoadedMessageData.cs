using Lidgren.Network;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Messages.Headers;

namespace SN.Messages.Zone.Authentification
{
    public class CharacterLoadedMessageData : SNMessageData
    {

        public CharacterLoadedMessageData()
        {
            DataHeader = (short)ZoneMessageTypes.CharacterLoaded;
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
