using Microsoft.Xna.Framework;
using Nez;
using SN.Client.ECS.Components;
using SN.Client.ECS.Entities;
using SN.Client.UI;
using SN.ClientProtocol.Peers;
using SN.Global.Models;
using SN.ProtocolAbstractions.Services;

namespace SN.Client.Scenes
{
    public class GameScene : BaseScene
    {
        private readonly IOutgoingMessageService<ZoneClientNetPeer> outgoingMessageService;
        private PlayerEntity playerEntity;

        public GameScene(
            IOutgoingMessageService<ZoneClientNetPeer> outgoingMessageService,
            WindowService windowService) : base(windowService)
        {
            this.outgoingMessageService = outgoingMessageService;
        }

        public override void OnStart()
        {
            base.OnStart();
        }

        public void LoadCharacter(Character character)
        {
            var tiledEntity = CreateEntity("tiled-map-entity");
            var map = Core.Content.LoadTiledMap("Content/Map/Map.tmx");
            tiledEntity.AddComponent(new TiledMapRenderer(map));

            playerEntity = AddEntity(new PlayerEntity());
            playerEntity.Name = character.AccountPersistentId;
        }

        public override void Update()
        {
            base.Update();

            playerEntity.GetComponent<PlayerMovement>().SendDirection(outgoingMessageService);
        }

    }
}
