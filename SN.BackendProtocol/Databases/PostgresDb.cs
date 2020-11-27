using System.Data;
using Npgsql;

namespace SN.BackendProtocol.Databases
{
    public class PostgresDb : Database
    {
        /// <summary>
        /// Creates a new instance of the class <see cref="PostgresDB"/>, setting the connection string
        /// </summary>
        /// <param name="host">The address of the DataBase Host</param>
        /// <param name="database">The DataBase name</param>
        /// <param name="user">The userrname</param>
        /// <param name="password">The password</param>
        public PostgresDb(string host, string database = "", string user = "postgres", string password = "")
        {
            sConnection = string.Format("Host={0};Username={1};Password={2};Database={3}", host, user, password, database);
        }

        /// <summary>
        /// Creates the Connection object
        /// </summary>
        /// <returns>
        /// The Connection object
        /// </returns>
        protected override IDbConnection GetConnection()
        {
            return new NpgsqlConnection(sConnection);
        }

        /// <summary>
        /// Creates the Commands Execution object
        /// </summary>
        /// <param name="sSql">The SQL command to be executed</param>
        /// <returns>
        /// The Commmands object
        /// </returns>
        protected override IDbCommand GetCommand(string sSql)
        {
            return new NpgsqlCommand(sSql, (NpgsqlConnection)connection);
        }

        /// <summary>
        /// Creates the Data Adapter object
        /// </summary>
        /// <param name="command">The Commands object</param>
        /// <returns>
        /// The Data Adapter object
        /// </returns>
        protected override IDbDataAdapter GetAdapter(IDbCommand command)
        {
            return new NpgsqlDataAdapter((NpgsqlCommand)command);
        }
    }
}
