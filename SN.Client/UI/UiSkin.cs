using Nez;
using Nez.UI;

namespace SN.Client.UI
{
    public class UiSkin : Skin
    {

        public UiSkin()
        {
            CreateDefaultSkin();
            Generate();
        }

        private void Generate()
        {
            AddSprites(Core.Content.LoadSpriteAtlas("Content/UI/ui.atlas"));

            Add("Button", new TextButtonStyle()
            {
                Up = new NinePatchDrawable(GetSprite("Button"), 10, 10, 10, 10),
                Down = new NinePatchDrawable(GetSprite("ButtonDown"), 10, 10, 10, 10),
                Over = new NinePatchDrawable(GetSprite("ButtonOver"), 10, 10, 10, 10),
            });

            Add("Checkbox", new CheckBoxStyle()
            {
                CheckboxOff = GetDrawable("Checkbox"),
                CheckboxOn = GetDrawable("CheckboxChecked")
            });

            Add("Textbox", new TextFieldStyle()
            {
                Background = new NinePatchDrawable(GetSprite("Textbox"), 10, 10, 10, 10),
                Cursor = GetDrawable("Cursor"),
                Selection = GetDrawable("Cursor"),
            });

            Add("Window", new WindowStyle()
            {
                Background = new NinePatchDrawable(GetSprite("Window"), 10, 10, 10, 10)
            });
        }

    }
}
