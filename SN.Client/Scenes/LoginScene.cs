using SN.Client.UI;
using SN.Client.UI.Windows;

namespace SN.Client.Scenes
{
    public class LoginScene : BaseScene
    {
        private readonly WindowService windowService;

        public LoginScene(WindowService windowService)
        {
            this.windowService = windowService;
        }

        public override void OnStart()
        {
            base.OnStart();

            windowService.Open(new LoginWindow(), WindowAnchor.Center);
        }

    }
}
