using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace SN.Client.ECS.Entities
{
    public class NetPlayerEntity : Entity
    {

        public NetPlayerEntity() : base("netplayer")
        {
            Scale = new Vector2(0.3f, 0.3f);

            AddComponent(new SpriteRenderer(Core.Content.LoadTexture("Content/Player.png")));
        }

    }
}
