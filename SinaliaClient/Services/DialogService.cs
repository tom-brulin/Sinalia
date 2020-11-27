using Nez;
using Nez.UI;
using SinaliaClient.Scenes;
using SinaliaClient.UI;
using SinaliaClient.UI.Dialogs;
using System;
using System.Threading.Tasks;

namespace SinaliaClient.Services
{
    public class DialogService
    {

        private readonly UISkin _uiSkin;

        private UICanvas _currentDialog;

        public DialogService(UISkin uiSkin)
        {
            _uiSkin = uiSkin;
        }

        public void ShowTextDialog(string text)
        {
            if (Core.Scene == null)
                return;

            _currentDialog = new TextDialog(_uiSkin.Skin, text);
            ((BaseScene)Core.Scene).Dialog.RemoveAllComponents();
            ((BaseScene)Core.Scene).Dialog.AddComponent(_currentDialog);
        }

        public async void ShowTextDialog(string text, int ms)
        {
            ShowTextDialog(text);
            await Task.Delay(ms);
            Close();
        }

        public void ShowOkDialog(string text, Action<Button> action)
        {
            if (Core.Scene == null)
                return;

            _currentDialog = new OkDialog(_uiSkin.Skin, text, action);
            ((BaseScene)Core.Scene).Dialog.RemoveAllComponents();
            ((BaseScene)Core.Scene).Dialog.AddComponent(_currentDialog);
        }

        public void ShowOkDialog(string text)
        {
            ShowOkDialog(text, (btn) => { Close(); });
        }

        public async void ShowOkDialog(string text, int ms)
        {
            ShowOkDialog(text);
            await Task.Delay(ms);
            Close();
        }

        public async void ShowOkDialog(string text, Action<Button> action, int ms)
        {
            ShowOkDialog(text, action);
            await Task.Delay(ms);
            Close();
        }

        public void Close()
        {
            if (Core.Scene == null)
                return;

            ((BaseScene)Core.Scene).Dialog.RemoveAllComponents();
            _currentDialog = null;
        }

    }
}
