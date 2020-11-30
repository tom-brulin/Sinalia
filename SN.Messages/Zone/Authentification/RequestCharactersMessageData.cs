using Lidgren.Network;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Messages.Headers;

namespace SN.Messages.Zone.Authentification
{
    public class RequestCharactersMessageData : SNMessageData
    {

        public RequestCharactersMessageData()
        {
            DataHeader = (short)ZoneMessageTypes.RequestCharacters;
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
