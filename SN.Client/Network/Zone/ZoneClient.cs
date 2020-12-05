using System;
using System.Threading;
using Lidgren.Network;
using SN.ClientProtocol.Peers;
using Microsoft.Extensions.DependencyInjection;
using SN.GlobalAbstractions.Logging;
using SN.ProtocolAbstractions.Messages.Headers;
using SN.Client.Network.MessageHandlers.Zone.Authentification;
using SN.Client.Network.MessageHandlers.Zone.Entities;

namespace SN.Client.Network.Zone
{
    public class ZoneClient
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IncomingMessageProcessor incomingMessageProcessor;
        private readonly ILoggingService loggingService;
        private readonly ZoneClientNetPeer zoneClientNetPeer;

        public ZoneClient(
            IServiceProvider serviceProvider,
            IncomingMessageProcessor incomingMessageProcessor,
            ILoggingService loggingService,
            ZoneClientNetPeer zoneClientNetPeer)
        {
            this.serviceProvider = serviceProvider;
            this.incomingMessageProcessor = incomingMessageProcessor;
            this.loggingService = loggingService;
            this.zoneClientNetPeer = zoneClientNetPeer;
        }

        public void Start()
        {
            if (zoneClientNetPeer.ConnectionStatus != NetConnectionStatus.None && zoneClientNetPeer.ConnectionStatus != NetConnectionStatus.Disconnected)
                return;

            loggingService.Log("[Start] Zone Server", LogMessageType.INFO);

            //zoneClientNetPeer.Configuration.SimulatedMinimumLatency = 0.05f;
            zoneClientNetPeer.Configuration.EnableMessageType(NetIncomingMessageType.StatusChanged);
            zoneClientNetPeer.Configuration.EnableMessageType(NetIncomingMessageType.Data);
            zoneClientNetPeer.Configuration.EnableMessageType(NetIncomingMessageType.UnconnectedData);
            
            zoneClientNetPeer.RegisterReceivedCallback(new SendOrPostCallback(OnReceive), new SynchronizationContext());
            zoneClientNetPeer.Start();

            RegisterMessageHandlers();
        }
        
        private void OnReceive(object peer)
        {
            var msg = ((NetClient)peer).ReadMessage();

            switch (msg.MessageType)
            {
                case NetIncomingMessageType.Data:
                case NetIncomingMessageType.UnconnectedData:
                    incomingMessageProcessor.PacketQueue.Enqueue(msg);
                    break;

                case NetIncomingMessageType.StatusChanged:
                    incomingMessageProcessor.ClientStatusChangeQueue.Enqueue(msg);
                    break;
            }
        }

        private void RegisterMessageHandlers()
        {
            incomingMessageProcessor.RegisterMessageHandler(NetConnectionStatus.Connected, serviceProvider.GetService<ConnectedMessageHandler>().Handle);

            incomingMessageProcessor.RegisterMessageHandler(ClientMessageTypes.PlayerLoginSuccess, serviceProvider.GetService<PlayerLoginSuccessMessageHandler>().Handle);
            incomingMessageProcessor.RegisterMessageHandler(ClientMessageTypes.PlayerLoginError, serviceProvider.GetService<PlayerLoginErrorMessageHandler>().Handle);
            incomingMessageProcessor.RegisterMessageHandler(ClientMessageTypes.SendCharacters, serviceProvider.GetService<SendCharactersMessageHandler>().Handle);
            incomingMessageProcessor.RegisterMessageHandler(ClientMessageTypes.CharacterSelected, serviceProvider.GetService<CharacterSelectedMessageHandler>().Handle);
            
            incomingMessageProcessor.RegisterMessageHandler(ClientMessageTypes.EntityPosition, serviceProvider.GetService<EntityPositionMessageHandler>().Handle);
            incomingMessageProcessor.RegisterMessageHandler(ClientMessageTypes.CharacterDisconnected, serviceProvider.GetService<CharacterDisconnectedMessageHandler>().Handle);
        }

    }
}
