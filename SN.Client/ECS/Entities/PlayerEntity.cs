using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using SN.Client.ECS.Components;

namespace SN.Client.ECS.Entities
{
    public class PlayerEntity : Entity
    {

        public PlayerEntity() : base("player")
        {
            Scale = new Vector2(0.3f, 0.3f);

            AddComponent(new SpriteRenderer(Core.Content.LoadTexture("Content/Player.png")));
            AddComponent(new FollowCamera(this));
            AddComponent(new Mover());
            AddComponent(new PlayerMovement());
        }

    }
}
