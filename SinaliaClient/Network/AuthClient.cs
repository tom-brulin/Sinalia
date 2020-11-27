using Lidgren.Network;
using SinaliaCore;
using SinaliaCore.Logging;
using SinaliaCore.Network.Actors;
using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using SinaliaCore.Network.Messages.Headers;
using SinaliaClient.Network.MessageHandlers.Auth;

namespace SinaliaClient.Network
{
    public class AuthClient
    {

        public string AuthToken { get; set; }
        public int CliendId { get; set; }

        private readonly IServiceProvider _serviceProvider;
        private readonly IncomingMessageProcessor _incomingMessageProcessor;
        private readonly ILoggingService _loggingService;
        private readonly ClientAuthNetPeer _clientAuthNetPeer;

        public AuthClient(
            IServiceProvider serviceProvider,
            IncomingMessageProcessor incomingMessageProcessor,
            ILoggingService loggingService,
            ClientAuthNetPeer clientAuthNetPeer)
        {
            _serviceProvider = serviceProvider;
            _incomingMessageProcessor = incomingMessageProcessor;
            _loggingService = loggingService;
            _clientAuthNetPeer = clientAuthNetPeer;

            AuthToken = "";
        }

        public void Connect()
        {
            if (_clientAuthNetPeer.ConnectionStatus != NetConnectionStatus.None && _clientAuthNetPeer.ConnectionStatus != NetConnectionStatus.Disconnected)
                return;

            _loggingService.Log("[Connecting] Auth Server", LogMessageType.INFO);

            _clientAuthNetPeer.Configuration.EnableMessageType(NetIncomingMessageType.StatusChanged);
            _clientAuthNetPeer.Configuration.EnableMessageType(NetIncomingMessageType.Data);

            _clientAuthNetPeer.RegisterReceivedCallback(new SendOrPostCallback(OnReceive), new SynchronizationContext());
            _clientAuthNetPeer.FlushSendQueue();
            _clientAuthNetPeer.Start();

            NetOutgoingMessage approval = _clientAuthNetPeer.CreateMessage();
            approval.Write("AuthApproval");

            _clientAuthNetPeer.Connect(Constants.AuthHost, SharedConstants.AuthServerPort, approval);

            _loggingService.Log("[Connected] Auth Server", LogMessageType.INFO);
            RegisterMessageHandlers();
        }

        public void Disconnect()
        {
            if (_clientAuthNetPeer.ConnectionStatus != NetConnectionStatus.Connected)
                return;

            _loggingService.Log("[Disconnecting] Auth Server", LogMessageType.INFO);
            _clientAuthNetPeer.Disconnect("Disconnect");
            _loggingService.Log("[Disconnected] Auth Server", LogMessageType.INFO);
        }

        private void OnReceive(object peer)
        {
            var msg = ((NetClient)peer).ReadMessage();

            switch(msg.MessageType)
            {
                case NetIncomingMessageType.Data:
                    _incomingMessageProcessor.PacketQueue.Enqueue(msg);
                    break;
            }
        }

        private void RegisterMessageHandlers()
        {
            _incomingMessageProcessor.RegisterMessageHandler(ClientMessageTypes.LoginError, _serviceProvider.GetService<LoginErrorMessageHandler>().Handle);
            _incomingMessageProcessor.RegisterMessageHandler(ClientMessageTypes.LoginSuccess, _serviceProvider.GetService<LoginSuccessMessageHandler>().Handle);
        }

    }
}
