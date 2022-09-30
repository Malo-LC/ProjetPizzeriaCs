using MySql.Data.MySqlClient;
using System;
using System.Collections;
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
        private int IdLastCommande;

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
                data.DataPrix = PrixTotal.Content;

                ListePizza.Items.Add(data);


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
            MySqlConnection sqlCon = new MySqlConnection("Server=localhost;Database=wpfpizzeria;User Id=root;Password=password;");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    MySqlCommand queryCommande = new MySqlCommand();
                    queryCommande.Connection = sqlCon;
                    queryCommande.CommandText = "INSERT INTO commande(heure,date,IDclient,nomClient) values(?heure,?date,?IDclient,?nomClient)";
                    queryCommande.Parameters.Add("heure", MySqlDbType.VarChar).Value = DateTime.Now.ToString("hh:mm:ss tt");
                    queryCommande.Parameters.Add("date", MySqlDbType.VarChar).Value = DateTime.Now.ToString("d/M/yyyy");
                    queryCommande.Parameters.Add("IDclient", MySqlDbType.Int64).Value = 15;
                    queryCommande.Parameters.Add("nomClient", MySqlDbType.VarChar).Value = "Malo";
                    queryCommande.ExecuteNonQuery();
                    ///////////////////////////////////
                    
                    MySqlCommand queryLastCommande = new MySqlCommand();
                    queryLastCommande.Connection = sqlCon;
                    queryLastCommande.CommandText = "select * from command order by ID desc limit 1";
                    var reader = queryLastCommande.ExecuteReader();
                    if (reader.Read())
                    {
                        IdLastCommande = (int)reader["ID"];
                    }

                    ///////////////////////////////////
                    
                    for(int i = 0; i < ListePizza.Items.Count; i++)
                    { 
                        
                        MySqlCommand queryItem = new MySqlCommand();
                        queryItem.Connection = sqlCon;
                        var rowContainer = GetR
                        queryItem.CommandText = "inert into commandeitem(commandeID,pizza,taille,type,boisson,prix) values(?cid,?pizza,?taille,?type,?boisson,?prix)";
                        queryItem.Parameters.Add("cid", MySqlDbType.Int64).Value = IdLastCommande;
                        ListePizza.SelectedCells = new DataGridCellInfo(ListePizza.Items[i], ListePizza.Columns[0])
                        queryItem.Parameters.Add("pizza", MySqlDbType.VarChar).Value = ;
                        queryItem.Parameters.Add("taille", MySqlDbType.VarChar).Value = IdLastCommande;

                    }

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
        public IEnumerable<datagridrow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }
    }
}
