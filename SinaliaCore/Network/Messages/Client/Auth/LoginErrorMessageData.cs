using Lidgren.Network;
using SinaliaCore.Network.Messages.Headers;

namespace SinaliaCore.Network.Messages.Client.Auth
{
    public class LoginErrorMessageData : SNMessageData
    {

        public AuthError Error { get; set; }

        public LoginErrorMessageData()
        {
            DataHeader = (ushort) ClientMessageTypes.LoginError;
        }

        public override void Encode(NetOutgoingMessage outgoing)
        {
            base.Encode(outgoing);

            outgoing.Write((ushort) Error);
        }

        public override void Decode(NetIncomingMessage incoming)
        {
            base.Decode(incoming);

            Error = (AuthError) incoming.ReadUInt16();
        }

    }

    public enum AuthError : ushort
    {
        NoAccount,
        BadPassword
    }
}
