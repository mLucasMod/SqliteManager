using Microsoft.Win32;
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
            MessageBox.Show("New");
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

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}