using System;
using System.Diagnostics;
using System.Threading;
using SN.BackendProtocol;
using SN.GlobalAbstractions.Logging;
using SN.ZoneServer.Entities;

namespace SN.ZoneServer.Server
{
    public class ServerLoop
    {

        private readonly EntitiesProcessor entititesProcessor;
        private readonly IncomingMessageProcessor incomingMessageProcessor;
        private readonly ILoggingService loggingService;

        private bool running;

        public ServerLoop(
            EntitiesProcessor entititesProcessor,
            IncomingMessageProcessor incomingMessageProcessor,
            ILoggingService loggingService)
        {
            this.entititesProcessor = entititesProcessor;
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

            var gameTimer = Stopwatch.StartNew();

            const int framesTarget = 25;
            const int tickTarget = 1000 / framesTarget;

            long startTime = gameTimer.ElapsedMilliseconds;
            float deltaTime;
            int timeToSleep;

            while (running)
            {
                deltaTime = (gameTimer.ElapsedMilliseconds - startTime) / 1000f;
                startTime = gameTimer.ElapsedMilliseconds;

                Loop(deltaTime);

                timeToSleep = (int)(tickTarget - (gameTimer.ElapsedMilliseconds - startTime));

                if (timeToSleep >= 0)
                    Thread.Sleep(timeToSleep);
                else
                    loggingService.Log("Server is running " + Math.Abs(timeToSleep) + "ms behind", LogMessageType.WARNING);
            }
        }

        public void Stop()
        {
            running = false;
            loggingService.Log("[Loop Stopped]", LogMessageType.INFO);
        }

        private void Loop(float deltaTime)
        {
            incomingMessageProcessor.ProcessesStatusQueue();
            incomingMessageProcessor.ProcessesDataQueue();

            entititesProcessor.ProcessEntities(deltaTime);
        }

    }
}
