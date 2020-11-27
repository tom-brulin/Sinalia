using Lidgren.Network;
using SinaliaCore.Models;
using SinaliaCore.Network.Messages.Headers;
using System.Collections.Generic;

namespace SinaliaCore.Network.Messages.Client.Auth
{
    public class LoginSuccessMessageData : SNMessageData
    {

        public int ClientId { get; set; }
        public string Token { get; set; }
        public List<Character> Characters { get; set; }

        private int _charactersAmount;

        public LoginSuccessMessageData()
        {
            DataHeader = (ushort)ClientMessageTypes.LoginSuccess;
        }

        public override void Encode(NetOutgoingMessage outgoing)
        {
            base.Encode(outgoing);

            outgoing.Write(ClientId);
            outgoing.Write(Token);
            _charactersAmount = Characters.Count;
            outgoing.Write(_charactersAmount);

            foreach (var character in Characters)
            {
                outgoing.Write(character);
            }
        }

        public override void Decode(NetIncomingMessage incoming)
        {
            base.Decode(incoming);

            ClientId = incoming.ReadInt32();
            Token = incoming.ReadString();
            _charactersAmount = incoming.ReadInt32();

            Characters = new List<Character>();

            for (int i = 0; i < _charactersAmount; i++)
            {
                Characters.Add(incoming.ReadCharacter());
            }
        }

    }
}
