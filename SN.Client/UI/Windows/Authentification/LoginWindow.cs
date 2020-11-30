using System.Net;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Nez.UI;
using SN.Client.UI.Windows.Dialogs;
using SN.ClientProtocol.Peers;
using SN.Global;
using SN.Messages.Zone.Authentification;

namespace SN.Client.UI.Windows.Authentification
{
    public class LoginWindow : SNWindow
    {
        private readonly WindowService windowService;
        private readonly ZoneClientNetPeer zoneClientNetPeer;

        private TextField emailTf;
        private TextField passwordTf;

        public LoginWindow(
            WindowService windowService,
            ZoneClientNetPeer zoneClientNetPeer)
        {
            this.windowService = windowService;
            this.zoneClientNetPeer = zoneClientNetPeer;

            SetWidth(200);
            SetMovable(false);

            var skin = GameSettings.Skin;

            var table = new Table();
            table.Center();

            var emailLbl = new Label("Email adress");
            emailLbl.SetFontColor(Color.Black);
            emailTf = new TextField("Cypgain", skin.Get<TextFieldStyle>("Textbox"));

            var passwordLbl = new Label("Password");
            passwordLbl.SetFontColor(Color.Black);
            passwordTf = new TextField("test", skin.Get<TextFieldStyle>("Textbox"));
            passwordTf.SetPasswordMode(true);

            var loginBtn = new TextButton("Login", skin.Get<TextButtonStyle>("Button"));
            loginBtn.OnClicked += LoginButton_OnClicked;

            table.Add(emailLbl);
            table.Row();
            table.Add(emailTf).SetPadBottom(10);
            table.Row();
            table.Add(passwordLbl);
            table.Row();
            table.Add(passwordTf).SetPadBottom(10);
            table.Row();
            table.Add(loginBtn).SetFillX().SetExpandX();

            Add(table);
        }

        private void LoginButton_OnClicked(Button btn)
        {
            if (string.IsNullOrEmpty(emailTf.GetText()) || string.IsNullOrEmpty(passwordTf.GetText()))
            {
                windowService.Open(new OkWindow(windowService, "Error", "Fields can't be empty"));
                return;
            }

            windowService.Open(new TextWindow("Information", "Login in..."));

            var msg = zoneClientNetPeer.CreateMessage();
            var messageData = new PlayerLoginMessageData();
            messageData.Email = emailTf.GetText();
            messageData.Password = passwordTf.GetText();
            messageData.Encode(msg);

            var receiver = new IPEndPoint(NetUtility.Resolve(Constants.Host), Constants.ZoneServerPort);
            zoneClientNetPeer.SendUnconnectedMessage(msg, receiver);
        }

    }
}
