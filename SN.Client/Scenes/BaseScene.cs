using Nez;

namespace SN.Client.Scenes
{
    public class BaseScene : Scene
    {

        public BaseScene()
        {
            CreateEntity("ui").AddComponent<UICanvas>();
        }

    }
}
