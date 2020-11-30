using Lidgren.Network;

namespace SN.BackendProtocol.Actors
{
    public class PlayerClient
    {

        public string PersistentId { get; }
        public NetConnection Connection { get; }
        public string Email { get; set; }

        public PlayerClient(string persistentId, NetConnection connection)
        {
            PersistentId = persistentId;
            Connection = connection;
        }

    }
}
