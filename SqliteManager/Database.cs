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
                            columns.Add(new Column(tableName, name, type));
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

        public bool CreateTable(string tableName, List<Column> columns)
        {
            Refresh();

            if (Tables.ContainsKey(tableName))
            {
                return false;
            }

            columns.Add(new Column(tableName, "id", Column.DataType.INTEGER));

            var command = connection.CreateCommand();
            command.CommandText = $"CREATE TABLE IF NOT EXISTS {tableName} ({string.Join(", ", columns.Select(c => $"{c.Name} {c.Type}"))})";
            command.ExecuteNonQuery();

            return true;
        }

        public bool RenameTable(string tableName, string newTableName)
        {
            Refresh();

            if (!Tables.ContainsKey(tableName))
            {
                return false;
            }

            var command = connection.CreateCommand();
            command.CommandText = $"ALTER TABLE {tableName} RENAME TO {newTableName}";
            command.ExecuteNonQuery();

            return true;
        }

        public bool DropTable(string tableName)
        {
            Refresh();

            if (!Tables.ContainsKey(tableName))
            {
                return false;
            }

            var command = connection.CreateCommand();
            command.CommandText = $"DROP TABLE {tableName}";
            command.ExecuteNonQuery();

            return true;
        }

        public bool AddColumn(string tableName, string columnName, Column.DataType columnType)
        {
            Refresh();

            if (!Tables.ContainsKey(tableName))
            {
                return false;
            }

            var command = connection.CreateCommand();
            command.CommandText = $"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnType}";
            command.ExecuteNonQuery();

            return true;
        }

        public bool RenameColumn(string tableName, string columnName, string newColumnName)
        {
            Refresh();

            if (!Tables.ContainsKey(tableName))
            {
                return false;
            }

            var command = connection.CreateCommand();
            command.CommandText = $"ALTER TABLE {tableName} RENAME COLUMN {columnName} TO {newColumnName}";
            command.ExecuteNonQuery();

            return true;
        }

        public bool DeleteColumn(string tableName, string columnName)
        {
            Refresh();

            if (!Tables.ContainsKey(tableName))
            {
                return false;
            }
            else if (Tables[tableName].Columns.Count == 1)
            {
                return false;
            }

            var command = connection.CreateCommand();
            command.CommandText = $"ALTER TABLE {tableName} DROP COLUMN {columnName}";
            command.ExecuteNonQuery();

            return true;
        }
    }
}
