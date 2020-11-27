namespace SN.GlobalAbstractions.Logging
{
    public interface ILoggingService
    {

        void Log(string message, LogMessageType type);

    }
}
