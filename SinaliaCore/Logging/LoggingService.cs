using Pastel;
using System;
using System.Drawing;

namespace SinaliaCore.Logging
{
    public class LoggingService : ILoggingService
    {

        public void Log(string message, LogMessageType type)
        {
            switch(type)
            {
                case LogMessageType.DEBUG: LogDebug(message); break;
                case LogMessageType.SUCCESS: LogSuccess(message); break;
                case LogMessageType.INFO: LogInfo(message); break;
                case LogMessageType.WARNING: LogWarning(message); break;
                case LogMessageType.ERROR: LogError(message); break;
            }
        }

        private void LogDebug(string message)
        {
            Console.WriteLine(message.Pastel(Color.Orchid));
        }

        private void LogSuccess(string message)
        {
            Console.WriteLine(message.Pastel(Color.Black).PastelBg(Color.Lime));
        }

        private void LogInfo(string message)
        {
            Console.WriteLine(message.Pastel(Color.White));
        }

        private void LogWarning(string message)
        {
            Console.WriteLine(message.Pastel(Color.Gold));
        }

        private void LogError(string message)
        {
            Console.WriteLine(message.Pastel(Color.Crimson));
        }


    }
}
