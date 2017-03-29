using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_dbms.Data.ServerInstance
{
    public class ServerInstance : IServerInstance
    {
        private readonly string connectionString;
        private readonly DbProviderFactory providerFactory;

        private DbConnection connection;

        public ServerInstance(DbProviderFactory providerFactory, string connectionString)
        {
            this.providerFactory = providerFactory;
            this.connectionString = connectionString;
        }

        private DbConnection GetConnection()
        {
            if (connection == null || connection.State == ConnectionState.Broken)
            {
                connection = providerFactory.CreateConnection();
                connection.ConnectionString = connectionString;
            }

            return connection;
        }
        
        public DataTable Select(string query)
        {
            var connection = GetConnection();
            connection.Open();

            var results = new DataTable();

            using (var command = providerFactory.CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                command.Connection = connection;
                
                using (var adapter = providerFactory.CreateDataAdapter())
                {
                    adapter.SelectCommand = command;
                    adapter.Fill(results);
                }
            }

            connection.Close();

            return results;
        }
    }
}
