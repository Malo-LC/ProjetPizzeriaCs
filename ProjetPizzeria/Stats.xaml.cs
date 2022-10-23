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
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : Window
    {
        public class EntityData
        {
            public string Nom { set; get; }
            public string Prenom { set; get; }
            public string Ville { set; get; }
            public Int64 NbCom { set; get; }
            public string Type { set; get; }
        }
        public Stats()
        {
            InitializeComponent();
            MySqlConnection sqlCon = new MySqlConnection("Server=localhost;Database=wpfpizzeria;User Id=root;Password=password;");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    MySqlCommand queryCommis = new MySqlCommand();
                    queryCommis.Connection = sqlCon;
                    queryCommis.CommandText = "SELECT *, count(s.tel) FROM commis c left outer join suivicommis s on c.tel = s.tel;";
                    var readerCommis = queryCommis.ExecuteReader();
                    while (readerCommis.Read())
                    {
                        EntityData data = new EntityData();
                        data.NbCom = (Int64)readerCommis["count(s.tel)"];
                        data.Type = "Commis";
                        data.Ville = (string)readerCommis["Ville"];
                        data.Prenom = (string)readerCommis["Prenom"];
                        data.Nom = (string)readerCommis["Nom"];
                        EntityList.Items.Add(data);
                    }
                    readerCommis.Close();
                    MySqlCommand queryLivreur = new MySqlCommand();
                    queryLivreur.Connection = sqlCon;
                    queryLivreur.CommandText = "SELECT *, count(s.tel) FROM livreur c left outer join suivilivreur s on c.tel = s.tel;";
                    var readerLivreur = queryLivreur.ExecuteReader();
                    while (readerLivreur.Read())
                    {
                        EntityData data = new EntityData();
                        data.NbCom = (Int64)readerLivreur["count(s.tel)"];
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
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
