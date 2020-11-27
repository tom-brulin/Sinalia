using SinaliaCore.Network.Messages;

namespace SinaliaCore.Network.Factories
{
    public interface IMessageFactory
    {

        SNMessageData GetMessageData(ushort type);

    }
}
