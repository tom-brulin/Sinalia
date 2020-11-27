using Lidgren.Network;
using SinaliaAuthServer.Services;
using SinaliaCore.Logging;
using SinaliaCore.Network.Factories;
using SinaliaCore.Network.Messages;
using SinaliaCore.Network.Messages.Headers;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SinaliaAuthServer.Network
{
    public delegate void MessageHandler(NetConnection connection, SNMessageData data);
    public delegate void StatusChangeHandler(NetConnection connection, NetIncomingMessage incoming);

    public class IncomingMessageProcessor
    {
        private readonly ConnectionValidationService _connectionValidationService;
        private readonly AuthMessageFactory _authMessageFactory;
        private readonly ILoggingService _loggingService;

        private readonly Dictionary<ushort, MessageHandler> _handlers = new Dictionary<ushort, MessageHandler>();
        private readonly Dictionary<NetConnectionStatus, StatusChangeHandler> _statusChangeHandlers = new Dictionary<NetConnectionStatus, StatusChangeHandler>();

        public ConcurrentQueue<NetIncomingMessage> PacketQueue = new ConcurrentQueue<NetIncomingMessage>();
        public ConcurrentQueue<NetIncomingMessage> ClientStatusChangeQueue = new ConcurrentQueue<NetIncomingMessage>();

        public IncomingMessageProcessor(
            ConnectionValidationService connectionValidationService,
            AuthMessageFactory authMessageFactory,
            ILoggingService loggingService)
        {
            _connectionValidationService = connectionValidationService;
            _authMessageFactory = authMessageFactory;
            _loggingService = loggingService;
        }

        public void RegisterMessageHandler(AuthMessageTypes type, MessageHandler handler)
        {
            ushort sType = (ushort)type;
            if (!_handlers.ContainsKey(sType))
                _handlers.Add(sType, handler);
            else
                _handlers[sType] = handler;
        }

        public void RegisterMessageHandler(NetConnectionStatus type, StatusChangeHandler handler)
        {
            if (!_statusChangeHandlers.ContainsKey(type))
                _statusChangeHandlers.Add(type, handler);
            else
                _statusChangeHandlers[type] = handler;
        }

        private void HandleStatusChange(NetConnectionStatus status, NetConnection connection, NetIncomingMessage incomingMessage)
        {
            if (_statusChangeHandlers.ContainsKey(status))
                _statusChangeHandlers[status](connection, incomingMessage);
            else
                _loggingService.Log($"Could not handle {status.ToString()} status message", LogMessageType.WARNING);
        }

        private void HandleMessageData(NetConnection connection, SNMessageData messageData)
        {
            if (_handlers.ContainsKey(messageData.DataHeader))
                _handlers[messageData.DataHeader](connection, messageData);
            else
                _loggingService.Log($"Could not handle {messageData.DataHeader.ToString()} message", LogMessageType.WARNING);
        }

        public void ProcessesStatusQueue()
        {
            NetIncomingMessage msg;

            while (ClientStatusChangeQueue.TryDequeue(out msg))
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChange(msg.SenderConnection.Status, msg.SenderConnection, msg);
                        break;
                    case NetIncomingMessageType.ConnectionApproval:
                        _connectionValidationService.Validate(msg);
                        break;
                }
            }
        }

        public void ProcessesDataQueue()
        {
            NetIncomingMessage msg;

            while (PacketQueue.TryDequeue(out msg))
            {
                ushort type = msg.PeekUInt16();
                SNMessageData messageData = _authMessageFactory.GetMessageData(type);

                if (messageData == null)
                    return;

                messageData.Decode(msg);

                HandleMessageData(msg.SenderConnection, messageData);
            }
        }

    }
}
