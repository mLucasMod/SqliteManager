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
using System.Windows.Shapes;

namespace SqliteManager
{
    /// <summary>
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public string UserInput { get; private set; }

        public InputWindow()
        {
            InitializeComponent();
        }

        // Lorsque l'utilisateur appuie sur le bouton OK
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            UserInput = InputTextBox.Text; // Récupère le texte saisi
            DialogResult = true; // Indique que l'entrée est validée
            Close(); // Ferme la fenêtre
        }

        // Lorsque l'utilisateur appuie sur le bouton Annuler
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Indique que l'entrée a été annulée
            Close(); // Ferme la fenêtre
        }
    }
}
