using Lidgren.Network;
using Microsoft.Xna.Framework;
using Nez;
using SN.Client.ECS.Entities;
using SN.Client.Scenes;
using SN.Messages.Client.Entities;
using SN.ProtocolAbstractions.Messages;

namespace SN.Client.Network.MessageHandlers.Zone.Entities
{
    public class EntityPositionMessageHandler : IMessageHandler
    {

        public EntityPositionMessageHandler()
        {

        }

        public void Handle(NetConnection sender, SNMessageData messageData)
        {
            var entityPositionMessageData = (EntityPositionMessageData)messageData;

            if (!(Core.Scene is GameScene))
                return;

            var gameScene = (GameScene)Core.Scene;
            var entity = gameScene.FindEntity(entityPositionMessageData.Uid);

            if (entity == null)
            {
                entity = new NetPlayerEntity();
                entity.Name = entityPositionMessageData.Uid;
                gameScene.AddEntity(entity);
            }
            
            entity.SetPosition(new Vector2(entityPositionMessageData.X, entityPositionMessageData.Y));
        }

    }
}
