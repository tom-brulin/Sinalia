using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Lidgren.Network;
using SN.ZoneServer.Entities;

namespace SN.ZoneServer.Repositories
{
    public class PlayerRepository
    {

        public ConcurrentDictionary<string, Player> Players = new ConcurrentDictionary<string, Player>();

        public PlayerRepository()
        {

        }

        public void Add(Player p)
        {
            Players.TryAdd(p.Client.PersistentId, p);
            Console.Title = $"[ZoneServer] Sinalia - {Players.Count} players online";
        }

        public void Delete(string persistentId)
        {
            Players.TryRemove(persistentId, out _);
            Console.Title = $"[ZoneServer] Sinalia - {Players.Count} players online";
        }

        public Player Get(string persistentId)
        {
            Players.TryGetValue(persistentId, out Player value);
            return value;
        }

        public List<NetConnection> GetPlayersConnection(List<Player> players)
        {
            List<NetConnection> connections = new List<NetConnection>();

            foreach (var p in players)
            {
                connections.Add(p.Client.Connection);
            }

            return connections;
        }

    }
}
