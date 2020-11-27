using Microsoft.Extensions.DependencyInjection;
using SinaliaAuthServer.MessageHandlers;
using SinaliaAuthServer.Network;
using SinaliaAuthServer.Services;
using SinaliaBackendCore.Databases;
using SinaliaCore.Logging;
using SinaliaCore.Network.Actors;
using SinaliaCore.Network.Factories;
using SinaliaCore.Network.Services;
using System;

namespace SinaliaAuthServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            RegisterServices(services);

            new Application(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            #region General
            services.AddSingleton<AuthServer>();
            services.AddSingleton<AuthServerNetPeer>();
            services.AddSingleton<AuthDB>();
            services.AddSingleton<CharactersDB>();
            services.AddSingleton<ServerLoop>();
            services.AddSingleton<IncomingMessageProcessor>();
            services.AddSingleton<ILoggingService, LoggingService>();
            services.AddSingleton<IOutgoingMessageService<AuthServerNetPeer>, OutgoingMessageService<AuthServerNetPeer>>();
            #endregion

            #region Repositories

            #endregion

            #region Factories
            services.AddSingleton<AuthMessageFactory>();
            #endregion

            #region Services
            services.AddSingleton<ConnectionValidationService>();
            #endregion

            #region Message Handlers
            services.AddTransient<LoginMessageHandler>();
            #endregion
        }

    }

    internal class Application
    {

        public IServiceProvider Services { get; }

        public Application(IServiceCollection services)
        {
            Services = services.BuildServiceProvider();
            Services.GetService<AuthServer>().Start();
        }

    }

}
