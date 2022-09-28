using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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
using MySql.Data.MySqlClient;

namespace ProjetPizzeria
{
    /// <summary>
    /// Interaction logic for AjouterNouveauClient.xaml
    /// </summary>
    public partial class AjouterNouveauClient : Window
    {
        public AjouterNouveauClient()
        {
            InitializeComponent();
            DatePremiereCommande.Text = DateTime.Now.ToString("d/M/yyyy");
        }

        private void NomClientTC(object sender, TextChangedEventArgs e)
        {

        }

        private void PrenomClientTC(object sender, TextChangedEventArgs e)
        {

        }

        private void TestSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

        private void Conn_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection sqlCon = new MySqlConnection("Server=localhost;Database=wpfpizzeria;User Id=root;Password=password;");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    String query = "SELECT * FROM client";
                    MySqlCommand sqlCmd = new MySqlCommand(query, sqlCon);
                    var reader = sqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var ii = reader.FieldCount;
                        for (int i = 0; i < ii; i++)
                        {
                            if (reader[i] is DBNull)
                                Trace.WriteLine("null");
                            else
                                Trace.WriteLine(String.Format("{0}, {1}",
                                reader["Nom"], reader["Prenom"]));
                        }

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
        private void AjouterClient_Click(object sender, RoutedEventArgs e)
        {
            if (NomClient.Text.Length != 0 && PrenomClient.Text.Length != 0)
            {
                testResultat.Items.Add(NomClient.Text);
                testResultat.Items.Add(PrenomClient.Text);
                NomClient.Clear();
                PrenomClient.Clear();
            }
        }

    }
}

