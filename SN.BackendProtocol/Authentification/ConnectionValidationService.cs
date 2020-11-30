using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Lidgren.Network;
using Microsoft.IdentityModel.Tokens;
using SN.BackendProtocol.Actors;
using SN.BackendProtocol.Repositories;
using SN.GlobalAbstractions.Logging;
using SN.ProtocolAbstractions.Authentification;
using SN.ZoneServer.Repositories;

namespace SN.BackendProtocol.Authentification
{
    public class ConnectionValidationService : IConnectionValidationService
    {
        private readonly PlayerRepository playerRepository;
        private readonly ClientRepository clientRepository;
        private readonly ILoggingService loggingService;
        private readonly TokenRepository tokenRepository;
        private readonly TokenManager tokenManager;

        public ConnectionValidationService(
            PlayerRepository playerRepository,
            ClientRepository clientRepository,
            ILoggingService loggingService,
            TokenRepository tokenRepository,
            TokenManager tokenManager)
        {
            this.playerRepository = playerRepository;
            this.clientRepository = clientRepository;
            this.loggingService = loggingService;
            this.tokenRepository = tokenRepository;
            this.tokenManager = tokenManager;
        }

        public async Task<bool> Validate(NetIncomingMessage connectionMessage)
        {
            string token = connectionMessage.ReadString();

            if (!tokenManager.ValidateToken(token) || !tokenRepository.Has(token))
            {
                connectionMessage.SenderConnection.Deny();
                loggingService.Log($"Invalid token ({connectionMessage.SenderEndPoint.ToString()})", LogMessageType.WARNING);
                return false;
            }

            SecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
            if (securityTokenHandler.CanReadToken(token))
            {
                var tokenDecoded = securityTokenHandler.ReadToken(token) as JwtSecurityToken;

                // TODO - Block Client if Cooldown

                tokenRepository.Delete(token);
                ApproveClient(tokenDecoded.Subject, tokenDecoded.Payload["name"].ToString(), connectionMessage.SenderConnection);
            };

            return true;
        }

        private void ApproveClient(string persistentId, string email, NetConnection connection)
        {
            var client = new PlayerClient(persistentId, connection);
            connection.Tag = client;
            client.Email = email;
            client.Connection.Approve();
            loggingService.Log($"client {persistentId} {email} validated", LogMessageType.INFO);

            var existingClient = clientRepository.Get(persistentId);
            if (existingClient != null)
            {
                loggingService.Log($"client {persistentId} {email} already found, kicking", LogMessageType.INFO);
                clientRepository.Delete(persistentId);
                playerRepository.Delete(client.PersistentId);
                existingClient.Connection.Disconnect("Someone connecting to your account");
            }

            clientRepository.Add(client);
        }

    }
}
