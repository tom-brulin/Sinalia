using System.Collections.Generic;
using Lidgren.Network;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Services;

namespace SN.BackendProtocol.Services
{
    public class OutgoingMessageService<T> : IOutgoingMessageService<T> where T : NetPeer
    {

        private T peer;

        public OutgoingMessageService(T peer)
        {
            this.peer = peer;
        }

        public void Send(SNMessageData messageData, IList<NetConnection> clients, NetConnection except = null, NetDeliveryMethod method = NetDeliveryMethod.ReliableOrdered)
        {
            var outgoingMessage = peer.CreateMessage();
            messageData.Encode(outgoingMessage);

            if (except != null && clients.Contains(except))
                clients.Remove(except);

            if (clients.Count < 1)
                return;

            peer.SendMessage(outgoingMessage, clients, method, 0);
        }

        public void Send(SNMessageData messageData, NetConnection client, NetDeliveryMethod method = NetDeliveryMethod.ReliableOrdered)
        {
            var outgoingMessage = peer.CreateMessage();
            messageData.Encode(outgoingMessage);

            peer.SendMessage(outgoingMessage, client, method);
        }

        public void Send(SNMessageData messageData, NetDeliveryMethod method = NetDeliveryMethod.ReliableOrdered)
        {
            var outgoingMessage = peer.CreateMessage();
            messageData.Encode(outgoingMessage);

            peer.SendMessage(outgoingMessage, peer.Connections[0], method);
        }
    }
}
