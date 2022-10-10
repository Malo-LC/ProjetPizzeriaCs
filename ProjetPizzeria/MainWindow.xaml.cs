using System.Windows;
using System.Windows.Input;

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

        private void ChercherClient_Click(object sender, RoutedEventArgs e)
        {
            ChercherClient search = new ChercherClient();
            search.ShowDialog();
            search.Close();
        }

        private void CommanderPizza_Click(object sender, RoutedEventArgs e)
        {
            CommanderPizza commande = new CommanderPizza();
            commande.ShowDialog();
            commande.Close();
        }

        private void ToutClients_Click(object sender, RoutedEventArgs e)
        {
            VoirToutEntitee client = new VoirToutEntitee();
            client.ShowDialog();
            client.Close();
        }

        private void AddCommisLivr_Click(object sender, RoutedEventArgs e)
        {
            AjouterCommisLivreur ajouterCommisLivreur = new AjouterCommisLivreur();
            ajouterCommisLivreur.ShowDialog();
            ajouterCommisLivreur.Close();
        }

        async private void AsyncTest_Click(object sender, RoutedEventArgs e)
        {
            await Program.Main(1);

        }

        private void VoirCommandes_Click(object sender, RoutedEventArgs e)
        {
            VoirCommandes commandes = new VoirCommandes();
            commandes.ShowDialog();
            commandes.Close();
        }
    }
}
