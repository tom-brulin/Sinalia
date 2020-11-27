using Lidgren.Network;

namespace SinaliaCore.Network.Actors
{
    public interface IClient
    {

        string PersistentId { get; }
        NetConnection Connection { get; }

    }
}
