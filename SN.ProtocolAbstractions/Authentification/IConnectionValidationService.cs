using System.Threading.Tasks;
using Lidgren.Network;

namespace SN.ProtocolAbstractions.Authentification
{
    public interface IConnectionValidationService
    {

        Task<bool> Validate(NetIncomingMessage message);

    }
}
