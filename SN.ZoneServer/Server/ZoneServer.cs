using System.Threading;
using Lidgren.Network;
using SN.BackendProtocol.Peers;
using SN.Core;
using SN.CoreAbstractions.Logging;

namespace SN.ZoneServer.Server
{
    public class ZoneServer
    {

        private readonly ServerLoop serverLoop;
        private readonly ZoneServerNetPeer zoneServerNetPeer;
        private readonly ILoggingService loggingService;

        public ZoneServer(
            ServerLoop serverLoop,
            ZoneServerNetPeer zoneServerNetPeer,
            ILoggingService loggingService)
        {
            this.serverLoop = serverLoop;
            this.zoneServerNetPeer = zoneServerNetPeer;
            this.loggingService = loggingService;
        }

        public void Start()
        {
            if (zoneServerNetPeer.Status == NetPeerStatus.Running)
            {
                loggingService.Log("[Server Already Started]", LogMessageType.WARNING);
                return;
            }

            RegisterMessageHandlers();

            zoneServerNetPeer.Configuration.Port = Constants.ZoneServerPort;
            zoneServerNetPeer.Configuration.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            zoneServerNetPeer.Configuration.EnableMessageType(NetIncomingMessageType.StatusChanged);
            zoneServerNetPeer.Configuration.EnableMessageType(NetIncomingMessageType.Data);

            zoneServerNetPeer.RegisterReceivedCallback(new SendOrPostCallback(MessageReceive), new SynchronizationContext());
            zoneServerNetPeer.FlushSendQueue();
            zoneServerNetPeer.Start();

            loggingService.Log("[Server Started]", LogMessageType.INFO);

            serverLoop.Start();
        }

        private void MessageReceive(object peer)
        {
            var msg = ((NetServer)peer).ReadMessage();

            switch (msg.MessageType)
            {
                case NetIncomingMessageType.StatusChanged:
                case NetIncomingMessageType.ConnectionApproval:

                    break;

                case NetIncomingMessageType.Data:

                    break;
            }
        }

        private void RegisterMessageHandlers()
        {

        }

    }
}
