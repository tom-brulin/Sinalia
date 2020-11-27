using Lidgren.Network;
using SinaliaCore.Logging;
using SinaliaCore.Network.Services;
using System.Threading.Tasks;

namespace SinaliaAuthServer.Services
{
    public class ConnectionValidationService : IConnectionValidationService
    {
        private readonly ILoggingService _loggingService;

        public ConnectionValidationService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public async Task<bool> Validate(NetIncomingMessage connectionMessage)
        {
            string token = connectionMessage.ReadString();

            if (!token.Equals("AuthApproval"))
            {
                connectionMessage.SenderConnection.Deny();
                _loggingService.Log($"Deny connection from " + connectionMessage.SenderEndPoint.ToString(), LogMessageType.INFO);
                return false;
            }

            connectionMessage.SenderConnection.Approve();
            _loggingService.Log($"New connection from " + connectionMessage.SenderEndPoint.ToString(), LogMessageType.INFO);
            return true;
        }

    }
}
