using System.Collections.Generic;
using Nez;
using Nez.UI;
using SN.Client.UI;
using SN.Client.UI.Windows.Authentification;
using SN.ClientProtocol.Peers;
using SN.Global.Models;
using SN.Messages.Zone.Authentification;
using SN.ProtocolAbstractions.Services;

namespace SN.Client.Scenes
{
    public class CharacterSelectionScene : BaseScene
    {
        private List<Character> characters;

        private readonly WindowService windowService;
        private readonly IOutgoingMessageService<ZoneClientNetPeer> outgoingMessageService;

        public CharacterSelectionScene(
            WindowService windowService,
            IOutgoingMessageService<ZoneClientNetPeer> outgoingMessageService)
            : base(windowService)
        {
            this.windowService = windowService;
            this.outgoingMessageService = outgoingMessageService;
        }

        public override void OnStart()
        {
            base.OnStart();

            windowService.Open(new CharactersListWindow(), WindowAnchor.Right, WindowAnchor.Top);
            RequestCharacters();

            CreateEntity("enter-world-ui").AddComponent<UICanvas>();
            CreateEntity("create-character-btn").AddComponent<UICanvas>();

            LoadEnterWorld();
            LoadCreateCharacterBtn();
        }

        public void LoadCharacters(List<Character> characters)
        {
            ((CharactersListWindow)windowService.Get("CharacterList"))?.LoadCharacters(characters);
            this.characters = characters;
        }

        private void LoadEnterWorld()
        {
            var skin = GameSettings.Skin;

            UICanvas canvas = FindEntity("enter-world-ui").GetComponent<UICanvas>();

            var container = new Table();
            container.SetFillParent(true);
            container.Center().Bottom();
            container.Pad(15);

            var enterWorldBtn = new TextButton("Enter world", skin.Get<TextButtonStyle>("Button"));
            enterWorldBtn.OnClicked += EnterWorldBtn_OnClicked;

            container.Add(enterWorldBtn).SetMinWidth(150);

            canvas.Stage.AddElement(container);
        }

        private void LoadCreateCharacterBtn()
        {
            var skin = GameSettings.Skin;

            UICanvas canvas = FindEntity("create-character-btn").GetComponent<UICanvas>();

            var container = new Table();
            container.SetFillParent(true);
            container.Bottom().Right();
            container.Pad(15);

            var createCharBtn = new TextButton("Create a new character", skin.Get<TextButtonStyle>("Button"));
            createCharBtn.OnClicked += CreateCharacterBtn_OnClicked;

            container.Add(createCharBtn).SetMinWidth(270);

            canvas.Stage.AddElement(container);
        }

        private void RequestCharacters()
        {
            var requestCharactersMessageData = new RequestCharactersMessageData();
            outgoingMessageService.Send(requestCharactersMessageData);
        }

        private void CreateCharacterBtn_OnClicked(Button btn)
        {
            
        }

        private void EnterWorldBtn_OnClicked(Button btn)
        {
            CharactersListWindow characterListWindow = (CharactersListWindow)windowService.Get("CharacterList");

            int index = characterListWindow.CharacterElements.IndexOf(characterListWindow.CharacterElementSelected);
            var character = characters[index];

            var selectCharacterMessageData = new SelectCharacterMessageData();
            selectCharacterMessageData.CharacterId = character.Id;
            outgoingMessageService.Send(selectCharacterMessageData);
        }

    }
}
