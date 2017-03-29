using System.Data;

namespace simple_dbms.Data.ServerInstance
{
    public interface IServerInstance
    {
        DataTable Select(string query);
    }
}