using Microsoft.Xna.Framework;
using SN.BackendProtocol.Actors;
using SN.Global.Models;

namespace SN.ZoneServer.Entities
{
    public class Player
    {

        public PlayerClient Client { get; }
        public Character Character { get; }

        public Vector2 OldPosition;
        public Vector2 Position;
        public Vector2 Velocity;
        public float Speed;

        public Player(PlayerClient client, Character character)
        {
            Client = client;
            Character = character;
            OldPosition = new Vector2(0, 0);
            Position = new Vector2(0, 0);
            Velocity = new Vector2(0, 0);
            Speed = 300f;
        }

    }
}
