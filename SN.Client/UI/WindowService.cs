using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nez;
using Nez.UI;
using SN.Client.UI.Windows;

namespace SN.Client.UI
{
    public enum WindowAnchor
    {
        Center,
        Left,
        Right,
        Top,
        Bottom
    }

    public class WindowService
    {

        private readonly List<IUiWindow> openWindows = new List<IUiWindow>();

        public WindowService()
        {

        }

        public void Open(IUiWindow window, params WindowAnchor[] anchors)
        {
            openWindows.AddIfNotPresent(window);

            if (!((Element)window).HasParent())
            {
                var container = new Table();
                container.SetFillParent(true);

                foreach (var anchor in anchors)
                {
                    switch(anchor)
                    {
                        case WindowAnchor.Center: container.Center(); break;
                        case WindowAnchor.Left: container.Left(); break;
                        case WindowAnchor.Right: container.Right(); break;
                        case WindowAnchor.Top: container.Top(); break;
                        case WindowAnchor.Bottom: container.Bottom(); break;
                    }
                }

                container.Add((Element)window);
                Core.Scene.FindEntity("ui").GetComponent<UICanvas>().Stage.AddElement(container);
            }
            else
            {
                window.ToggleVisibility();
            }
        }

        public void Open(IUiWindow window, int x, int y)
        {
            window.SetPosition(x, y);
            Open(window);
        }

        public void Open(IUiWindow window, Vector2 position)
        {
            Open(window, (int)position.X, (int)position.Y);
        }

        public void Close(IUiWindow window)
        {
            openWindows.Remove(window);
            window.Close();
        }

        public void CloseAll()
        {
            foreach (var window in openWindows)
            {
                Close(window);
            }
        }

        public void CloseTopWindow()
        {
            if (openWindows.Count > 0)
            {
                var window = openWindows?.LastItem();
                Close(window);
            }
        }

    }
}
