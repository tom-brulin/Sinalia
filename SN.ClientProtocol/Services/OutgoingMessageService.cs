using System;
using Lidgren.Network;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Services;

namespace SN.ClientProtocol.Services
{
    public class OutgoingMessageService<T> : IOutgoingMessageService<T> where T : NetPeer
    {

        private T _peer;

        public OutgoingMessageService(T peer)
        {
            _peer = peer;
        }

        public void Send(SNMessageData messageData, NetConnection client, NetDeliveryMethod method = NetDeliveryMethod.ReliableOrdered)
        {
            throw new Exception("[OutgoingMessageService] Can't send message to a client");
        }

        public void Send(SNMessageData messageData, NetDeliveryMethod method = NetDeliveryMethod.ReliableOrdered)
        {
            var outgoingMessage = _peer.CreateMessage();
            messageData.Encode(outgoingMessage);

            _peer.SendMessage(outgoingMessage, _peer.Connections[0], method);
        }

    }
}
