using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using Lidgren.Network;
using SN.GlobalAbstractions.Logging;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Messages.Factories;
using SN.ProtocolAbstractions.Messages.Headers;

namespace SN.Client.Network
{
    public delegate void MessageHandler(NetConnection connection, SNMessageData data);
    public delegate void UnconnectedMessageHandler(IPEndPoint ep, SNMessageData data);
    public delegate void StatusChangeHandler(NetConnection connection, NetIncomingMessage incoming);

    public class IncomingMessageProcessor
    {

        private readonly ClientMessageFactory clientMessageFactory;
        private readonly ILoggingService loggingService;

        private readonly Dictionary<short, MessageHandler> handlers = new Dictionary<short, MessageHandler>();
        private readonly Dictionary<short, UnconnectedMessageHandler> handlersUnconnected = new Dictionary<short, UnconnectedMessageHandler>();
        private readonly Dictionary<NetConnectionStatus, StatusChangeHandler> statusChangeHandlers = new Dictionary<NetConnectionStatus, StatusChangeHandler>();

        public ConcurrentQueue<NetIncomingMessage> PacketQueue = new ConcurrentQueue<NetIncomingMessage>();
        public ConcurrentQueue<NetIncomingMessage> ClientStatusChangeQueue = new ConcurrentQueue<NetIncomingMessage>();

        public IncomingMessageProcessor(
            ClientMessageFactory clientMessageFactory,
            ILoggingService loggingService)
        {
            this.clientMessageFactory = clientMessageFactory;
            this.loggingService = loggingService;
        }

        public void RegisterMessageHandler(ClientMessageTypes type, MessageHandler handler)
        {
            if (!handlers.ContainsKey((short)type))
                handlers.Add((short)type, handler);
            else
                handlers[(short)type] = handler;
        }

        public void RegisterMessageHandler(ClientMessageTypes type, UnconnectedMessageHandler handler)
        {
            if (!handlersUnconnected.ContainsKey((short)type))
                handlersUnconnected.Add((short)type, handler);
            else
                handlersUnconnected[(short)type] = handler;
        }

        public void RegisterMessageHandler(NetConnectionStatus type, StatusChangeHandler handler)
        {
            if (!statusChangeHandlers.ContainsKey(type))
                statusChangeHandlers.Add(type, handler);
            else
                statusChangeHandlers[type] = handler;
        }

        private void HandleStatusChange(NetConnectionStatus status, NetConnection connection, NetIncomingMessage incomingMessage)
        {
            if (statusChangeHandlers.ContainsKey(status))
                statusChangeHandlers[status](connection, incomingMessage);
            else
                loggingService.Log($"Could not handle {status.ToString()} status message", LogMessageType.WARNING);
        }

        private void HandleMessageData(NetConnection connection, SNMessageData messageData)
        {
            if (handlers.ContainsKey(messageData.DataHeader))
                handlers[messageData.DataHeader](connection, messageData);
            else
                loggingService.Log($"Could not handle {messageData.DataHeader.ToString()} message", LogMessageType.WARNING);
        }

        private void HandleUnconnectedMessageData(IPEndPoint ep, SNMessageData messageData)
        {
            if (handlersUnconnected.ContainsKey(messageData.DataHeader))
                handlersUnconnected[messageData.DataHeader](ep, messageData);
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
                }
            }
        }

        public void ProcessesDataQueue()
        {
            NetIncomingMessage msg;

            while (PacketQueue.TryDequeue(out msg))
            {
                SNMessageData messageData = clientMessageFactory.GetMessageData(msg.PeekInt16());

                if (messageData == null)
                    return;

                messageData.Decode(msg);

                switch(msg.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        HandleMessageData(msg.SenderConnection, messageData);
                        break;
                    case NetIncomingMessageType.UnconnectedData:
                        HandleUnconnectedMessageData(msg.SenderEndPoint, messageData);
                        break;
                }

            }
        }

    }

}

