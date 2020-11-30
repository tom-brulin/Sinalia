using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using SN.BackendProtocol.Authentification;
using SN.BackendProtocol.Databases;
using SN.BackendProtocol.Peers;
using SN.BackendProtocol.Repositories;
using SN.Messages.Client.Authentification;
using SN.Messages.Zone.Authentification;
using SN.ProtocolAbstractions.Messages;

namespace SN.ZoneServer.MessageHandlers.Client.Authentification
{
    public class PlayerLoginMessageHandler : IUnconnectedMessageHandler
    {
        private readonly TokenRepository tokenRepository;
        private readonly ZoneServerNetPeer zoneServerNetPeer;
        private readonly LogDb logDb;
        private readonly TokenManager tokenManager;

        public PlayerLoginMessageHandler(
            TokenRepository tokenRepository,
            ZoneServerNetPeer zoneServerNetPeer,
            LogDb logDb,
            TokenManager tokenManager)
        {
            this.tokenRepository = tokenRepository;
            this.zoneServerNetPeer = zoneServerNetPeer;
            this.logDb = logDb;
            this.tokenManager = tokenManager;
        }

        public void Handle(IPEndPoint sender, SNMessageData messageData)
        {
            var playerLoginMessageData = (PlayerLoginMessageData)messageData;

            Task.Run(() =>
            {
                if (IsValidUser(playerLoginMessageData.Email, playerLoginMessageData.Password, out string persistentId))
                {
                    string token = tokenManager.GenerateToken(playerLoginMessageData.Email, persistentId);

                    tokenRepository.Add(token);

                    var msg = zoneServerNetPeer.CreateMessage();
                    var playerLoginSuccessMessageData = new PlayerLoginSuccessMessageData();
                    playerLoginSuccessMessageData.Token = token;
                    playerLoginSuccessMessageData.Encode(msg);
                    zoneServerNetPeer.SendUnconnectedMessage(msg, sender);
                }
                else
                {
                    var msg = zoneServerNetPeer.CreateMessage();
                    var playerLoginErrorMessageData = new PlayerLoginErrorMessageData();
                    playerLoginErrorMessageData.Encode(msg);
                    zoneServerNetPeer.SendUnconnectedMessage(msg, sender);
                }
            });
        }

        private bool IsValidUser(string email, string password, out string persistentId)
        {
            var rows = GetUser(email);

            if (rows.Count <= 0)
            {
                persistentId = "";
                return false;
            }

            var row = rows[0];

            if (!IsRightPassword(row[2].ToString(), password))
            {
                persistentId = "";
                return false;
            }

            persistentId = row[0].ToString();
            return true;
        }

        private bool IsRightPassword(string dbPassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, dbPassword);
        }

        private DataRowCollection GetUser(string email)
        {
            logDb.Open();
            var dataSet = logDb.SqlSelect(
                "SELECT * FROM accounts WHERE email=@Email",
                new Dictionary<string, object>()
                {
                    { "@Email", email }
                });
            logDb.Close();

            return dataSet.Tables[0].Rows;
        }

    }
}
