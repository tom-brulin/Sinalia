using Lidgren.Network;

namespace SinaliaCore.Network.Actors
{

    public class MasterServerNetPeer : NetServer
    {
        public MasterServerNetPeer() : base(new NetPeerConfiguration("MasterServer"))
        {

        }
    }

    public class ZoneServerNetPeer : NetServer
    {
        public ZoneServerNetPeer() : base(new NetPeerConfiguration("ZoneServer"))
        {

        }
    }

    public class ChatServerNetPeer : NetServer
    {
        public ChatServerNetPeer() : base(new NetPeerConfiguration("ChatServer"))
        {

        }
    }

    public class AuthServerNetPeer : NetServer
    {
        public AuthServerNetPeer() : base(new NetPeerConfiguration("AuthServer"))
        {

        }
    }

    public class ClientAuthNetPeer : NetClient
    {
        public ClientAuthNetPeer() : base(new NetPeerConfiguration("AuthServer"))
        {

        }
    }

}
