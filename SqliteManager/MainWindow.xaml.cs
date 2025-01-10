using Microsoft.Win32;
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
using System.Windows.Shapes;

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
            MessageBox.Show("Refresh");
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
            }

            RefreshTables();
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



        private void DeleteColumn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Column column)
            {
                // Récupérer la table à laquelle appartient cette colonne
                var table = TablesListBox.ItemsSource
                    .OfType<Table>()
                    .FirstOrDefault(t => t.Columns.Contains(column));

                if (table != null)
                {
                    // Supprimer la colonne de la liste
                    table.Columns.Remove(column);

                    // Met à jour la base de données (SQLite ne supporte pas bien la suppression directe de colonne)
                    db.DropTable(table.Name); // Supprime la table
                    db.CreateTable(table.Name, table.Columns); // La recrée sans la colonne supprimée

                    RefreshTables(); // Rafraîchir l'affichage
                }
            }
        }
    }
}