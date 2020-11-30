using Nez;
using SN.Client.UI;

namespace SN.Client.Scenes
{
    public class BaseScene : Scene
    {
        private readonly WindowService windowService;

        public BaseScene(WindowService windowService)
        {
            this.windowService = windowService;

            CreateEntity("ui").AddComponent<UICanvas>();
        }

        public override void Unload()
        {
            base.Unload();

            windowService.CloseAll();
        }

    }
}
