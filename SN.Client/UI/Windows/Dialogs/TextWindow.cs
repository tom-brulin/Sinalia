using Microsoft.Xna.Framework;
using Nez.UI;

namespace SN.Client.UI.Windows.Dialogs
{
    public class TextWindow : SNWindow
    {

        public TextWindow(string title, string text) : base(title)
        {
            Pad(15);
            PadTop(25);
            GetTitleLabel().FillParent = true;
            GetTitleLabel().SetAlignment(Nez.UI.Align.Center);
            GetTitleLabel().SetFontColor(Color.Black);

            var table = new Table();
            table.Center();

            var textLbl = new Label(text);
            textLbl.SetFontColor(Color.Black);

            table.Add(textLbl).SetPadTop(10);

            Add(table).SetMinWidth(150);
        }

    }
}
