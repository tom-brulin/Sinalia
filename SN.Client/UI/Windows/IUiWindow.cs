namespace SN.Client.UI.Windows
{
    public interface IUiWindow
    {

        void ToggleVisibility();

        void Close();

        void SetPosition(int x, int y);

    }
}
