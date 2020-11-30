using Lidgren.Network;

namespace SN.ProtocolAbstractions.Messages
{
    public interface IStatusHandler
    {

        void Handle(NetConnection sender, NetIncomingMessage message);

    }
}
