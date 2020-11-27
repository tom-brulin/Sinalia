using Lidgren.Network;

namespace SN.ProtocolAbstractions.Messages
{
    public class SNMessageData
    {

        public short DataHeader { get; set; }

        public virtual void Encode(NetOutgoingMessage outgoing)
        {
            outgoing.Write(DataHeader);
        }

        public virtual void Decode(NetIncomingMessage incoming)
        {
            DataHeader = incoming.ReadInt16();
        }

    }
}
