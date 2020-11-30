using Lidgren.Network;
using SN.Global.Models;
using SN.ProtocolAbstractions;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Messages.Headers;

namespace SN.Messages.Client.Entities
{
    public class CharacterDisconnectedMessageData : SNMessageData
    {

        public Character Character { get; set; }

        public CharacterDisconnectedMessageData()
        {
            DataHeader = (short)ClientMessageTypes.CharacterDisconnected;
        }

        public override void Encode(NetOutgoingMessage outgoing)
        {
            base.Encode(outgoing);

            outgoing.Write(Character);
        }

        public override void Decode(NetIncomingMessage incoming)
        {
            base.Decode(incoming);

            Character = incoming.ReadCharacter();
        }

    }
}
