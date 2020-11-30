using System;
using Microsoft.Extensions.DependencyInjection;
using SN.Client.Network;
using SN.Client.Network.MessageHandlers.Zone.Authentification;
using SN.Client.Network.MessageHandlers.Zone.Entities;
using SN.Client.Network.Zone;
using SN.Client.Scenes;
using SN.Client.UI;
using SN.ClientProtocol.Peers;
using SN.ClientProtocol.Services;
using SN.Global.Logging;
using SN.GlobalAbstractions.Logging;
using SN.ProtocolAbstractions.Messages.Factories;
using SN.ProtocolAbstractions.Services;

namespace SN.Client
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            IServiceCollection services = new ServiceCollection();
            RegisterServices(services);
            new Application(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            #region Client
            services.AddSingleton<GameCore>();
            services.AddSingleton<WindowService>();

            services.AddSingleton<ILoggingService, LoggingService>();
            #endregion

            #region Scenes
            services.AddTransient<LoginScene>();
            services.AddTransient<CharacterSelectionScene>();
            services.AddTransient<GameScene>();
            #endregion

            #region Network General
            services.AddSingleton<IncomingMessageProcessor>();
            services.AddSingleton(typeof(IOutgoingMessageService<>), typeof(OutgoingMessageService<>));
            #endregion

            #region Network Factories
            services.AddSingleton<ClientMessageFactory>();
            #endregion

            #region Zone Network
            services.AddSingleton<ZoneClientNetPeer>();
            services.AddSingleton<ZoneClient>();
            #endregion

            #region Zone MessageHandlers
            services.AddTransient<ConnectedMessageHandler>();

            services.AddTransient<PlayerLoginErrorMessageHandler>();
            services.AddTransient<PlayerLoginSuccessMessageHandler>();
            services.AddTransient<SendCharactersMessageHandler>();
            services.AddTransient<CharacterSelectedMessageHandler>();

            services.AddTransient<EntityPositionMessageHandler>();
            services.AddTransient<CharacterDisconnectedMessageHandler>();
            #endregion
        }

    }

    internal class Application
    {

        public IServiceProvider Services { get; }

        public Application(IServiceCollection services)
        {
            Services = services.BuildServiceProvider();
            Services.GetService<ZoneClient>().Start();
            Services.GetService<GameCore>().Run();
        }

    }
}
