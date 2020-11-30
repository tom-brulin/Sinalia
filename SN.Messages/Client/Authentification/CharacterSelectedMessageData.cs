using Lidgren.Network;
using SN.Global.Models;
using SN.ProtocolAbstractions;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Messages.Headers;

namespace SN.Messages.Client.Authentification
{
    public class CharacterSelectedMessageData : SNMessageData
    {

        public Character Character { get; set; }

        public CharacterSelectedMessageData()
        {
            DataHeader = (short)ClientMessageTypes.CharacterSelected;
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
