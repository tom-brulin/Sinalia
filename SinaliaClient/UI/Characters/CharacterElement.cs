using Microsoft.Xna.Framework;
using Nez.UI;
using System;

namespace SinaliaClient.UI.Characters
{
    public class CharacterElement : Table, IInputListener
    {

        public event Action<CharacterElement> OnClicked;

        public CharacterElement(string characterName, string characterClass, int level, string zone)
        {
            touchable = Touchable.Enabled;

            Label characterLabel = new Label(characterName);
            characterLabel.SetFontColor(new Color(223, 195, 15));

            Label classLabel = new Label($"Level {level} {characterClass}");

            Label zoneLabel = new Label(zone);
            zoneLabel.SetFontColor(new Color(144, 144, 144));

            Add(characterLabel).Left();
            Row();
            Add(classLabel).Left();
            Row();
            Add(zoneLabel).Left();

            Left();
        }

        void IInputListener.OnMouseEnter()
        {
    
        }

        void IInputListener.OnMouseExit()
        {

        }

        void IInputListener.OnMouseMoved(Vector2 mousePos)
        {

        }

        bool IInputListener.OnMousePressed(Vector2 mousePos)
        {
            return true;
        }

        bool IInputListener.OnMouseScrolled(int mouseWheelDelta)
        {
            return false;
        }

        void IInputListener.OnMouseUp(Vector2 mousePos)
        {
            OnClicked?.Invoke(this);
        }
    }
}
