using Nez;
using Microsoft.Extensions.DependencyInjection;
using System;
using SinaliaClient.Scenes;
using SinaliaCore.Logging;
using Microsoft.Xna.Framework;
using SinaliaClient.Network;

namespace SinaliaClient
{

    public class GameCore : Core
    {
        private readonly IncomingMessageProcessor _incomingMessageProcessor;
        private readonly ILoggingService _loggingService;
        private readonly IServiceProvider _serviceProvider;

        public GameCore(
            IncomingMessageProcessor incomingMessageProcessor,
            ILoggingService loggingService,
            IServiceProvider serviceProvider) : base(windowTitle: "Sinalia")
        {
            _incomingMessageProcessor = incomingMessageProcessor;
            _loggingService = loggingService;
            _serviceProvider = serviceProvider;
        }

        protected override void Initialize()
        {
            base.Initialize();

            IsMouseVisible = true;

            Scene = _serviceProvider.GetService<LoginScene>();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _incomingMessageProcessor.ProcessesDataQueue();
        }

    }
}
