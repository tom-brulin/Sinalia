using System.Collections.Generic;
using Lidgren.Network;
using SN.Global.Models;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Messages.Headers;
using SN.ProtocolAbstractions;

namespace SN.Messages.Client.Authentification
{
    public class SendCharactersMessageData : SNMessageData
    {

        public List<Character> Characters { get; set; }

        private int charactersAmount;

        public SendCharactersMessageData()
        {
            DataHeader = (short)ClientMessageTypes.SendCharacters;
        }

        public override void Encode(NetOutgoingMessage outgoing)
        {
            base.Encode(outgoing);

            outgoing.Write(Characters.Count);

            foreach (var character in Characters)
            {
                outgoing.Write(character);
            }
        }

        public override void Decode(NetIncomingMessage incoming)
        {
            base.Decode(incoming);

            Characters = new List<Character>();
            charactersAmount = incoming.ReadInt32();

            for (int i = 0; i < charactersAmount; i++)
            {
                Characters.Add(incoming.ReadCharacter());
            }
        }

    }
}
