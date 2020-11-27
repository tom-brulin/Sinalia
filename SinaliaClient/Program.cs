using Microsoft.Extensions.DependencyInjection;
using SinaliaClient.Network;
using SinaliaClient.Network.MessageHandlers.Auth;
using SinaliaClient.Scenes;
using SinaliaClient.Services;
using SinaliaClient.UI;
using SinaliaClient.UI.Characters;
using SinaliaCore.Logging;
using SinaliaCore.Network.Actors;
using SinaliaCore.Network.Factories;
using SinaliaCore.Network.Services;
using System;

namespace SinaliaClient
{

    public static class Program
    {

        public static void Main()
        {
            IServiceCollection services = new ServiceCollection();

            RegisterServices(services);

            new Application(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            #region General
            services.AddSingleton<ILoggingService, LoggingService>();
            #endregion

            #region Game
            services.AddSingleton<GameCore>();
            #endregion

            #region Scenes
            services.AddTransient<LoginScene>();
            services.AddSingleton<CharactersScene>();
            #endregion

            #region UI
            services.AddSingleton<UISkin>();
            services.AddSingleton<DialogService>();
            services.AddTransient<LoginUI>();
            services.AddTransient<CharactersListUI>();
            services.AddTransient<CharacterPreviewUI>();
            #endregion

            #region Network
            services.AddSingleton<AuthClient>();
            services.AddSingleton<ClientAuthNetPeer>();
            services.AddSingleton<IncomingMessageProcessor>();
            services.AddSingleton<IOutgoingMessageService<ClientAuthNetPeer>, OutgoingMessageService<ClientAuthNetPeer>>();
            #endregion

            #region Factories
            services.AddSingleton<ClientMessageFactory>();
            #endregion

            #region Message Handlers
            services.AddTransient<LoginErrorMessageHandler>();
            services.AddTransient<LoginSuccessMessageHandler>();
            #endregion
        }

    }

    internal class Application
    {

        public IServiceProvider Services { get; }

        public Application(IServiceCollection services)
        {
            Services = services.BuildServiceProvider();
            Services.GetService<AuthClient>().Connect();
            Services.GetService<GameCore>().Run();
        }

    }

}
