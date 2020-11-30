using System;
using Microsoft.Extensions.DependencyInjection;
using SN.BackendProtocol.Authentification;
using SN.BackendProtocol.Databases;
using SN.BackendProtocol.Peers;
using SN.BackendProtocol.Repositories;
using SN.BackendProtocol.Services;
using SN.Global.Logging;
using SN.GlobalAbstractions.Logging;
using SN.ProtocolAbstractions.Messages.Factories;
using SN.ProtocolAbstractions.Services;
using SN.ZoneServer.Entities;
using SN.ZoneServer.MessageHandlers.Client.Authentification;
using SN.ZoneServer.MessageHandlers.Client.Players;
using SN.ZoneServer.Repositories;
using SN.ZoneServer.Server;

namespace SN.ZoneServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "[ZoneServer] Sinalia - 0 players online";
            IServiceCollection services = new ServiceCollection();
            RegisterServices(services);
            new Application(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            #region Server
            services.AddSingleton<ZoneServerNetPeer>();
            services.AddSingleton<Server.ZoneServer>();
            services.AddSingleton<ServerLoop>();
            services.AddSingleton<IncomingMessageProcessor>();
            services.AddSingleton<EntitiesProcessor>();
            services.AddSingleton(typeof(IOutgoingMessageService<>), typeof(OutgoingMessageService<>));

            services.AddSingleton<ILoggingService, LoggingService>();
            #endregion

            #region Databases
            services.AddSingleton<LogDb>();
            services.AddSingleton<GameDb>();
            #endregion

            #region Repositories
            services.AddSingleton<TokenRepository>();
            services.AddSingleton<ClientRepository>();
            services.AddSingleton<PlayerRepository>();
            #endregion

            #region Authentification
            services.AddSingleton<TokenManager>();
            services.AddSingleton<ConnectionValidationService>();
            #endregion

            #region Factories
            services.AddSingleton<ZoneMessageFactory>();
            #endregion

            #region MessageHandlers
            services.AddTransient<PlayerLoginMessageHandler>();

            services.AddTransient<RequestCharactersMessageHandler>();
            services.AddTransient<SelectCharacterMessageHandler>();
            services.AddTransient<PlayerDisconnectMessageHandler>();
            services.AddTransient<CharacterLoadedMessageHandler>();

            services.AddTransient<PlayerDirectionMessageHandler>();
            #endregion
        }
    }

    internal class Application
    {

        public IServiceProvider Services { get; }

        public Application(IServiceCollection services)
        {
            Services = services.BuildServiceProvider();
            Services.GetService<Server.ZoneServer>().Start();
        }

    }
}
