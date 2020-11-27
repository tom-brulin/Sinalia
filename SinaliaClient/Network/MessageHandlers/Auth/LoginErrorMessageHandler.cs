using Lidgren.Network;
using SinaliaClient.Services;
using SinaliaCore.Network.Messages;
using SinaliaCore.Network.Messages.Client.Auth;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace SinaliaClient.Network.MessageHandlers.Auth
{
    public class LoginErrorMessageHandler : IAuthMessageHandler
    {

        private readonly IServiceProvider _serviceProvider;

        public LoginErrorMessageHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Handle(NetConnection connection, SNMessageData messageData)
        {
            var loginErrorMessageData = (LoginErrorMessageData) messageData;
            var dialogService = _serviceProvider.GetService<DialogService>();

            switch (loginErrorMessageData.Error)
            {
                case AuthError.NoAccount:
                    dialogService.ShowOkDialog("Accout not found !");
                    break;

                case AuthError.BadPassword:
                    dialogService.ShowOkDialog("Bad password !");
                    break;

                default:
                    dialogService.ShowOkDialog("An error happened");
                    break;
            }
        }

    }
}
