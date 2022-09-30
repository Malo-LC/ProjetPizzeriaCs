using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;


namespace ProjetPizzeria
{
    public partial class ChercherClient : Window
    {
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
    }
}
