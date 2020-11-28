using Nez;
using Microsoft.Extensions.DependencyInjection;
using System;
using SN.Client.Scenes;

namespace SN.Client
{
    public class GameCore : Core
    {
        private readonly IServiceProvider serviceProvider;

        public GameCore(IServiceProvider serviceProvider) : base(windowTitle: "Sinalia")
        {
            this.serviceProvider = serviceProvider;
        }

        protected override void Initialize()
        {
            base.Initialize();

            IsMouseVisible = true;

            Scene = serviceProvider.GetService<LoginScene>();
        }

    }
}
