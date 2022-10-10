using MySql.Data.MySqlClient;
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
using static ProjetPizzeria.VoirToutEntitee;

namespace ProjetPizzeria
{
    /// <summary>
    /// Interaction logic for AjouterCommisLivreur.xaml
    /// </summary>
    public partial class AjouterCommisLivreur : Window
    {
        public AjouterCommisLivreur()
        {
            InitializeComponent();
        }

        private void NomCommis_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PrenomCommis_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Nomlivreur_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PrenomLivreur_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void VilleLivreur_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void VilleCommis_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AjouterCommis_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection sqlCon = new MySqlConnection("Server=localhost;Database=wpfpizzeria;User Id=root;Password=password;");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    MySqlCommand query = new MySqlCommand();
                    query.Connection = sqlCon;
                    query.CommandText = "INSERT INTO commis(Nom, Prenom, Ville) VALUE(?nom,?prenom,?ville)";
                    query.Parameters.Add("nom", MySqlDbType.VarChar).Value = NomCommis.Text.ToString();
                    query.Parameters.Add("prenom", MySqlDbType.VarChar).Value = PrenomCommis.Text.ToString();
                    query.Parameters.Add("ville", MySqlDbType.VarChar).Value = VilleCommis.Text.ToString();
                    var reader = query.ExecuteNonQuery();
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
                MessageBox.Show("Commis Ajouté");
                NomCommis.Clear();
                PrenomCommis.Clear();
                VilleCommis.Clear();
            }
        }

        private void AjouterLiveur_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection sqlCon = new MySqlConnection("Server=localhost;Database=wpfpizzeria;User Id=root;Password=password;");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    MySqlCommand query = new MySqlCommand();
                    query.Connection = sqlCon;
                    query.CommandText = "INSERT INTO livreur(Nom, Prenom, Ville) VALUE(?nom,?prenom,?ville)";
                    query.Parameters.Add("nom", MySqlDbType.VarChar).Value = Nomlivreur.Text.ToString();
                    query.Parameters.Add("prenom", MySqlDbType.VarChar).Value = PrenomLivreur.Text.ToString();
                    query.Parameters.Add("ville", MySqlDbType.VarChar).Value = VilleLivreur.Text.ToString();
                    var reader = query.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
                MessageBox.Show("Livreur Ajouté");
                Nomlivreur.Clear();
                PrenomLivreur.Clear();
                VilleLivreur.Clear();
            }
        }
        
        
    }
}
