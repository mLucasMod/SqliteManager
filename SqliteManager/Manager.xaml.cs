using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;
using System.IO;

namespace SqliteManager
{
    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Page
    {
        private Database database;
        public Database db 
        {
            get => database;
            set
            {
                database = value;
                FileName.Content = Path.GetFileName(db.Filepath);
                Refresh();
            }
        }

        public Manager()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            var tables = db.GetTables();
            TablesListBox.ItemsSource = tables;
        }

        private void RenameTable_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Table table)
            {
                var inputWindow = new InputWindow();

                if (inputWindow.ShowDialog() == true)
                {
                    db.RenameTable(table.Name, inputWindow.UserInput);
                }
            }

            Refresh();
        }

        private void DropTable_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Table table)
            {
                db.DropTable(table.Name);
            }

            Refresh();
        }


        private void AddColumn_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Table table)
            {
                var inputWindow = new InputWindow();

                if (inputWindow.ShowDialog() == true)
                {
                    db.AddColumn(table.Name, inputWindow.UserInput, Column.DataType.TEXT);
                }
            }

            Refresh();
        }

        private void RenameColumn_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Column column)
            {
                var inputWindow = new InputWindow();

                if (inputWindow.ShowDialog() == true)
                {
                    db.AddColumn(column.TableName, inputWindow.UserInput, Column.DataType.TEXT);
                }
            }

            Refresh();
        }

        private void DeleteColumn_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Column column)
            {
                db.DeleteColumn(column.TableName, column.Name);
            }

            Refresh();
        }
    }
}
