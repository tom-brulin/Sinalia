using Lidgren.Network;
using SinaliaCore.Network.Messages.Headers;

namespace SinaliaCore.Network.Messages.Auth
{
    public class LoginMessageData : SNMessageData
    {

        public string Email { get; set; }
        public string Password { get; set; }

        public LoginMessageData()
        {
            DataHeader = (ushort) AuthMessageTypes.Login;
        }

        public override void Encode(NetOutgoingMessage outgoing)
        {
            base.Encode(outgoing);

            outgoing.Write(Email);
            outgoing.Write(Password);
        }

        public override void Decode(NetIncomingMessage incoming)
        {
            base.Decode(incoming);

            Email = incoming.ReadString();
            Password = incoming.ReadString();
        }

    }
}
