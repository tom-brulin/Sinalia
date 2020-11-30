using System.Collections.Generic;
using Lidgren.Network;
using SN.BackendProtocol.Actors;
using SN.BackendProtocol.Databases;
using SN.BackendProtocol.Peers;
using SN.Global.Models;
using SN.Messages.Client.Authentification;
using SN.Messages.Zone.Authentification;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Services;
using SN.ZoneServer.Entities;
using SN.ZoneServer.Repositories;

namespace SN.ZoneServer.MessageHandlers.Client.Authentification
{
    public class SelectCharacterMessageHandler : IMessageHandler
    {
        private readonly EntitiesProcessor entitiesProcessor;
        private readonly PlayerRepository playerRepository;
        private readonly IOutgoingMessageService<ZoneServerNetPeer> outgoingMessageService;
        private readonly LogDb logDb;

        public SelectCharacterMessageHandler(
            EntitiesProcessor entitiesProcessor,
            PlayerRepository playerRepository,
            IOutgoingMessageService<ZoneServerNetPeer> outgoingMessageService,
            LogDb logDb)
        {
            this.entitiesProcessor = entitiesProcessor;
            this.playerRepository = playerRepository;
            this.outgoingMessageService = outgoingMessageService;
            this.logDb = logDb;
        }

        public void Handle(NetConnection sender, SNMessageData messageData)
        {
            if (!(sender.Tag is PlayerClient))
                return;

            var client = (PlayerClient)sender.Tag;
            var selectCharacterMessageData = (SelectCharacterMessageData)messageData;

            if (!IsAccountCharacter(client.PersistentId, selectCharacterMessageData.CharacterId, out Character character))
                return;

            var characterSelectedMessageData = new CharacterSelectedMessageData();
            characterSelectedMessageData.Character = character;
            outgoingMessageService.Send(characterSelectedMessageData, sender);

            var player = new Player(client, character);
            playerRepository.Add(player);
            sender.Tag = player;

            // Send already connected players positions
            entitiesProcessor.NewPlayerConnected(player);
        }

        private bool IsAccountCharacter(string persistentId, int characterId, out Character character)
        {
            logDb.Open();
            var dataSet = logDb.SqlSelect(
                "SELECT * FROM characters WHERE id=@CharacterId AND account_persistent_id=@PersistentId", 
                new Dictionary<string, object>() { { "@CharacterId", characterId } , { "@PersistentId", persistentId } });

            var rows = dataSet.Tables[0].Rows;

            logDb.Close();

            if (rows.Count > 0)
                character = new Character(persistentId, int.Parse(rows[0][0].ToString()), rows[0][2].ToString(), 0, 0);
            else
                character = null;

            return rows.Count > 0;
        }

    }
}
