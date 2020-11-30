using System.Net;
using SN.Client.UI;
using SN.Client.UI.Windows.Dialogs;
using SN.ProtocolAbstractions.Messages;

namespace SN.Client.Network.MessageHandlers.Zone.Authentification
{
    public class PlayerLoginErrorMessageHandler : IUnconnectedMessageHandler
    {
        private readonly WindowService windowService;

        public PlayerLoginErrorMessageHandler(WindowService windowService)
        {
            this.windowService = windowService;
        }

        public void Handle(IPEndPoint sender, SNMessageData messageData)
        {
            windowService.CloseTopWindow();
            windowService.Open(new OkWindow(windowService, "Error", "Account not found"));
        }

    }
}
