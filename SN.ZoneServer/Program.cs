using System;
using Microsoft.Extensions.DependencyInjection;
using SN.BackendProtocol.Peers;
using SN.Global.Logging;
using SN.GlobalAbstractions.Logging;
using SN.ProtocolAbstractions.Messages.Factories;
using SN.ZoneServer.Server;

namespace SN.ZoneServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "[ZoneServer] Sinalia";
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

            services.AddSingleton<ILoggingService, LoggingService>();
            #endregion

            #region Factories
            services.AddSingleton<ZoneMessageFactory>();
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
