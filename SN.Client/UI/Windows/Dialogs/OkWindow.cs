using Microsoft.Xna.Framework;
using Nez.UI;

namespace SN.Client.UI.Windows.Dialogs
{
    public class OkWindow : SNWindow
    {
        private readonly WindowService windowService;

        public OkWindow(WindowService windowService, string title, string text) : base(title)
        {
            Pad(15);
            PadTop(25);
            GetTitleLabel().FillParent = true;
            GetTitleLabel().SetAlignment(Nez.UI.Align.Center);
            GetTitleLabel().SetFontColor(Color.Black);

            this.windowService = windowService;

            var skin = GameSettings.Skin;

            var table = new Table();
            table.Center();

            var textLbl = new Label(text);
            textLbl.SetFontColor(Color.Black);
            var okBtn = new TextButton("Ok", skin.Get<TextButtonStyle>("Button"));
            okBtn.OnClicked += OkBtn_OnClicked;

            table.Add(textLbl).SetPadBottom(10).SetPadTop(10);
            table.Row();
            table.Add(okBtn);

            Add(table);
        }

        private void OkBtn_OnClicked(Button btn)
        {
            windowService.Close(this);
        }

    }
}
