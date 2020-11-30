using System;
using Lidgren.Network;
using Nez;
using SN.ProtocolAbstractions.Messages;
using Microsoft.Extensions.DependencyInjection;
using SN.Client.Scenes;

namespace SN.Client.Network.MessageHandlers.Zone.Authentification
{
    public class ConnectedMessageHandler : IStatusHandler
    {
        private readonly IServiceProvider serviceProvider;

        public ConnectedMessageHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Handle(NetConnection sender, NetIncomingMessage mesasge)
        {
            Nez.Core.StartSceneTransition(new WindTransition(() => serviceProvider.GetService<CharacterSelectionScene>()));
        }

    }
}
