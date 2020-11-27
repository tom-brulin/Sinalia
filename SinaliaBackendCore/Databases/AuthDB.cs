namespace SinaliaBackendCore.Databases
{
    public class AuthDB : PostgresDB
    {

        public AuthDB() : base("localhost", "auth", "postgres", "root")
        {
        }

    }
}
