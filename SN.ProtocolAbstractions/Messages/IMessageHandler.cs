using Lidgren.Network;

namespace SN.ProtocolAbstractions.Messages
{
    public interface IMessageHandler
    {

        void Handle(NetConnection sender, SNMessageData messageData);

    }
}
