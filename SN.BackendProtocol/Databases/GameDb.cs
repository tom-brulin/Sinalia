namespace SN.BackendProtocol.Databases
{
    public class GameDb : PostgresDb
    {

        public GameDb() : base(BackendConstants.DbHost, "game", BackendConstants.DbUsername, BackendConstants.DbPassword)
        {

        }

    }
}
