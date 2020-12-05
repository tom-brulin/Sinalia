using System.Collections.Generic;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using SN.BackendProtocol.Peers;
using SN.Messages.Client.Entities;
using SN.ProtocolAbstractions.Services;
using SN.ZoneServer.Repositories;

namespace SN.ZoneServer.Entities
{
    public class EntitiesProcessor
    {
        private readonly IOutgoingMessageService<ZoneServerNetPeer> outgoingMessageService;
        private readonly PlayerRepository playerRepository;

        public EntitiesProcessor(
            IOutgoingMessageService<ZoneServerNetPeer> outgoingMessageService,
            PlayerRepository playerRepository)
        {
            this.outgoingMessageService = outgoingMessageService;
            this.playerRepository = playerRepository;
        }

        public void ProcessEntities(float deltaTime)
        {
            foreach (var p in playerRepository.Players.Values)
            {
                if (p.Velocity.Equals(Vector2.Zero))
                    continue;

                p.Position.X += p.Velocity.X * p.Speed * deltaTime;
                p.Position.Y += p.Velocity.Y * p.Speed * deltaTime;

                if (p.OldPosition.X == p.Position.X && p.OldPosition.Y == p.Position.Y)
                    continue;

                var entityPositionMesageData = new EntityPositionMessageData();
                entityPositionMesageData.Uid = p.Client.PersistentId;
                entityPositionMesageData.X = p.Position.X;
                entityPositionMesageData.Y = p.Position.Y;
                outgoingMessageService.Send(entityPositionMesageData, playerRepository.GetPlayersConnection(new List<Player>(playerRepository.Players.Values)));

                p.OldPosition.X = p.Position.X;
                p.OldPosition.Y = p.Position.Y;

                System.Console.WriteLine(p.Position.X + " " + p.Position.Y);
            }
        }

        public void NewPlayerConnected(Player player)
        {
            // Send new player position to all players
            var entityPositionMesageData = new EntityPositionMessageData();
            entityPositionMesageData.Uid = player.Client.PersistentId;
            entityPositionMesageData.X = player.Position.X;
            entityPositionMesageData.Y = player.Position.Y;
            outgoingMessageService.Send(entityPositionMesageData, playerRepository.GetPlayersConnection(new List<Player>(playerRepository.Players.Values)), player.Client.Connection);

            // Send all players position to the new player
            foreach (var p in playerRepository.Players.Values)
            {
                if (p == player)
                    continue;

                System.Console.WriteLine("Sending " + p.Character.Name + " position to " + player.Character.Name);

                entityPositionMesageData = new EntityPositionMessageData();
                entityPositionMesageData.Uid = p.Client.PersistentId;
                entityPositionMesageData.X = p.Position.X;
                entityPositionMesageData.Y = p.Position.Y;
                outgoingMessageService.Send(entityPositionMesageData, player.Client.Connection);
            }
        }

    }
}
