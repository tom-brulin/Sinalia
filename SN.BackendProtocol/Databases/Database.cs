using System.Collections.Generic;
using System.Data;

namespace SN.BackendProtocol.Databases
{
    public abstract class Database
    {

        protected string sConnection = string.Empty;
        protected IDbConnection connection = null;
        protected IDbTransaction transaction = null;
        protected abstract IDbConnection GetConnection();
        protected abstract IDbCommand GetCommand(string sSql);
        protected abstract IDbDataAdapter GetAdapter(IDbCommand command);

        public Database()
        {
        }

        public void Open()
        {
            if (connection == null)
            {
                connection = GetConnection();
            }

            connection.Open();
        }

        public void Close()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

        public void CommitTransaction()
        {
            if (transaction != null)
            {
                transaction.Commit();
                transaction.Dispose();
                transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            if (transaction != null)
            {
                transaction.Rollback();
                transaction.Dispose();
                transaction = null;
            }
        }

        /// <summary>
        /// Run the SQL command with the provided parameters
        /// </summary>
        /// <param name="sSql">The SQL command</param>
        /// <param name="parameters">The SQL command parameters</param>
        /// <returns>The value on the first column of the first line returned by the command</returns>
        public string SqlExecute(string sSql, Dictionary<string, object> parameters = null)
        {
            // Get the command object from the heir class
            var command = GetCommand(sSql);

            // Checks if there's any parameters
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    // Creates the parameter relative to the Commands object
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = item.Key;
                    parameter.Value = item.Value;

                    // Add the parameter to the Commands object
                    command.Parameters.Add(parameter);
                }
            }

            // Checks if there's an active transaction
            if (transaction != null)
            {
                // If there is, executes the command inside the transaction
                command.Transaction = transaction;
            }

            // Executes the command and takes the value on the first column of the first line
            var result = (string)command.ExecuteScalar();

            // Free the command resources
            command.Dispose();

            return result;
        }

        /// <summary>
        /// Run the SQL select with the provided parameters
        /// </summary>
        /// <param name="sSql">The SQL select</param>
        /// <param name="parameters">The SQL select parameters</param>
        /// <param name="commandType">The SQL command type</param>
        /// <returns>The Data Set returned by the SQL select</returns>
        public DataSet SqlSelect(string sSql, Dictionary<string, object> parameters = null, CommandType commandType = CommandType.Text)
        {
            // Creates a new Data Set
            var dataSet = new DataSet();

            // Retrieves the Commands object
            var command = GetCommand(sSql);

            // Retrieves the Data Adapter object
            var adapter = GetAdapter(command);

            // Checks if there's any parameters
            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    // Creates the paramater relative to the Commands object
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = item.Key;
                    parameter.Value = item.Value;

                    // Add the parameter to the Commands object
                    command.Parameters.Add(parameter);
                }
            }

            // Set the type of the command to be executed
            command.CommandType = commandType;

            // Checks if there's an active transaction 
            if (transaction != null)
            {
                // If there is, execute the command inside the transaction
                command.Transaction = transaction;
            }

            // Use the Data Adapter to fill the Data Set with the data returned from the SQL select
            adapter.Fill(dataSet);

            // Free the command resources
            command.Dispose();

            return dataSet;
        }

    }
}
