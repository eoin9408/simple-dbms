using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace simple_dbms.Data.Database
{
    public interface IDatabase
    {
        DataTable ExecuteQuery(string query);
        ICollection<T> ExecuteQuery<T>(string query);
        ICollection<T> Query<T>(IQueryable<T> query);
    }
}