using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace ProjetPizzeria
{
    /// <summary>
    /// Interaction logic for CommanderPizza.xaml
    /// </summary>
    public partial class CommanderPizza : Window
    {
        private double PrixTaille = 0.00;
        private double PrixBoisson = 0.00;
        private double PrixType = 0.00;
        private double PrixPizza = 0.00;
        private int IdLastCommande;
        private int IdClient;
        private string NomClient;
        private double CoutTotalCommande = 0;

        public class MyPizzaData
        {
            public int DataNum { set; get; }
            public string DataPizza { set; get; }
            public string DataTaille { set; get; }
            public string DataType { set; get; }
            public string DataBoisson { set; get; }
            public double DataPrix { set; get; }
        }
        public CommanderPizza()
        {
            InitializeComponent();
            List<string> PizzaNames = new List<string>();
            PizzaNames.Add("Margherita");
            PizzaNames.Add("Reine");
            PizzaNames.Add("Campagnarde");
            PizzaNames.Add("Végétarienne");
            PizzaNames.Add("Savoyarde");
            PizzaNames.Add("Hawaiienne");
            PizzaNames.Add("Napolitaine");
            PizzaNames.Add("Américaine");
            PizzaNames.Add("4 fromages");
            SelectionPizza.ItemsSource = PizzaNames;

            List<string> TailleNames = new List<string>();
            TailleNames.Add("Petite");
            TailleNames.Add("Moyenne");
            TailleNames.Add("Grande");
            SelectionTaille.ItemsSource = TailleNames;

            List<string> TypeNames = new List<string>();
            TypeNames.Add("Base tomate");
            TypeNames.Add("Base crème");
            TypeNames.Add("Végétarienne");
            TypeNames.Add("Toutes garnies");
            SelectionType.ItemsSource = TypeNames;

            List<string> BoissonNames = new List<string>();
            BoissonNames.Add("Coca-Cola");
            BoissonNames.Add("Coaca-Cola Zero");
            BoissonNames.Add("Sprite");
            BoissonNames.Add("Oasis");
            BoissonNames.Add("Ice tea");
            BoissonNames.Add("Jus d'orange");
            BoissonNames.Add("Red Bull");
            BoissonNames.Add("Schweppes agrumes");
            BoissonNames.Add("Perrier");
            BoissonNames.Add("Vittel");
            SelectionBoisson.ItemsSource = BoissonNames;

            PrixTotal.Content = 0.00;
        }

        private void SelectionPizza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectionPizza.SelectedItem == null)
            {
                PrixPizza = 0;
                SetPrice();
                return;
            }

            if (SelectionPizza.SelectedItem.Equals("Margherita"))
            {
                PrixPizza = 0.00;
            }
            else
            {
                PrixPizza = 1.00;
            }

            SetPrice();
        }

        private void SelectionTaille_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectionTaille.SelectedItem == null)
            {
                PrixTaille = 0;
                SetPrice();
                return;
            }
            if (SelectionTaille.SelectedItem.Equals("Petite"))
            {
                PrixTaille = 7.00;
            }
            if (SelectionTaille.SelectedItem.Equals("Moyenne"))
            {
                PrixTaille = 10.00;
            }
            if (SelectionTaille.SelectedItem.Equals("Grande"))
            {
                PrixTaille = 13.00;
            }
            SetPrice();
        }

        private void SelectionBoisson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectionBoisson.SelectedItem == null)
            {
                PrixBoisson = 0;
                SetPrice();
                return;
            }
            if (SelectionBoisson.SelectedItem.Equals("Vittel"))
            {
                PrixBoisson = 0.50;
            }
            else if (SelectionBoisson.SelectedItem.Equals("Perrier"))
            {
                PrixBoisson = 0.80;
            }
            else if (SelectionBoisson.SelectedItem.Equals("Red Bull"))
            {
                PrixBoisson = 2.50;
            }
            else
            {
                PrixBoisson = 1.50;
            }
            SetPrice();

        }

        private void SelectionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectionType.SelectedItem == null)
            {
                PrixType = 0;
                SetPrice();

                return;
            }
            if (SelectionType.SelectedItem.Equals("Toutes garnies"))
            {
                PrixType = 2.00;
            }
            else
            {
                PrixType = 0.00;
            }
            SetPrice();
        }

        private void SetPrice()
        {
            PrixTotal.Content = PrixTaille + PrixBoisson + PrixType + PrixPizza;
        }

        private void AjouterItem_Click(object sender, RoutedEventArgs e)
        {
            MyPizzaData data = new MyPizzaData();
            if (SelectionPizza.SelectedItem != null && SelectionTaille.SelectedItem != null && SelectionType.SelectedItem != null)
            {
                data.DataPizza = SelectionPizza.SelectedItem.ToString();
                data.DataType = SelectionType.SelectedItem.ToString();
                data.DataTaille = SelectionTaille.SelectedItem.ToString();
                data.DataNum = ListePizza.Items.Count;
                if (SelectionBoisson.SelectedItem != null)
                {
                    data.DataBoisson = SelectionBoisson.SelectedItem.ToString();
                }
                else
                {
                    data.DataBoisson = null;
                }
                data.DataPrix = (double)PrixTotal.Content;

                ListePizza.Items.Add(data);
                CoutTotalCommande += (double)PrixTotal.Content;

                SelectionBoisson.UnselectAll();
                SelectionPizza.UnselectAll();
                SelectionType.UnselectAll();
                SelectionTaille.UnselectAll();
            }
        }

        private void ListePizza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PasserCommande_Click(object sender, RoutedEventArgs e)
        {
            if (TelClient.Text == "" || !int.TryParse(TelClient.Text,out int value))
            {
                MessageBox.Show("Veuillez entrer un téléphone valide");
                return;
            }
            if (ListePizza.Items.Count == 0)
            {
                MessageBox.Show("Liste de la commande vide !");
                return;
            }
            MySqlConnection sqlCon = new MySqlConnection("Server=localhost;Database=wpfpizzeria;User Id=root;Password=password;");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    MySqlCommand queryClient = new MySqlCommand();
                    queryClient.Connection = sqlCon;
                    queryClient.CommandText = "SELECT * FROM client WHERE Telephone = ?tel";
                    queryClient.Parameters.Add("tel", MySqlDbType.Int32).Value = TelClient.Text.ToString();
                    var readerCl = queryClient.ExecuteReader();
                    if (readerCl.Read())
                    {
                        IdClient = (int)readerCl["ID"];
                        NomClient = (string)readerCl["Nom"];
                    }
                    else
                    {
                        MessageBox.Show("Aucun client trouvé avec ce numéro de téléphone");
                        return;
                    }
                    readerCl.Close();

                    /////////////////////////////////////////
                    MySqlCommand queryCommande = new MySqlCommand();
                    queryCommande.Connection = sqlCon;
                    queryCommande.CommandText = "INSERT INTO commande(heureCmd,date,IDclient,nomClient,Total) values(?heure,?date,?IDclient,?nomClient,?Total)";
                    queryCommande.Parameters.Add("heure", MySqlDbType.VarChar).Value = DateTime.Now.ToString("hh:mm:ss tt");
                    queryCommande.Parameters.Add("date", MySqlDbType.VarChar).Value = DateTime.Now.ToString("d/M/yyyy");
                    queryCommande.Parameters.Add("IDclient", MySqlDbType.Int64).Value = IdClient;
                    queryCommande.Parameters.Add("nomClient", MySqlDbType.VarChar).Value = NomClient;
                    queryCommande.Parameters.Add("Total", MySqlDbType.Double).Value = CoutTotalCommande;
                    queryCommande.ExecuteNonQuery();
                    ///////////////////////////////////
                    
                    MySqlCommand queryLastCommande = new MySqlCommand();
                    queryLastCommande.Connection = sqlCon;
                    queryLastCommande.CommandText = "select * from commande order by ID desc limit 1";
                    var reader = queryLastCommande.ExecuteReader();
                    if (reader.Read())
                    {
                        IdLastCommande = (int)reader["ID"];
                    }
                    reader.Close();

                    ///////////////////////////////////
                    
                    foreach(MyPizzaData dgItem in ListePizza.Items)
                    {
                        MySqlCommand queryItem = new MySqlCommand();
                        queryItem.Connection = sqlCon;
                        queryItem.CommandText = "insert into commandeitem(commandeID,pizza,taille,type,boisson,prix) values(?cid,?pizza,?taille,?type,?boisson,?prix)";
                        queryItem.Parameters.Add("cid", MySqlDbType.Int64).Value = IdLastCommande;
                        queryItem.Parameters.Add("pizza", MySqlDbType.VarChar).Value = dgItem.DataPizza.ToString();
                        queryItem.Parameters.Add("taille", MySqlDbType.VarChar).Value = dgItem.DataTaille.ToString();
                        queryItem.Parameters.Add("type", MySqlDbType.VarChar).Value = dgItem.DataType.ToString();
                        if(dgItem.DataBoisson == null)
                        {
                            queryItem.Parameters.Add("boisson", MySqlDbType.VarChar).Value = "null";

                        }
                        else
                        {
                            queryItem.Parameters.Add("boisson", MySqlDbType.VarChar).Value = dgItem.DataBoisson.ToString();
                        }
                        queryItem.Parameters.Add("prix", MySqlDbType.Double).Value = dgItem.DataPrix.ToString();
                        queryItem.ExecuteNonQuery();
                    }
                    ListePizza.Items.Clear();
                    CoutTotalCommande = 0;
                    MessageBox.Show("Commande Validée");
                    Program.Main(IdLastCommande);
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

        private void TelClient_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
