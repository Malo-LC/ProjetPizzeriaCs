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
    /// Interaction logic for VoirCommandes.xaml
    /// </summary>
    public partial class VoirCommandes : Window
    {

        public class CommandeData
        {
            public int Numero { set; get; }
            public string Nom { set; get; }
            public string Etat { set; get; }
            public double Cout { set; get; }
            public string Heure { set; get; }
        }
        public VoirCommandes()
        {
            InitializeComponent();
            Refresh();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Refresh()
        {
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
                    query.CommandText = "SELECT * FROM commande";
                    var reader = query.ExecuteReader();
                    while (reader.Read())
                    {
                        MySqlCommand queryTot = new MySqlCommand();
                        queryTot.Connection = sqlCon2;
                        queryTot.CommandText = "SELECT * FROM etat where IDcom=?id";
                        queryTot.Parameters.Add("id", MySqlDbType.Int64).Value = (int)reader["ID"];
                        var readerEtat = queryTot.ExecuteReader();
                        CommandeData data = new CommandeData();
                        while (readerEtat.Read())
                        {
                            data.Etat = (string)readerEtat["etatcom"];
                        }
                        readerEtat.Close();
                        data.Nom = (string)reader["nomClient"];
                        data.Numero = (int)reader["ID"];
                        data.Cout = (double)reader["Total"];
                        data.Heure = (string)reader["date"] + " | " + (string)reader["heureCmd"];
                        CommandesListe.Items.Add(data);
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
                sqlCon2.Close();
            }
        }
    

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            CommandesListe.Items.Clear();
             Refresh();
        }
    }
}
