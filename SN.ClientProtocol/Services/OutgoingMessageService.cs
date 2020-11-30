using System;
using System.Collections.Generic;
using Lidgren.Network;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Services;

namespace SN.ClientProtocol.Services
{
    public class OutgoingMessageService<T> : IOutgoingMessageService<T> where T : NetPeer
    {

        private T peer;

        public OutgoingMessageService(T peer)
        {
            this.peer = peer;
        }

        public void Send(SNMessageData messageData, IList<NetConnection> connections, NetConnection except = null, NetDeliveryMethod method = NetDeliveryMethod.ReliableOrdered)
        {
            throw new Exception("[OutgoingMessageService] Can't send message to specifics client");
        }

        public void Send(SNMessageData messageData, NetConnection client, NetDeliveryMethod method = NetDeliveryMethod.ReliableOrdered)
        {
            throw new Exception("[OutgoingMessageService] Can't send message to a specific client");
        }

        public void Send(SNMessageData messageData, NetDeliveryMethod method = NetDeliveryMethod.ReliableOrdered)
        {
            var outgoingMessage = peer.CreateMessage();
            messageData.Encode(outgoingMessage);

            peer.SendMessage(outgoingMessage, peer.Connections[0], method);
        }
    }
}
