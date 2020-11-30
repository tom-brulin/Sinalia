using Microsoft.Xna.Framework;

namespace SN.Global.Models
{
    public class Character
    {

        public string AccountPersistentId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        public Character(string accountPersistentId, int id, string name, float x, float y)
        {
            AccountPersistentId = accountPersistentId;
            Id = id;
            Name = name;
            X = x;
            Y = y;
        }

    }
}
