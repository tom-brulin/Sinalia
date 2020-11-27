using Lidgren.Network;
using SinaliaCore.Logging;
using SinaliaCore.Network.Factories;
using SinaliaCore.Network.Messages;
using SinaliaCore.Network.Messages.Headers;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SinaliaClient.Network
{
    public delegate void MessageHandler(NetConnection connection, SNMessageData data);

    public class IncomingMessageProcessor
    {
        private readonly ClientMessageFactory _clientMessageFactory;
        private readonly ILoggingService _loggingService;

        private readonly Dictionary<ushort, MessageHandler> _handlers = new Dictionary<ushort, MessageHandler>();

        public ConcurrentQueue<NetIncomingMessage> PacketQueue = new ConcurrentQueue<NetIncomingMessage>();

        public IncomingMessageProcessor(
            ClientMessageFactory clientMessageFactory,
            ILoggingService loggingService)
        {
            _clientMessageFactory = clientMessageFactory;
            _loggingService = loggingService;
        }

        public void RegisterMessageHandler(ClientMessageTypes type, MessageHandler handler)
        {
            ushort sType = (ushort)type;
            if (!_handlers.ContainsKey(sType))
                _handlers.Add(sType, handler);
            else
                _handlers[sType] = handler;
        }

        private void HandleMessageData(NetConnection connection, SNMessageData messageData)
        {
            if (_handlers.ContainsKey(messageData.DataHeader))
                _handlers[messageData.DataHeader](connection, messageData);
            else
                _loggingService.Log($"Could not handle {messageData.DataHeader.ToString()} message", LogMessageType.WARNING);
        }

        public void ProcessesDataQueue()
        {
            NetIncomingMessage msg;

            while (PacketQueue.TryDequeue(out msg))
            {
                ushort type = msg.PeekUInt16();
                SNMessageData messageData = _clientMessageFactory.GetMessageData(type);

                if (messageData == null)
                    return;

                messageData.Decode(msg);

                HandleMessageData(msg.SenderConnection, messageData);
            }
        }

    }
}
