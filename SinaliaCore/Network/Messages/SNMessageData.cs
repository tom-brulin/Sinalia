using Lidgren.Network;

namespace SinaliaCore.Network.Messages
{
    public class SNMessageData
    {

        public ushort DataHeader { get; set; }

        public virtual void Encode(NetOutgoingMessage outgoing)
        {
            outgoing.Write(DataHeader);
        }

        public virtual void Decode(NetIncomingMessage incoming)
        {
            DataHeader = incoming.ReadUInt16();
        }

    }
}
