using Lidgren.Network;

namespace SN.ClientProtocol.Peers
{
    public class ZoneClientNetPeer : NetClient
    {

        public ZoneClientNetPeer() : base(new NetPeerConfiguration("Zone"))
        {

        }

    }
}
