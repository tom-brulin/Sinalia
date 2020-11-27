using Nez;
using Nez.BitmapFonts;
using Nez.Sprites;
using Nez.UI;

namespace SinaliaClient.UI
{
    public class UISkin
    {

        public Skin Skin { get; }
        public NinePatchDrawable PanelBackground { get; private set; }

        public UISkin()
        {
            Skin = Skin.CreateDefaultSkin();
            GenerateSkin();
        }

        private void GenerateSkin()
        {
            Skin.AddSprites(Core.Content.LoadSpriteAtlas("Content/UI/ui.atlas"));

            PanelBackground = new NinePatchDrawable(Skin.GetSprite("Panel"), 10, 10, 10, 10);

            Skin.Add("Button", new TextButtonStyle()
            {
                Up = new NinePatchDrawable(Skin.GetSprite("Button"), 6, 6, 6, 6),
                Down = new NinePatchDrawable(Skin.GetSprite("Button"), 6, 6, 6, 6),
                Over = new NinePatchDrawable(Skin.GetSprite("Button"), 6, 6, 6, 6),
            });

            Skin.Add("Gold_Button", new TextButtonStyle()
            {
                Up = new NinePatchDrawable(Skin.GetSprite("Gold_Button_Normal"), 6, 6, 6, 6),
                Down = new NinePatchDrawable(Skin.GetSprite("Gold_Button_Clicked"), 6, 6, 6, 6),
                Over = new NinePatchDrawable(Skin.GetSprite("Gold_Button_Hover"), 6, 6, 6, 6),
            });

            Skin.Add("Silver_Button", new TextButtonStyle()
            {
                Up = new NinePatchDrawable(Skin.GetSprite("Silver_Button_Normal"), 6, 6, 6, 6),
                Down = new NinePatchDrawable(Skin.GetSprite("Silver_Button_Clicked"), 6, 6, 6, 6),
                Over = new NinePatchDrawable(Skin.GetSprite("Silver_Button_Hover"), 6, 6, 6, 6),
            });

            Skin.Add("Gold_Checkbox", new CheckBoxStyle()
            {
                CheckboxOff = Skin.GetDrawable("Gold_Checkbox_Unchecked"),
                CheckboxOn = Skin.GetDrawable("Gold_Checkbox_Checked")
            });

            Skin.Add("Silver_Checkbox", new CheckBoxStyle()
            {
                CheckboxOff = Skin.GetDrawable("Silver_Checkbox_Unchecked"),
                CheckboxOn = Skin.GetDrawable("Silver_Checkbox_Checked")
            });

            Skin.Add("Textbox", new TextFieldStyle()
            {
                Background = new NinePatchDrawable(Skin.GetSprite("Textbox"), 10, 10, 10, 10),
                Cursor = Skin.GetDrawable("Cursor"),
                Selection = Skin.GetDrawable("Cursor"),
            });

            Skin.Add("Window", new WindowStyle()
            {
                Background = PanelBackground
            });
        }

    }
}
