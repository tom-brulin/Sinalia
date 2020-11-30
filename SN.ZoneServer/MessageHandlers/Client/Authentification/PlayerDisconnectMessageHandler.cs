using System.Collections.Generic;
using Lidgren.Network;
using SN.BackendProtocol.Actors;
using SN.BackendProtocol.Peers;
using SN.BackendProtocol.Repositories;
using SN.Messages.Client.Entities;
using SN.ProtocolAbstractions.Messages;
using SN.ProtocolAbstractions.Services;
using SN.ZoneServer.Entities;
using SN.ZoneServer.Repositories;

namespace SN.ZoneServer.MessageHandlers.Client.Authentification
{
    public class PlayerDisconnectMessageHandler : IStatusHandler
    {
        private readonly IOutgoingMessageService<ZoneServerNetPeer> outgoingMessageService;
        private readonly ClientRepository clientRepository;
        private readonly PlayerRepository playerRepository;

        public PlayerDisconnectMessageHandler(
            IOutgoingMessageService<ZoneServerNetPeer> outgoingMessageService,
            ClientRepository clientRepository,
            PlayerRepository playerRepository)
        {
            this.outgoingMessageService = outgoingMessageService;
            this.clientRepository = clientRepository;
            this.playerRepository = playerRepository;
        }

        public void Handle(NetConnection sender, NetIncomingMessage message)
        {
            if (!(sender.Tag is Player))
                return;

            var player = (Player)sender.Tag;
            playerRepository.Delete(player.Client.PersistentId);
            clientRepository.Delete(player.Client.PersistentId);

            var characterDisconnectedMessageData = new CharacterDisconnectedMessageData();
            characterDisconnectedMessageData.Character = player.Character;
            outgoingMessageService.Send(characterDisconnectedMessageData, playerRepository.GetPlayersConnection(new List<Player>(playerRepository.Players.Values)));
        }

    }
}
