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
using System.Windows.Shapes;

namespace SqliteManager
{
    /// <summary>
    /// Interaction logic for Browser.xaml
    /// </summary>
    public partial class Browser : Page
    {
        private Database database;
        public Database db
        {
            get => database;
            set
            {
                database = value;
                Refresh();
            }
        }

        public Browser()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            
        }
    }
}
