using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteManager
{
    class Database
    {
        public Dictionary<string, Table> Tables { get; set; }
        private SqliteConnection connection;

        public Database(string filepath)
        {
            connection = new SqliteConnection($"Data Source={filepath}");
            connection.Open();
        }

        public void Refresh()
        {
            Tables = new Dictionary<string, Table>();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT name FROM sqlite_master WHERE type='table'";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string tableName = reader.GetString(0);
                    var columns = new List<Column>();
                    var columnsCommand = connection.CreateCommand();
                    columnsCommand.CommandText = $"PRAGMA table_info({tableName})";

                    if (tableName == "sqlite_sequence")
                    {
                        continue;
                    }

                    using (var columnsReader = columnsCommand.ExecuteReader())
                    {
                        while (columnsReader.Read())
                        {
                            string name = columnsReader.GetString(1);
                            var type = (Column.DataType)Enum.Parse(typeof(Column.DataType), columnsReader.GetString(2), true);
                            columns.Add(new Column(name, type));
                        }
                    }

                    Tables.Add(tableName, new Table(tableName, columns));
                }
            }
        }

        public List<Table> GetTables()
        {
            Refresh();

            return Tables.Values.ToList();
        }

        public bool CreateTable(string name, List<Column> columns)
        {
            Refresh();

            if (Tables.ContainsKey(name))
            {
                return false;
            }

            var command = connection.CreateCommand();
            command.CommandText = $"CREATE TABLE IF NOT EXISTS {name} ({string.Join(", ", columns.Select(c => $"{c.Name} {c.Type}"))})";
            command.ExecuteNonQuery();

            Tables.Add(name, new Table(name, columns));

            return true;
        }

        public bool DropTable(string name)
        {
            Refresh();

            if (!Tables.ContainsKey(name))
            {
                return false;
            }

            var command = connection.CreateCommand();
            command.CommandText = $"DROP TABLE {name}";
            command.ExecuteNonQuery();

            Tables.Remove(name);

            return true;
        }
    }
}
