using System;
using Microsoft.Extensions.DependencyInjection;
using SN.Client.Scenes;
using SN.Client.UI;
using SN.Global.Logging;
using SN.GlobalAbstractions.Logging;

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
            #endregion

            #region Zone MessageHandlers
            #endregion
        }

    }

    internal class Application
    {

        public IServiceProvider Services { get; }

        public Application(IServiceCollection services)
        {
            Services = services.BuildServiceProvider();
            Services.GetService<GameCore>().Run();
        }

    }
}
