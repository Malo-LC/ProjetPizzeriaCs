using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;


namespace ProjetPizzeria
{
    public partial class ChercherClient : Window
    {
        private int clientId = 0;
        public ChercherClient()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ChercherClient1_Click(object sender, RoutedEventArgs e)
        {
            if (TelephoneClient.Text == "" || !int.TryParse(TelephoneClient.Text, out int value))
            {
                MessageBox.Show("Veuillez entrer un téléphone valide");
                return;
            }
            MySqlConnection sqlCon = new MySqlConnection("Server=localhost;Database=wpfpizzeria;User Id=root;Password=password;");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    MySqlCommand query = new MySqlCommand();
                    query.Connection = sqlCon;
                    query.CommandText = "SELECT * FROM client WHERE Telephone = ?Tel";
                    query.Parameters.Add("Tel", MySqlDbType.Int64).Value = TelephoneClient.Text;
                    var reader = query.ExecuteReader();
                    DataClient.Items.Clear();
                    if (reader.Read())
                    {
                        DataClient.Items.Add("Nom : " + reader["Nom"]);
                        DataClient.Items.Add("Prenom : " + reader["Prenom"]);
                        DataClient.Items.Add("Telephone : " + reader["Telephone"]);
                        DataClient.Items.Add("Adresse : " + reader["Rue"] + " | " + reader["Ville"]);

                        NomClient.Text = (string)reader["Nom"];
                        PrenomClient.Text = (string)reader["Prenom"];
                        RueClient.Text = (string)reader["Rue"];
                        ZipCodeClient.Text = reader["Zipcode"].ToString();
                        VilleClient.Text = (string)reader["Ville"];
                        DatePremiereCommande.Text = (string)reader["DatePremiereCommande"];
                        TelephoneClientMod.Text = reader["Telephone"].ToString();

                        clientId = (int)reader["ID"];

                    }
                    else
                    {
                        DataClient.Items.Add("Aucun client trouvé");
                    }
                    reader.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        

        private void SupprimerClient_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection sqlCon = new MySqlConnection("Server=localhost;Database=wpfpizzeria;User Id=root;Password=password;");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    MySqlCommand query = new MySqlCommand();
                    query.Connection = sqlCon;
                    query.CommandText = "DELETE FROM client WHERE ID = ?id";
                    query.Parameters.Add("id", MySqlDbType.Int32).Value = clientId;
                    query.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void ModifierClient_Click(object sender, RoutedEventArgs e)
        {
            if (clientId != 0)
            {
                Trace.WriteLine(clientId);
                MySqlConnection sqlCon = new MySqlConnection("Server=localhost;Database=wpfpizzeria;User Id=root;Password=password;");
                try
                {
                    if (sqlCon.State == System.Data.ConnectionState.Closed)
                    {
                        sqlCon.Open();
                        MySqlCommand query = new MySqlCommand();
                        query.Connection = sqlCon;
                        query.CommandText = "UPDATE client SET Nom = ?Nom , Prenom = ?Prenom , Telephone = ?Telephone , DatePremiereCommande = ?Date , Rue = ?Rue , Zipcode = ?Zip , Ville = ?Ville WHERE ID = ?id";
                        query.Parameters.Add("id", MySqlDbType.Int64).Value = clientId;
                        query.Parameters.Add("Nom", MySqlDbType.VarChar).Value = NomClient.Text;
                        query.Parameters.Add("Prenom", MySqlDbType.VarChar).Value = PrenomClient.Text;
                        query.Parameters.Add("Telephone", MySqlDbType.Int64).Value = TelephoneClientMod.Text;
                        query.Parameters.Add("Date", MySqlDbType.VarChar).Value = DatePremiereCommande.Text;
                        query.Parameters.Add("Rue", MySqlDbType.VarChar).Value = RueClient.Text;
                        query.Parameters.Add("Zip", MySqlDbType.Int64).Value = ZipCodeClient.Text;
                        query.Parameters.Add("Ville", MySqlDbType.VarChar).Value = VilleClient.Text;
                        query.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                }
            }
        }

        private void DatePremiereCommandeTC(object sender, TextChangedEventArgs e)
        {

        }

        private void TelephoneClientTC(object sender, TextChangedEventArgs e)
        {

        }

        private void RueClientTC(object sender, TextChangedEventArgs e)
        {

        }

        private void ZipCodeClient_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void VilleClient_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PrenomClientTC(object sender, TextChangedEventArgs e)
        {

        }

        private void NomClientTC(object sender, TextChangedEventArgs e)
        {

        }
    }
}
