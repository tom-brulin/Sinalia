using SinaliaCore.Network.Actors;

namespace SinaliaCore.Network.Messages
{
    public class SNIncomingMessage
    {

        public IClient Client { get; }
        public SNMessageData Data { get; }

        public SNIncomingMessage(IClient client, SNMessageData data)
        {
            Client = client;
            Data = data;
        }

    }
}
