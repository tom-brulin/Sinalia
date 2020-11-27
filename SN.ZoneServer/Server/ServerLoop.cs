using System;
using System.Threading;
using SN.BackendProtocol;
using SN.GlobalAbstractions.Logging;

namespace SN.ZoneServer.Server
{
    public class ServerLoop
    {
        private readonly IncomingMessageProcessor incomingMessageProcessor;
        private readonly ILoggingService loggingService;

        private bool running;

        public ServerLoop(
            IncomingMessageProcessor incomingMessageProcessor,
            ILoggingService loggingService)
        {
            this.incomingMessageProcessor = incomingMessageProcessor;
            this.loggingService = loggingService;
        }

        public void Start()
        {
            if (running)
            {
                loggingService.Log("Atempting to start server loop but loop already started", LogMessageType.WARNING);
                return;
            }


            loggingService.Log("[Loop Started]", LogMessageType.INFO);

            running = true;

            // float deltaTime;
            long startTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            int timeToSleep;

            while (running)
            {
                // deltaTime = (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - startTime) / 1000f;
                startTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                Loop();

                timeToSleep = (int)(BackendConstants.Tick30 * 1000 - (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - startTime));
                if (timeToSleep >= 0)
                {
                    Thread.Sleep(timeToSleep);
                }
                else
                {
                    loggingService.Log("Server is running " + Math.Abs(timeToSleep) + "ms behind", LogMessageType.WARNING);
                }
            }
        }

        public void Stop()
        {
            running = false;
            loggingService.Log("[Loop Stopped]", LogMessageType.INFO);
        }

        private void Loop()
        {
            incomingMessageProcessor.ProcessesStatusQueue();
            incomingMessageProcessor.ProcessesDataQueue();
        }

    }
}
