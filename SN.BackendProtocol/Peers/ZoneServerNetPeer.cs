using Lidgren.Network;

namespace SN.BackendProtocol.Peers
{
    public class ZoneServerNetPeer : NetServer
    {
        public ZoneServerNetPeer() : base(new NetPeerConfiguration("ZoneServer"))
        {

        }
    }
}
