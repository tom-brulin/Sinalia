using Nez;
using SinaliaClient.UI.Characters;
using SinaliaCore.Models;
using System.Collections.Generic;

namespace SinaliaClient.Scenes
{
    public class CharactersScene : BaseScene
    {
        private readonly CharacterPreviewUI _characterPreviewUI;
        private readonly CharactersListUI _charactersListUI;

        public CharactersScene(
            CharacterPreviewUI characterPreviewUI,
            CharactersListUI charactersListUI)
        {
            _characterPreviewUI = characterPreviewUI;
            _charactersListUI = charactersListUI;
        }

        public override void OnStart()
        {
            base.OnStart();

            Entity characterListUI = CreateEntity("character-list-ui");
            characterListUI.AddComponent(_charactersListUI);

            Entity characterPreviewUI = CreateEntity("character-preview-ui");
            characterPreviewUI.AddComponent(_characterPreviewUI);
        }

        public void LoadCharacters(List<Character> characters)
        {
            _charactersListUI.LoadCharacters(characters);
        }

    }
}
