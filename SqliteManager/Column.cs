using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteManager
{
    public class Column : INotifyPropertyChanged
    {
        private string _name;
        private DataType _type;

        public string TableName { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public DataType Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public enum DataType
        {
            INTEGER,
            TEXT,
            BLOB,
            REAL,
            NUMERIC
        }

        public Column(string tableName, string name, DataType type)
        {
            TableName = tableName;
            Name = name;
            Type = type;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
