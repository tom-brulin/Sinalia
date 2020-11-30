using System;
using Lidgren.Network;
using Nez;
using SN.Client.Scenes;
using SN.Messages.Client.Authentification;
using SN.ProtocolAbstractions.Messages;
using Microsoft.Extensions.DependencyInjection;
using SN.Messages.Zone.Authentification;
using SN.ProtocolAbstractions.Services;
using SN.ClientProtocol.Peers;

namespace SN.Client.Network.MessageHandlers.Zone.Authentification
{
    public class CharacterSelectedMessageHandler : IMessageHandler
    {
        private readonly IOutgoingMessageService<ZoneClientNetPeer> outgoingMessageService;
        private readonly IServiceProvider serviceProvider;

        public CharacterSelectedMessageHandler(
            IOutgoingMessageService<ZoneClientNetPeer> outgoingMessageService,
            IServiceProvider serviceProvider)
        {
            this.outgoingMessageService = outgoingMessageService;
            this.serviceProvider = serviceProvider;
        }

        public void Handle(NetConnection sender, SNMessageData messageData)
        {
            var characterSelectedMessageData = (CharacterSelectedMessageData)messageData;

            var gameScene = serviceProvider.GetService<GameScene>();
            Core.Scene = gameScene;
            gameScene.LoadCharacter(characterSelectedMessageData.Character);

            var characterLoadedMessageData = new CharacterLoadedMessageData();
            outgoingMessageService.Send(characterLoadedMessageData);
        }

    }
}
