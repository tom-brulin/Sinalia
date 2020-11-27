using Lidgren.Network;
using SinaliaCore.Network.Actors;
using SinaliaCore.Network.Messages;

namespace SinaliaCore.Network.Services
{
    public interface IOutgoingMessageService<T>
    {

        void Send(SNMessageData messageData, NetConnection client, NetDeliveryMethod method = NetDeliveryMethod.ReliableOrdered);

        void Send(SNMessageData messageData, NetDeliveryMethod method = NetDeliveryMethod.ReliableOrdered);

    }
}
