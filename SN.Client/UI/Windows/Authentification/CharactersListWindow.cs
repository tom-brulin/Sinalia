using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nez;
using Nez.UI;
using SN.Global.Models;

namespace SN.Client.UI.Windows.Authentification
{
    public class CharactersListWindow : SNWindow
    {

        private Table table;

        public readonly List<CharacterElement> CharacterElements = new List<CharacterElement>();
        public CharacterElement CharacterElementSelected;

        private readonly NinePatchDrawable characterSelectedNinePatch = new NinePatchDrawable(GameSettings.Skin.GetSprite("CharacterSelected"), 10, 10, 10, 10);
        private readonly NinePatchDrawable characterNotSelectedNinePatch = new NinePatchDrawable(GameSettings.Skin.GetSprite("CharacterNotSelected"), 10, 10, 10, 10);

        public CharactersListWindow() : base("CharacterList")
        {
            GetTitleTable().Remove();
            SetMovable(false);

            var skin = GameSettings.Skin;

            table = new Table();
            table.Top();

            var deleteCharBtn = new TextButton("Delete Character", skin.Get<TextButtonStyle>("Button"));
            deleteCharBtn.OnClicked += DeleteCharacter_OnClicked;

            Add(table).SetMinWidth(250).SetMinHeight(Core.GraphicsDevice.Viewport.Height - 125);
            Row();
            Add(deleteCharBtn).SetFillX().SetExpandX();
        }

        public void LoadCharacters(List<Character> characters)
        {
            CharacterElements.Clear();
            table.Clear();
            var skin = GameSettings.Skin;

            foreach (var character in characters)
            {
                var characterElement = new CharacterElement(characterNotSelectedNinePatch, character.Name);
                characterElement.OnClicked += CharacterElement_OnClicked;
                table.Add(characterElement).SetFillX().SetExpandX().SetPadBottom(5);
                table.Row();

                CharacterElements.Add(characterElement);
            }

            if (CharacterElements.Count > 0)
                CharacterElement_OnClicked(CharacterElements[0]);
        }

        private void CharacterElement_OnClicked(CharacterElement element)
        {
            if (CharacterElementSelected != null)
            {
                CharacterElementSelected.SetBackground(characterNotSelectedNinePatch);
            }

            CharacterElementSelected = element;
            element.SetBackground(characterSelectedNinePatch);
        }

        private void DeleteCharacter_OnClicked(Button button)
        {

        }

    }

    public class CharacterElement : Table, IInputListener
    {

        public event Action<CharacterElement> OnClicked;

        public CharacterElement(NinePatchDrawable background, string name, string characterClass = "Warrior", string location = "Ardenweald", int level = 1)
        {
            touchable = Touchable.Enabled;

            SetBackground(background);
            Left();

            var nameLbl = new Label(name);
            nameLbl.SetFontColor(Color.Black);

            var characterClassLbl = new Label($"Level {level} {characterClass}");
            characterClassLbl.SetFontColor(Color.Black);

            var locationLbl = new Label(location);
            locationLbl.SetFontColor(Color.Black);

            Add(nameLbl).SetAlign(Nez.UI.Align.Left);
            Row();
            Add(characterClassLbl).SetAlign(Nez.UI.Align.Left);
            Row();
            Add(locationLbl).SetAlign(Nez.UI.Align.Left);
        }

        #region Input Listeners
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
        #endregion

    }
}
