using Nez;
using Nez.UI;
using SinaliaCore.Models;
using System;
using System.Collections.Generic;

namespace SinaliaClient.UI.Characters
{
    public class CharactersListUI : UICanvas
    {


        private Table _table;
        private List<CharacterElement> _characters;
        private CharacterElement _characterSelected;


        public CharactersListUI(UISkin uiSkin)
        {
            _characters = new List<CharacterElement>();

            var container = new Table();
            container.SetFillParent(true);
            container.SetBackground(new SpriteDrawable(Core.Content.LoadTexture("Content/login_bg.jpg")));
            container.Right().Top().Pad(15);

            _table = new Table();
            _table.SetBackground(uiSkin.PanelBackground);
            _table.Pad(10);
            _table.Top();

            container.Add(_table).SetMaxWidth(250).SetMinHeight(Core.GraphicsDevice.Viewport.Height - 50);
            container.Row();
            container.Add(new TextButton("Create new character", uiSkin.Skin.Get<TextButtonStyle>("Button")));
            Stage.AddElement(container);
        }

        public void LoadCharacters(List<Character> characters)
        {
            _table.ClearChildren();

            int i = 1;
            foreach (var character in characters)
            {
                var btn = new CharacterElement(character.Name, character.Class.ToString(), 1, "Sylvarden");
                btn.OnClicked += OnCharacterClick;
                _characters.Add(btn);

                if (i != characters.Count)
                {
                    _table.Add(btn).SetFillX().SetPadBottom(5).SetMaxHeight(50).SetMinWidth(200);
                    _table.Row();
                }
                else
                {
                    _table.Add(btn).SetFillX().SetMaxHeight(50);
                }

                i++;
            }

            if (_characters.Count > 0)
            {
                OnCharacterClick(_characters[0]);
            }
        }

        private void OnCharacterClick(CharacterElement element)
        {
            if (_characterSelected != null)
            {
                _characterSelected.SetBackground(null);
            }

            _characterSelected = element;
            _characterSelected.SetBackground(new NinePatchDrawable(Core.Content.LoadTexture("Content/Character_Select.png"), 4, 4, 4, 4));
        }

    }
}
