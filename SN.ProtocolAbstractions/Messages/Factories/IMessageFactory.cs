using SN.ProtocolAbstractions.Messages;

namespace SN.ProtocolAbstractions.Messages.Factories
{
    public interface IMessageFactory
    {

        SNMessageData GetMessageData(short type);

    }
}
