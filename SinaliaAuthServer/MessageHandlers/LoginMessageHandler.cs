using Lidgren.Network;
using SinaliaBackendCore.Databases;
using SinaliaCore.Logging;
using SinaliaCore.Models;
using SinaliaCore.Network.Actors;
using SinaliaCore.Network.Messages;
using SinaliaCore.Network.Messages.Auth;
using SinaliaCore.Network.Messages.Client.Auth;
using SinaliaCore.Network.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SinaliaAuthServer.MessageHandlers
{
    public class LoginMessageHandler : IAuthMessageHandler
    {
        private readonly CharactersDB _charactersDB;
        private readonly IOutgoingMessageService<AuthServerNetPeer> _outgoingMessageService;
        private readonly AuthDB _authDB;
        private readonly ILoggingService _loggingService;

        public LoginMessageHandler(
            CharactersDB charactersDB,
            IOutgoingMessageService<AuthServerNetPeer> outgoingMessageService,
            AuthDB authDB,
            ILoggingService loggingService)
        {
            _charactersDB = charactersDB;
            _outgoingMessageService = outgoingMessageService;
            _authDB = authDB;
            _loggingService = loggingService;
        }

        public void Handle(NetConnection connection, SNMessageData messageData)
        {
            var loginMessageData = (LoginMessageData) messageData;

            Task.Run(() =>
            {
                _authDB.Open();

                var dataSet = _authDB.SqlSelect(
                    "SELECT * FROM accounts WHERE email=@Email",
                    new Dictionary<string, object>()
                    {
                        { "@Email", loginMessageData.Email }
                    });

                var rows = dataSet.Tables[0].Rows;
                
                if (rows.Count > 0)
                {
                    DataRow row = rows[0];

                    string password = "" + row[2];

                    if(BCrypt.Net.BCrypt.Verify(loginMessageData.Password, password))
                    {
                        // Login
                        string authToken = Guid.NewGuid().ToString();
                        int clientId = int.Parse("" + row[0]);

                        _authDB.SqlExecute(
                            "UPDATE accounts SET authtoken=@AuthToken WHERE id=@ClientId",
                            new Dictionary<string, object>()
                            {
                                { "@AuthToken", authToken },
                                { "@ClientId", clientId }
                            });

                        LoginSuccessMessageData loginSuccessMessageData = new LoginSuccessMessageData();
                        loginSuccessMessageData.ClientId = clientId;
                        loginSuccessMessageData.Token = authToken;
                        loginSuccessMessageData.Characters = GetCharacters(clientId);
                        _outgoingMessageService.Send(loginSuccessMessageData, connection);

                        _loggingService.Log(loginMessageData.Email + " logged", LogMessageType.DEBUG);
                    }
                    else
                    {
                        // Bad password
                        LoginErrorMessageData sendLoginErrorMessageData = new LoginErrorMessageData();
                        sendLoginErrorMessageData.Error = AuthError.BadPassword;
                        _outgoingMessageService.Send(sendLoginErrorMessageData, connection);
                    }
                }
                else
                {
                    // Account doesn't exist
                    LoginErrorMessageData sendLoginErrorMessageData = new LoginErrorMessageData();
                    sendLoginErrorMessageData.Error = AuthError.NoAccount;
                    _outgoingMessageService.Send(sendLoginErrorMessageData, connection);
                }

                _authDB.Close();
            });
        }

        private List<Character> GetCharacters(int accountId)
        {
            List<Character> characters = new List<Character>();

            _charactersDB.Open();

            var dataSet = _charactersDB.SqlSelect(
                "SELECT * FROM characters WHERE account_id=@AccountId",
                new Dictionary<string, object>()
                {
                    { "@AccountId", accountId }
                });

            var rows = dataSet.Tables[0].Rows;

            foreach (DataRow row in rows)
            {
                characters.Add(new Character((int) row[0], "" + row[2], (CharacterClass)((short) row[3])));
            }

            _charactersDB.Close();

            return characters;
        }

    }
}
