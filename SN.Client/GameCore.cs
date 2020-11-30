using Nez;
using Microsoft.Extensions.DependencyInjection;
using System;
using SN.Client.Scenes;
using Microsoft.Xna.Framework;
using SN.Client.Network;
using SN.ClientProtocol.Peers;

namespace SN.Client
{
    public class GameCore : Core
    {
        private readonly ZoneClientNetPeer zoneClientNetPeer;
        private readonly IncomingMessageProcessor incomingMessageProcessor;
        private readonly IServiceProvider serviceProvider;

        public GameCore(
            ZoneClientNetPeer zoneClientNetPeer,
            IncomingMessageProcessor incomingMessageProcessor,
            IServiceProvider serviceProvider) 
            : base(windowTitle: "Sinalia")
        {
            this.zoneClientNetPeer = zoneClientNetPeer;
            this.incomingMessageProcessor = incomingMessageProcessor;
            this.serviceProvider = serviceProvider;
        }

        protected override void Initialize()
        {
            base.Initialize();

            IsMouseVisible = true;

            Scene = serviceProvider.GetService<LoginScene>();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            incomingMessageProcessor.ProcessesStatusQueue();
            incomingMessageProcessor.ProcessesDataQueue();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();

            zoneClientNetPeer.Disconnect("Disconnection");
        }

    }
}
