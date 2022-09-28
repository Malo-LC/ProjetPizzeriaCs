using System;
using System.Collections.Generic;
using System.Diagnostics;
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


namespace ProjetPizzeria
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private void ButtonAddNewClient(object sender, RoutedEventArgs e)
        {
            
            
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Ajouter_un_client_Click(object sender, RoutedEventArgs e)
        {
            AjouterNouveauClient add = new AjouterNouveauClient();
            add.ShowDialog();
            add.Close();
        }

    }
}
