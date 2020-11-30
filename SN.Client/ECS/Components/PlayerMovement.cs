using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using SN.ClientProtocol.Peers;
using SN.Messages.Zone.Players;
using SN.ProtocolAbstractions.Services;

namespace SN.Client.ECS.Components
{
    public class PlayerMovement : Component, IUpdatable
    {

        private Mover mover;
        private float moveSpeed = 300f;
        private VirtualIntegerAxis xAxisInput;
        private VirtualIntegerAxis yAxisInput;
        private Vector2 previousDirection;

        public override void OnAddedToEntity()
        {
            SetupInput();
            mover = Entity.GetComponent<Mover>();
            previousDirection = Vector2.Zero;
        }

        public override void OnRemovedFromEntity()
        {
            base.OnRemovedFromEntity();
        }

        private void SetupInput()
        {
            // Horizontal input from dpad, left stick or keyboard q/d
            xAxisInput = new VirtualIntegerAxis();
            xAxisInput.Nodes.Add(new VirtualAxis.GamePadDpadLeftRight());
            xAxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickX());
            xAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Q, Keys.D));

            // Vertical input from dpad, left stick or keyboard z/s
            yAxisInput = new VirtualIntegerAxis();
            yAxisInput.Nodes.Add(new VirtualAxis.GamePadDpadUpDown());
            yAxisInput.Nodes.Add(new VirtualAxis.GamePadLeftStickY());
            yAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Z, Keys.S));
        }

        void IUpdatable.Update()
        {
            /*
            var moveDirection = new Vector2(xAxisInput.Value, yAxisInput.Value);
            
            if (moveDirection != Vector2.Zero)
            {
                moveDirection.Normalize();
                var movement = moveDirection * moveSpeed * Time.DeltaTime;

                mover.Move(movement, out _);
            }*/
            
        }

        public void SendDirection(IOutgoingMessageService<ZoneClientNetPeer> outgoingMessageService)
        {
            var direction = new Vector2(xAxisInput.Value, yAxisInput.Value);
           
            if (direction.X != 0 && direction.Y != 0)
                direction.Normalize();

            if (!previousDirection.Equals(direction))
            {
                var playerDirectionMessageData = new PlayerDirectionMessageData();
                playerDirectionMessageData.X = direction.X;
                playerDirectionMessageData.Y = direction.Y;
                outgoingMessageService.Send(playerDirectionMessageData);
            }

            previousDirection = direction;
        }

    }
}
