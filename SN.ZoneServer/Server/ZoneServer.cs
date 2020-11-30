using System;
using System.Threading;
using Lidgren.Network;
using SN.BackendProtocol.Peers;
using SN.Global;
using SN.GlobalAbstractions.Logging;
using SN.ProtocolAbstractions.Messages.Headers;
using Microsoft.Extensions.DependencyInjection;
using SN.ZoneServer.MessageHandlers.Client.Authentification;
using SN.ZoneServer.MessageHandlers.Client.Players;

namespace SN.ZoneServer.Server
{
    public class ZoneServer
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IncomingMessageProcessor incomingMessageProcessor;
        private readonly ServerLoop serverLoop;
        private readonly ZoneServerNetPeer zoneServerNetPeer;
        private readonly ILoggingService loggingService;

        public ZoneServer(
            IServiceProvider serviceProvider,
            IncomingMessageProcessor incomingMessageProcessor,
            ServerLoop serverLoop,
            ZoneServerNetPeer zoneServerNetPeer,
            ILoggingService loggingService)
        {
            this.serviceProvider = serviceProvider;
            this.incomingMessageProcessor = incomingMessageProcessor;
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
            zoneServerNetPeer.Configuration.EnableMessageType(NetIncomingMessageType.UnconnectedData);

            zoneServerNetPeer.RegisterReceivedCallback(new SendOrPostCallback(MessageReceive), new SynchronizationContext());
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
                    incomingMessageProcessor.ClientStatusChangeQueue.Enqueue(msg);
                    break;

                case NetIncomingMessageType.Data:
                case NetIncomingMessageType.UnconnectedData:
                    incomingMessageProcessor.PacketQueue.Enqueue(msg);
                    break;
            }
        }

        private void RegisterMessageHandlers()
        {
            incomingMessageProcessor.RegisterMessageHandler(NetConnectionStatus.Disconnected, serviceProvider.GetService<PlayerDisconnectMessageHandler>().Handle);

            incomingMessageProcessor.RegisterMessageHandler(ZoneMessageTypes.PlayerLogin, serviceProvider.GetService<PlayerLoginMessageHandler>().Handle);
            incomingMessageProcessor.RegisterMessageHandler(ZoneMessageTypes.RequestCharacters, serviceProvider.GetService<RequestCharactersMessageHandler>().Handle);
            incomingMessageProcessor.RegisterMessageHandler(ZoneMessageTypes.SelectCharacter, serviceProvider.GetService<SelectCharacterMessageHandler>().Handle);
            incomingMessageProcessor.RegisterMessageHandler(ZoneMessageTypes.CharacterLoaded, serviceProvider.GetService<CharacterLoadedMessageHandler>().Handle);

            incomingMessageProcessor.RegisterMessageHandler(ZoneMessageTypes.PlayerDirection, serviceProvider.GetService<PlayerDirectionMessageHandler>().Handle);
        }

    }
}
