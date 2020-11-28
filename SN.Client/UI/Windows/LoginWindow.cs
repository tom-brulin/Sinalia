using Nez.UI;

namespace SN.Client.UI.Windows
{
    public class LoginWindow : SNWindow
    {

        public LoginWindow()
        {
            SetWidth(200);
            SetMovable(false);

            var skin = GameSettings.Skin;

            var table = new Table();
            table.Center();

            var emailLbl = new Label("Email adress");
            var emailTf = new TextField("", skin.Get<TextFieldStyle>("Textbox"));

            var passwordLbl = new Label("Password");
            var passwordTf = new TextField("", skin.Get<TextFieldStyle>("Textbox"));
            passwordTf.SetPasswordMode(true);

            var loginBtn = new TextButton("Login", skin.Get<TextButtonStyle>("Button"));

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

    }
}
