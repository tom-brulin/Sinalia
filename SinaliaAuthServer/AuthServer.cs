using Lidgren.Network;
using SinaliaAuthServer.MessageHandlers;
using SinaliaAuthServer.Network;
using SinaliaCore;
using SinaliaCore.Logging;
using SinaliaCore.Network.Actors;
using SinaliaCore.Network.Messages.Headers;
using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace SinaliaAuthServer
{
    public class AuthServer
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly IncomingMessageProcessor _incomingMessageProcessor;
        private readonly ServerLoop _serverLoop;
        private readonly ILoggingService _loggingService;
        private readonly AuthServerNetPeer _authServerNetPeer;

        public AuthServer(
            IServiceProvider serviceProvider,
            IncomingMessageProcessor incomingMessageProcessor,
            ServerLoop serverLoop,
            ILoggingService loggingService,
            AuthServerNetPeer authServerNetPeer)
        {
            _serviceProvider = serviceProvider;
            _incomingMessageProcessor = incomingMessageProcessor;
            _serverLoop = serverLoop;
            _loggingService = loggingService;
            _authServerNetPeer = authServerNetPeer;
        }

        public void Start()
        {
            RegisterMessageHandlers();

            _authServerNetPeer.Configuration.Port = SharedConstants.AuthServerPort;
            _authServerNetPeer.Configuration.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            _authServerNetPeer.Configuration.EnableMessageType(NetIncomingMessageType.StatusChanged);
            _authServerNetPeer.Configuration.EnableMessageType(NetIncomingMessageType.Data);

            _authServerNetPeer.RegisterReceivedCallback(new SendOrPostCallback(MessageReceive), new SynchronizationContext());
            _authServerNetPeer.FlushSendQueue();
            _authServerNetPeer.Start();

            _loggingService.Log("[Server Started]", LogMessageType.INFO);

            _serverLoop.Start();
        }

        public void Stop()
        {
            _serverLoop.Stop();
            _authServerNetPeer.Shutdown("AuthServer shutdown");
            _loggingService.Log("[Server Stoped]", LogMessageType.INFO);
        }

        private void MessageReceive(object peer)
        {
            var msg = ((NetServer) peer).ReadMessage();

            switch (msg.MessageType)
            {
                case NetIncomingMessageType.StatusChanged:
                case NetIncomingMessageType.ConnectionApproval:
                    _incomingMessageProcessor.ClientStatusChangeQueue.Enqueue(msg);
                    break;

                case NetIncomingMessageType.Data:
                    _incomingMessageProcessor.PacketQueue.Enqueue(msg);
                    break;
            }
        }

        private void RegisterMessageHandlers()
        {
            _incomingMessageProcessor.RegisterMessageHandler(AuthMessageTypes.Login, _serviceProvider.GetService<LoginMessageHandler>().Handle);
        }

    }
}
