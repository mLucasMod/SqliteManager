using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteManager
{
    class Column : INotifyPropertyChanged
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
            INTEGER,   // Entier signé (stocké sur 1, 2, 3, 4, 6 ou 8 octets)
            REAL,      // Nombre à virgule flottante (stocké sur 8 octets)
            TEXT,      // Chaîne de caractères (stockée en UTF-8, UTF-16)
            BLOB,      // Donnée binaire brute
            NULL       // Valeur nulle
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
