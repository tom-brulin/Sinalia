using Lidgren.Network;

namespace SinaliaCore.Network.Actors
{
    public class PlayerClient : IClient
    {

        public string PersistentId { get; }
        public NetConnection Connection { get; }

        public PlayerClient(string persistentId, NetConnection connection)
        {
            PersistentId = persistentId;
            Connection = connection;
        }

    }
}
