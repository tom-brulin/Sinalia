using Nez;
using Nez.UI;

namespace SinaliaClient.UI.Dialogs
{
    public class TextDialog : UICanvas
    {

        public TextDialog(Skin skin, string text)
        {
            var container = new Table();
            container.SetFillParent(true);

            var label = new Label(text);
            label.SetFontScale(1.2f);

            var dialog = new Dialog("", skin, "Window");
            dialog.AddText(label);
            dialog.Center();
            dialog.PadTop(28);
            dialog.PadLeft(50);
            dialog.PadRight(50);

            container.Add(dialog).SetMaxHeight(50).SetMinWidth(100);
            Stage.AddElement(container);
        }

    }
}
