using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace simple_dbms
{
    public class Database : IDatabase
    {
        private readonly IServerInstance serverInstance;

        public Database(IServerInstance serverInstance)
        {
            this.serverInstance = serverInstance;
        }

        
        DataTable ExecuteQuery(string query)
        {
            throw new NotImplementedException();
        }

        ICollection<T> ExecuteQuery<T>(string query)
        {
            throw new NotImplementedException();
        }

        ICollection<T> Query<T>(IQueryable<T> query)
        {
            throw new NotImplementedException();
        }
        
    }
}