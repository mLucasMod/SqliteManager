using Microsoft.Win32;
using System.Data.Common;
using System.Diagnostics;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database db;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RefreshTables()
        {
            var tables = db.GetTables();
            TablesListBox.ItemsSource = tables;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (db != null)
            {
                RefreshTables();
            }
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "SQLite Database (*.db)|*.db",
                Title = "Créer un nouveau fichier .db"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                db = new Database(saveFileDialog.FileName);
                FileName.Content = Path.GetFileName(saveFileDialog.FileName);
            }
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Fichiers SQLite (*.db)|*.db|Tous les fichiers (*.*)|*.*",
                Title = "Sélectionner un fichier SQLite"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                db = new Database(openFileDialog.FileName);
                FileName.Content = Path.GetFileName(openFileDialog.FileName);
                RefreshTables();
            }
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save");
        }

        private void SaveFileAs_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save As");
        }

        private void DeleteFile_Click(Object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete Database");
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void AddTable_Click(object sender, RoutedEventArgs e)
        {
            var inputWindow = new InputWindow();

            if (inputWindow.ShowDialog() == true)
            {
                db.CreateTable(inputWindow.UserInput, new List<Column>());
            }

            RefreshTables();
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

            RefreshTables();
        }

        private void DeleteTable_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Table table)
            {
                db.DropTable(table.Name);
            }

            RefreshTables();
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

            RefreshTables();
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

            RefreshTables();
        }

        private void DeleteColumn_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is Column column)
            {
                db.DeleteColumn(column.TableName, column.Name);
            }

            RefreshTables();
        }
    }
}