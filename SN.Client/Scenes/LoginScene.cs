using SN.Client.UI;
using SN.Client.UI.Windows.Authentification;
using SN.ClientProtocol.Peers;

namespace SN.Client.Scenes
{
    public class LoginScene : BaseScene
    {
        private readonly ZoneClientNetPeer zoneClientNetPeer;
        private readonly WindowService windowService;

        public LoginScene(
            ZoneClientNetPeer zoneClientNetPeer,
            WindowService windowService) 
            : base(windowService)
        {
            this.zoneClientNetPeer = zoneClientNetPeer;
            this.windowService = windowService;
        }

        public override void OnStart()
        {
            base.OnStart();

            windowService.Open(new LoginWindow(windowService, zoneClientNetPeer), WindowAnchor.Center);
        }

    }
}
