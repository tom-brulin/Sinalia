using Nez;

namespace SinaliaClient.Scenes
{
    public class BaseScene : Scene
    {

        public Entity Dialog { get; private set; }

        public BaseScene()
        {

        }

        public override void OnStart()
        {
            base.OnStart();

            Dialog = CreateEntity("dialog");
        }

    }
}
