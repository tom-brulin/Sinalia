using Lidgren.Network;
using SinaliaCore.Network.Messages;
using SinaliaCore.Network.Messages.Client.Auth;
using System;
using Microsoft.Extensions.DependencyInjection;
using SinaliaClient.Scenes;
using Nez;

namespace SinaliaClient.Network.MessageHandlers.Auth
{
    public class LoginSuccessMessageHandler : IAuthMessageHandler
    {
        private readonly AuthClient _authClient;
        private readonly IServiceProvider _serviceProvider;

        public LoginSuccessMessageHandler(
            AuthClient authClient,
            IServiceProvider serviceProvider)
        {
            _authClient = authClient;
            _serviceProvider = serviceProvider;
        }

        public void Handle(NetConnection connection, SNMessageData messageData)
        {
            var loginSuccessMessageData = (LoginSuccessMessageData) messageData;

            _authClient.AuthToken = loginSuccessMessageData.Token;
            _authClient.CliendId = loginSuccessMessageData.ClientId;

            var characterScene = _serviceProvider.GetService<CharactersScene>();
            Core.StartSceneTransition(new WindTransition(() => characterScene));
            characterScene.LoadCharacters(loginSuccessMessageData.Characters);
        }

    }
}
