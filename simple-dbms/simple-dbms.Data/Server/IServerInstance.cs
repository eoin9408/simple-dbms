using System.Data;

namespace simple_dbms.Data.Server
{
    public interface IServerInstance
    {
        DataTable Select(string query);
    }
}