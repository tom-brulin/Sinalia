using Nez;
using Nez.UI;
using System;

namespace SinaliaClient.UI.Dialogs
{
    public class OkDialog : UICanvas
    {

        public OkDialog(Skin skin, string text, Action<Button> action)
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
            var okButton = dialog.AddButton("Ok", skin.Get<TextButtonStyle>("Button"));

            if (action != null)
                okButton.OnClicked += action;

            container.Add(dialog).SetMaxHeight(70).SetMinWidth(100);
            Stage.AddElement(container);
        }

        public OkDialog(Skin skin, string text) : this(skin, text, null)
        {

        }

    }
}
