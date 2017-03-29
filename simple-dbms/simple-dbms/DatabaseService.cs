using System.Collections.Generic;
using System.Data;

namespace simple_dbms
{
    public class DatabaseService
    {
        public IDatabase db { get; set; }

        public DatabaseService(IDatabase db)
        {
            this.db = db;
        }

        public IEnumerable<string> GetDatabases()
        {
            int columnIndex = 0;
            List<string> result = new List<string>();
            DataTable table = db.ExecuteQuery("SELECT * FROM sys.databases");
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