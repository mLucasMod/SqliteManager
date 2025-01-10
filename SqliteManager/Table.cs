using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteManager
{
    class Table
    {
        public string Name { get; set; }
        public List<Column> Columns { get; set; }

        public Table(string name, List<Column> columns)
        {
            Name = name;
            Columns = columns;
        }
    }
}
