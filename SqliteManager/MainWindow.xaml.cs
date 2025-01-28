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
using System.Windows.Shapes;


namespace SqliteManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Manager manager = new Manager();
        Browser browser = new Browser();

        private Database database;
        public Database db 
        { 
            get => database;
            set 
            {
                database = value; 
                manager.db = value;
                browser.db = value;
                manager.Refresh();
                browser.Refresh();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = manager;
            SelectManagerButton.IsChecked = true;
        }

        private void SelectManagerView_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = manager;

            SelectManagerButton.IsChecked = true;
            SelectBrowserButton.IsChecked = false;
        }

        private void SelectBrowserView_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = browser;

            SelectManagerButton.IsChecked = false;
            SelectBrowserButton.IsChecked = true;
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

        private void CreateTable_Click(object sender, RoutedEventArgs e)
        {
            var inputWindow = new InputWindow();

            if (db == null)
            {
                MessageBox.Show("No database selected", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (inputWindow.ShowDialog() == true)
            {
                db.CreateTable(inputWindow.UserInput, new List<Column>());
                manager.Refresh();
                browser.Refresh();
            }
        }
    }
}