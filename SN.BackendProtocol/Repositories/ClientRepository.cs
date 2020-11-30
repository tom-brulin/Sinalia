using System.Collections.Concurrent;
using SN.BackendProtocol.Actors;

namespace SN.BackendProtocol.Repositories
{
    public class ClientRepository
    {

        private ConcurrentDictionary<string, PlayerClient> clients = new ConcurrentDictionary<string, PlayerClient>();

        public ClientRepository()
        {

        }

        public void Add(PlayerClient client)
        {
            clients.TryAdd(client.PersistentId, client);
        }

        public bool Delete(string persistentId)
        {
            return clients.TryRemove(persistentId, out _);
        }

        public PlayerClient Get(string persistentId)
        {
            clients.TryGetValue(persistentId, out PlayerClient value);
            return value;
        }

        public bool Has(string persistentId)
        {
            return clients.ContainsKey(persistentId);
        }

    }
}
