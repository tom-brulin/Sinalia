using Microsoft.Xna.Framework;
using Nez;
using Nez.UI;
using SinaliaClient.Services;
using SinaliaCore.Network.Actors;
using SinaliaCore.Network.Messages.Auth;
using SinaliaCore.Network.Services;

namespace SinaliaClient.UI
{
    public class LoginUI : UICanvas
    {

        private readonly ClientAuthNetPeer _clientAuthNetPeer;
        private readonly DialogService _dialogService;
        private readonly IOutgoingMessageService<ClientAuthNetPeer> _outgoingMessageService;

        private Label _emailLabel;
        private TextField _emailTf;
        private Label _passwordLabel;
        private TextField _passwordTf;
        private TextButton _loginButton;

        public LoginUI(
            ClientAuthNetPeer clientAuthNetPeer,
            DialogService dialogService,
            IOutgoingMessageService<ClientAuthNetPeer> outgoingMessageService,
            UISkin uiSkin)
        {
            _clientAuthNetPeer = clientAuthNetPeer;
            _dialogService = dialogService;
            _outgoingMessageService = outgoingMessageService;

            var container = new Table();
            container.SetFillParent(true);
            container.SetBackground(new SpriteDrawable(Core.Content.LoadTexture("Content/login_bg.jpg")));

            var table = new Table();
            table.Pad(10);

            _emailLabel = new Label("Email adress");
            _emailLabel.SetFontColor(new Color(223, 195, 15));
            _emailLabel.SetFontScale(1.1f);

            _emailTf = new TextField("Cypgain", uiSkin.Skin.Get<TextFieldStyle>("Textbox"));

            _passwordLabel = new Label("Password");
            _passwordLabel.SetFontColor(new Color(223, 195, 15));
            _passwordLabel.SetFontScale(1.1f);

            _passwordTf = new TextField("test", uiSkin.Skin.Get<TextFieldStyle>("Textbox"));
            _passwordTf.SetPasswordMode(true);

            _loginButton = new TextButton("Login", uiSkin.Skin.Get<TextButtonStyle>("Button"));
            _loginButton.OnClicked += LoginButtonClicked;

            table.Add(_emailLabel).SetPadTop(50).SetPadBottom(5);
            table.Row();
            table.Add(_emailTf).SetMinWidth(200).SetPadBottom(20);
            table.Row();
            table.Add(_passwordLabel).SetPadBottom(5);
            table.Row();
            table.Add(_passwordTf).SetFillX().SetPadBottom(20);
            table.Row();
            table.Add(_loginButton).SetMinHeight(30).SetFillX();

            container.Add(table);
            Stage.AddElement(container);
        }

        private void LoginButtonClicked(Button obj)
        {
            if (string.IsNullOrEmpty(_emailTf.GetText()) || string.IsNullOrEmpty(_passwordTf.GetText()))
            {
                _dialogService.ShowOkDialog("Please, fill email and password !");
                return;
            }

            if (_clientAuthNetPeer.ConnectionsCount <= 0)
            {
                _dialogService.ShowOkDialog("Can't connect to login server !");
                return;
            }

            LoginMessageData loginMessageData = new LoginMessageData();
            loginMessageData.Email = _emailTf.GetText();
            loginMessageData.Password = _passwordTf.GetText();

            _outgoingMessageService.Send(loginMessageData);

            _dialogService.ShowTextDialog("Login in...");
        }

    }
}
