using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Lidgren.Network;
using SN.BackendProtocol.Authentification;
using SN.GlobalAbstractions.Logging;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Messages.Factories;
using SN.ProtocolAbstractions.Messages.Headers;

namespace SN.ZoneServer.Server
{
    public delegate void MessageHandler(NetConnection connection, SNMessageData data);
    public delegate void MessageUnconnectedHandler(IPEndPoint ep, SNMessageData data);
    public delegate void StatusChangeHandler(NetConnection connection, NetIncomingMessage incoming);

    public class IncomingMessageProcessor
    {
        private readonly ConnectionValidationService connectionValidationService;
        private readonly ZoneMessageFactory zoneMessageFactory;
        private readonly ILoggingService loggingService;

        private readonly Dictionary<ZoneMessageTypes, MessageHandler> _handlers = new Dictionary<ZoneMessageTypes, MessageHandler>();
        private readonly Dictionary<ZoneMessageTypes, MessageUnconnectedHandler> _handlersUnconnected = new Dictionary<ZoneMessageTypes, MessageUnconnectedHandler>();
        private readonly Dictionary<NetConnectionStatus, StatusChangeHandler> _statusChangeHandlers = new Dictionary<NetConnectionStatus, StatusChangeHandler>();

        public ConcurrentQueue<NetIncomingMessage> PacketQueue = new ConcurrentQueue<NetIncomingMessage>();
        public ConcurrentQueue<NetIncomingMessage> ClientStatusChangeQueue = new ConcurrentQueue<NetIncomingMessage>();

        public IncomingMessageProcessor(
            ConnectionValidationService connectionValidationService,
            ZoneMessageFactory zoneMessageFactory,
            ILoggingService loggingService)
        {
            this.connectionValidationService = connectionValidationService;
            this.zoneMessageFactory = zoneMessageFactory;
            this.loggingService = loggingService;
        }

        public void RegisterMessageHandler(ZoneMessageTypes type, MessageHandler handler)
        {
            if (!_handlers.ContainsKey(type))
                _handlers.Add(type, handler);
            else
                _handlers[type] = handler;
        }

        public void RegisterMessageHandler(ZoneMessageTypes type, MessageUnconnectedHandler handler)
        {
            if (!_handlers.ContainsKey(type))
                _handlersUnconnected.Add(type, handler);
            else
                _handlersUnconnected[type] = handler;
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
                loggingService.Log($"Could not handle {status.ToString()} status message", LogMessageType.WARNING);
        }

        private void HandleMessageData(NetConnection connection, SNMessageData messageData)
        {
            if (_handlers.ContainsKey((ZoneMessageTypes)messageData.DataHeader))
                _handlers[(ZoneMessageTypes)messageData.DataHeader](connection, messageData);
            else
                loggingService.Log($"Could not handle {messageData.DataHeader.ToString()} message", LogMessageType.WARNING);
        }

        private void HandleMessageData(IPEndPoint ep, SNMessageData messageData)
        {
            if (_handlersUnconnected.ContainsKey((ZoneMessageTypes)messageData.DataHeader))
                _handlersUnconnected[(ZoneMessageTypes)messageData.DataHeader](ep, messageData);
            else
                loggingService.Log($"Could not handle {messageData.DataHeader.ToString()} message", LogMessageType.WARNING);
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
                        connectionValidationService.Validate(msg);
                        break;
                }
            }
        }

        public void ProcessesDataQueue()
        {
            NetIncomingMessage msg;

            while (PacketQueue.TryDequeue(out msg))
            {
                SNMessageData messageData = zoneMessageFactory.GetMessageData(msg.PeekInt16());

                if (messageData == null)
                    return;

                messageData.Decode(msg);

                switch(msg.MessageType)
                {
                    case NetIncomingMessageType.UnconnectedData:
                        HandleMessageData(msg.SenderEndPoint, messageData);
                        break;
                    case NetIncomingMessageType.Data:
                        HandleMessageData(msg.SenderConnection, messageData);
                        break;
                }
            }
        }

    }
}
