using System.Net;

namespace SN.ProtocolAbstractions.Messages
{
    public interface IUnconnectedMessageHandler
    {

        void Handle(IPEndPoint sender, SNMessageData messageData);

    }
}
