using Lidgren.Network;
using SinaliaCore.Network.Actors;

namespace SinaliaCore.Network.Messages
{
    public interface IMessageHandler
    {

        void Handle(IClient client, SNMessageData messageData);

    }
}
