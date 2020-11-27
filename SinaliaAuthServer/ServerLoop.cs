using SinaliaAuthServer.Network;
using SinaliaCore;
using SinaliaCore.Logging;
using System;

namespace SinaliaAuthServer
{
    public class ServerLoop
    {
        private readonly IncomingMessageProcessor _incomingMessageProcessor;
        private readonly ILoggingService _loggingService;

        private bool _running;

        public ServerLoop(
            IncomingMessageProcessor incomingMessageProcessor,
            ILoggingService loggingService)
        {
            _incomingMessageProcessor = incomingMessageProcessor;
            _loggingService = loggingService;
        }

        public void Start()
        {
            if (_running)
            {
                _loggingService.Log("Atempting to start server loop but loop already started", LogMessageType.WARNING);
                return;
            }


            _loggingService.Log("[Loop Started]", LogMessageType.INFO);

            _running = true;

            float deltaTime;
            long startTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            long timeToSleep;

            while (_running)
            {
                deltaTime = (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - startTime) / 1000f;
                startTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                Loop();

                timeToSleep = (long) (SharedConstants.Tick30 * 1000 - (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - startTime));
                if (timeToSleep > 0)
                {

                }
                else
                {
                    _loggingService.Log("AuthServer is running " + Math.Abs(timeToSleep) + "ms behind", LogMessageType.WARNING);
                }
            }
        }

        public void Stop()
        {
            _running = false;
            _loggingService.Log("[Loop Stopped]", LogMessageType.INFO);
        }

        private void Loop()
        {
            _incomingMessageProcessor.ProcessesDataQueue();
            _incomingMessageProcessor.ProcessesStatusQueue();
        }

    }
}
