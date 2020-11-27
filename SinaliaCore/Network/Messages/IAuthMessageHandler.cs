using Lidgren.Network;

namespace SinaliaCore.Network.Messages
{
    public interface IAuthMessageHandler
    {

        void Handle(NetConnection connection, SNMessageData messageData);

    }
}
