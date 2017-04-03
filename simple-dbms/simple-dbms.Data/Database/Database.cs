using simple_dbms.Data.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace simple_dbms.Data.Database
{
    public class Database : IDatabase
    {
        private readonly IServerInstance serverInstance;

        public Database(IServerInstance serverInstance)
        {
            this.serverInstance = serverInstance;
        }
                
        public DataTable ExecuteQuery(string query)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> ExecuteQuery<T>(string query)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> Query<T>(IQueryable<T> query)
        {
            throw new NotImplementedException();
        }        
    }
}