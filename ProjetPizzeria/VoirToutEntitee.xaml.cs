using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ProjetPizzeria
{
    /// <summary>
    /// Interaction logic for VoirToutEntitee.xaml
    /// </summary>
    public partial class VoirToutEntitee : Window
    {

        public class EntityData
        {
            public string Nom { set; get; }
            public string Prenom { set; get; }
            public string Ville { set; get; }
            public double Cumul { set; get; }
            public string Type { set; get; }
        }
        public VoirToutEntitee()
        {
            InitializeComponent();
            MySqlConnection sqlCon = new MySqlConnection("Server=localhost;Database=wpfpizzeria;User Id=root;Password=password;");
            MySqlConnection sqlCon2 = new MySqlConnection("Server=localhost;Database=wpfpizzeria;User Id=root;Password=password;");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed && sqlCon2.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    sqlCon2.Open();
                    MySqlCommand query = new MySqlCommand();
                    query.Connection = sqlCon;
                    query.CommandText = "SELECT * FROM client";
                    var reader = query.ExecuteReader();
                    while (reader.Read())
                    {
                        MySqlCommand queryTot = new MySqlCommand();
                        queryTot.Connection = sqlCon2;
                        queryTot.CommandText = "SELECT c.Total FROM wpfpizzeria.commande c where c.IDclient=?id";
                        queryTot.Parameters.Add("id", MySqlDbType.Int64).Value = (int)reader["ID"];
                        var readerTot = queryTot.ExecuteReader();
                        double total = 0;
                        while (readerTot.Read())
                        {
                            total += (double)readerTot["Total"];
                        }
                        readerTot.Close();
                        EntityData data = new EntityData();
                        data.Cumul = total;
                        data.Type = "Client";
                        data.Ville = (string)reader["Ville"];
                        data.Prenom = (string)reader["Prenom"];
                        data.Nom = (string)reader["Nom"];
                        EntityList.Items.Add(data);
                    }
                    reader.Close();
                    MySqlCommand queryCommis = new MySqlCommand();
                    queryCommis.Connection = sqlCon;
                    queryCommis.CommandText = "SELECT * FROM commis";
                    var readerCommis = queryCommis.ExecuteReader();
                    while (readerCommis.Read())
                    {
                        EntityData data = new EntityData();
                        data.Cumul = 0;
                        data.Type = "Commis";
                        data.Ville = (string)readerCommis["Ville"];
                        data.Prenom = (string)readerCommis["Prenom"];
                        data.Nom = (string)readerCommis["Nom"];
                        EntityList.Items.Add(data);
                    }
                    readerCommis.Close();
                    MySqlCommand queryLivreur = new MySqlCommand();
                    queryLivreur.Connection = sqlCon;
                    queryLivreur.CommandText = "SELECT * FROM livreur";
                    var readerLivreur = queryLivreur.ExecuteReader();
                    while (readerLivreur.Read())
                    {
                        EntityData data = new EntityData();
                        data.Cumul = 0;
                        data.Type = "Livreur";
                        data.Ville = (string)readerLivreur["Ville"];
                        data.Prenom = (string)readerLivreur["Prenom"];
                        data.Nom = (string)readerLivreur["Nom"];
                        EntityList.Items.Add(data);
                    }
                    readerLivreur.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
                sqlCon2.Close();
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
