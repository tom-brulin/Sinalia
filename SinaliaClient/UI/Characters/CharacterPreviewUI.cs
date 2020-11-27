using Nez;
using Nez.UI;

namespace SinaliaClient.UI.Characters
{
    public class CharacterPreviewUI : UICanvas
    {

        public CharacterPreviewUI(UISkin uiSkin)
        {
            var container = new Table();
            container.SetFillParent(true);
            container.Bottom();
            container.Pad(15);
           
            var table = new Table();
            table.Bottom();

            var enterWorldButton = new TextButton("Enter world", uiSkin.Skin.Get<TextButtonStyle>("Button"));

            table.Add(enterWorldButton);

            container.Add(table);
            Stage.AddElement(container);
        }

    }
}
