using System.Collections.Generic;
using System.Data;
using Lidgren.Network;
using SN.BackendProtocol.Actors;
using SN.BackendProtocol.Databases;
using SN.BackendProtocol.Peers;
using SN.Global.Models;
using SN.Messages.Client.Authentification;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Services;

namespace SN.ZoneServer.MessageHandlers.Client.Authentification
{
    public class RequestCharactersMessageHandler : IMessageHandler
    {
        private readonly IOutgoingMessageService<ZoneServerNetPeer> outgoingMessageService;
        private readonly LogDb logDb;

        public RequestCharactersMessageHandler(
            IOutgoingMessageService<ZoneServerNetPeer> outgoingMessageService,
            LogDb logDb)
        {
            this.outgoingMessageService = outgoingMessageService;
            this.logDb = logDb;
        }

        public void Handle(NetConnection sender, SNMessageData messageData)
        {
            if (!(sender.Tag is PlayerClient))
                return;

            var client = (PlayerClient)sender.Tag;

            var sendCharactersMessageData = new SendCharactersMessageData();
            sendCharactersMessageData.Characters = GetCharacters(client.PersistentId);
            outgoingMessageService.Send(sendCharactersMessageData, sender);
        }

        public List<Character> GetCharacters(string persistentId)
        {
            var characters = new List<Character>();

            logDb.Open();
            var dataSet = logDb.SqlSelect("SELECT * FROM characters WHERE account_persistent_id=@PersistentId", new Dictionary<string, object>() { { "@PersistentId", persistentId } });
            var rows = dataSet.Tables[0].Rows;

            foreach (DataRow row in rows)
            {
                characters.Add(new Character(persistentId, int.Parse(row[0].ToString()), row[2].ToString(), 0, 0));
            }

            logDb.Close();

            return characters;
        }

    }
}
