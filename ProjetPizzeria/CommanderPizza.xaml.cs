using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
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

        public class MyPizzaData
        {
            public int DataNum { set; get; }
            public string DataPizza { set; get; }
            public string DataTaille { set; get; }
            public string DataType { set; get; }
            public string DataBoisson { set; get; }
            public object DataPrix { set; get; }
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
            if(SelectionPizza.SelectedItem != null) 
            { 
            if (SelectionPizza.SelectedItem.Equals("Margherita"))
            {
                PrixPizza = 0.00;
            }
            else
            {
                PrixPizza = 1.00;
            }
            }
            SetPrice();
        }

        private void SelectionTaille_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
            if(SelectionType.SelectedItem.Equals("Toutes garnies"))
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

        private void PasserCommande_Click(object sender, RoutedEventArgs e)
        {
            MyPizzaData data = new MyPizzaData();
            if(SelectionPizza.SelectedItem != null && SelectionTaille.SelectedItem != null && SelectionType.SelectedItem != null)
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
                data.DataPrix = PrixTotal.Content;
                Trace.WriteLine(data);

                ListePizza.Items.Add(data);

            }
            
            
            
            
            /*
            MySqlConnection sqlCon = new MySqlConnection("Server=localhost;Database=wpfpizzeria;User Id=root;Password=password;");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    MySqlCommand query = new MySqlCommand();
                    query.Connection = sqlCon;
                    query.CommandText = "INSERT INTO commande * FROM client WHERE Telephone = ?Tel";
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
            }*/
        }

        private void ListePizza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
