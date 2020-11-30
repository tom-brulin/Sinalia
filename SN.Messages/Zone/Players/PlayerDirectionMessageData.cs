using Lidgren.Network;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Messages.Headers;

namespace SN.Messages.Zone.Players
{
    public class PlayerDirectionMessageData : SNMessageData
    {

        public float X { get; set; }
        public float Y { get; set; }

        public PlayerDirectionMessageData()
        {
            DataHeader = (short)ZoneMessageTypes.PlayerDirection;
        }

        public override void Encode(NetOutgoingMessage outgoing)
        {
            base.Encode(outgoing);

            outgoing.Write(X);
            outgoing.Write(Y);
        }

        public override void Decode(NetIncomingMessage incoming)
        {
            base.Decode(incoming);

            X = incoming.ReadFloat();
            Y = incoming.ReadFloat();
        }

    }
}
