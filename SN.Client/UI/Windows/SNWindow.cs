using Nez.UI;

namespace SN.Client.UI.Windows
{
    public abstract class SNWindow : Window, IUiWindow
    {

        public SNWindow(string title = "") : base(title, GameSettings.Skin.Get<WindowStyle>("Window"))
        {
            if (title.Equals(""))
                GetTitleTable().Remove();
        }

        public void Close()
        {
            Remove();
        }

        public void SetPosition(int x, int y)
        {
            SetX(x);
            SetY(y);
        }

        public void ToggleVisibility()
        {
            SetVisible(!IsVisible());
        }

    }
}
