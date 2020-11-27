using Lidgren.Network;
using System.Threading.Tasks;

namespace SinaliaCore.Network.Services
{
    public interface IConnectionValidationService
    {

        Task<bool> Validate(NetIncomingMessage connectionMessage);

    }
}
