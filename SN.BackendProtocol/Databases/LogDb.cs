namespace SN.BackendProtocol.Databases
{
    public class LogDb : PostgresDb
    {

        public LogDb() : base(BackendConstants.DbHost, "log", BackendConstants.DbUsername, BackendConstants.DbPassword)
        {

        }

    }
}
