using simple_dbms.Data.Database;
using System.Collections.Generic;
using System.Data;

namespace simple_dbms.Services
{
    public class DatabaseService
    {
        private readonly IDatabase database;

        public DatabaseService(IDatabase database)
        {
            this.database = database;
        }

        public IEnumerable<string> GetDatabases()
        {
            int columnIndex = 0;
            List<string> result = new List<string>();
            DataTable table = database.ExecuteQuery("SELECT * FROM sys.databases");
            for(int i = 0; i < table.Columns.Count; i++)
            {
                if(table.Columns[i].ColumnName == "name")
                {
                    columnIndex = i;
                    break;
                }
            }

            for(int c = 0; c < table.Rows.Count; c++)
            {
                result.Add((string)table.Rows[c].ItemArray[columnIndex]);
            }
            
            return result;

            // look through table, return database names
        }
    }
}