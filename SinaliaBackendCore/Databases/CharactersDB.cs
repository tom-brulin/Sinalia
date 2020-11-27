namespace SinaliaBackendCore.Databases
{
    public class CharactersDB : PostgresDB
    {

        public CharactersDB() : base("localhost", "characters", "postgres", "root")
        {
        }

    }
}
