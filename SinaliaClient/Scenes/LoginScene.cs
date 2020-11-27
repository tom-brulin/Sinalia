using Nez;
using SinaliaClient.UI;
using SinaliaCore.Logging;

namespace SinaliaClient.Scenes
{
    public class LoginScene : BaseScene
    {
        private readonly ILoggingService _loggingService;
        private readonly LoginUI _loginUi;

        public LoginScene(
            ILoggingService loggingService,
            LoginUI loginUi)
        {
            _loggingService = loggingService;
            _loginUi = loginUi;
        }

        public override void OnStart()
        {
            base.OnStart();

            Entity loginUi = CreateEntity("login-ui");
            loginUi.AddComponent(_loginUi);
        }



    }
}
