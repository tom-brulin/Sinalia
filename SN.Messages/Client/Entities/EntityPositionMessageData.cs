using Lidgren.Network;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Messages.Headers;

namespace SN.Messages.Client.Entities
{
    public class EntityPositionMessageData : SNMessageData
    {

        public string Uid { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        public EntityPositionMessageData()
        {
            DataHeader = (short)ClientMessageTypes.EntityPosition;
        }

        public override void Encode(NetOutgoingMessage outgoing)
        {
            base.Encode(outgoing);

            outgoing.Write(Uid);
            outgoing.Write(X);
            outgoing.Write(Y);
        }

        public override void Decode(NetIncomingMessage incoming)
        {
            base.Decode(incoming);

            Uid = incoming.ReadString();
            X = incoming.ReadFloat();
            Y = incoming.ReadFloat();
        }

    }
}
