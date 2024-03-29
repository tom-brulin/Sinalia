﻿using System.Collections.Generic;
using Lidgren.Network;
using SN.ProtocolAbstractions.Messages;

namespace SN.ProtocolAbstractions.Services
{
    public interface IOutgoingMessageService<T>
    {

        void Send(SNMessageData messageData, IList<NetConnection> connections, NetConnection except = null, NetDeliveryMethod method = NetDeliveryMethod.ReliableOrdered);

        void Send(SNMessageData messageData, NetConnection client, NetDeliveryMethod method = NetDeliveryMethod.ReliableOrdered);

        void Send(SNMessageData messageData, NetDeliveryMethod method = NetDeliveryMethod.ReliableOrdered);

    }
}
