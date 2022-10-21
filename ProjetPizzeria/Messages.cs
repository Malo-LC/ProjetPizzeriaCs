using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace ProjetPizzeria
{

    class Commande { }

    class Program
    {
        public static async Task Main(int id)
        {
            List<string> taille = new();
            List<string> boisson = new();
            string adresse = "";
            double prix = 0;
            string nomClient;
            List<string> pizza = new();
            MySqlConnection sqlCon = new MySqlConnection("Server=localhost;Database=wpfpizzeria;User Id=root;Password=password;");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    MySqlCommand queryinfo = new MySqlCommand();
                    queryinfo.Connection = sqlCon;
                    queryinfo.CommandText = "SELECT c.nomClient,cl.Rue,ci.prix, ci.boisson, ci.taille,ci.pizza FROM commande c Join client cl ON c.IDclient = cl.ID Join commandeitem ci ON ci.commandeID = c.ID WHERE c.ID = ?id;";
                    queryinfo.Parameters.Add("id",MySqlDbType.Int64).Value = id;
                    var reader = queryinfo.ExecuteReader();
                    while (reader.Read())
                    {
                        taille.Add((string)reader["taille"]);
                        boisson.Add((string)reader["boisson"]);
                        adresse = (string)reader["Rue"];
                        prix += (double)reader["prix"];
                        nomClient = (string)reader["nomClient"];
                        pizza.Add((string)reader["pizza"]);
                    }
                    reader.Close();

                    ///////////////////////////////////////////////////////
                    MySqlCommand queryEtat = new MySqlCommand();
                    queryEtat.Connection = sqlCon;


                    queryEtat.CommandText = "INSERT INTO etat(IDcom,etatcom) VALUES(?id,?etat)";
                    queryEtat.Parameters.Add("id", MySqlDbType.Int64).Value = id;
                    queryEtat.Parameters.Add("etat", MySqlDbType.VarChar).Value = "Debut de la commande";
                    queryEtat.ExecuteNonQuery();

                    Trace.WriteLine($"Preparation de la commande id : {id}");
                    for (int i = 0; i < taille.Count; i++)
                    {
                        queryEtat.CommandText = "UPDATE etat SET etatcom = ?etat WHERE idCom=?id";
                        queryEtat.Parameters.Clear();
                        queryEtat.Parameters.Add("id", MySqlDbType.Int64).Value = id;
                        queryEtat.Parameters.Add("etat", MySqlDbType.VarChar).Value = $"Preparation de la {i+1}e pizza";
                        queryEtat.ExecuteNonQuery();
                        await fairePizza(taille[i], pizza[i]);
                        await RajouterBoisson(boisson[i]);
                    }
                    queryEtat.CommandText = "UPDATE etat SET etatcom = ?etat WHERE idCom=?id";
                    queryEtat.Parameters.Clear();

                    queryEtat.Parameters.Add("id", MySqlDbType.Int64).Value = id;
                    queryEtat.Parameters.Add("etat", MySqlDbType.VarChar).Value = "Livraison de la commande";
                    queryEtat.ExecuteNonQuery();
                    await LivrerCommande(adresse,prix);
                    queryEtat.CommandText = "UPDATE etat SET etatcom = ?etat WHERE idCom=?id";
                    queryEtat.Parameters.Clear();
                    queryEtat.Parameters.Add("id", MySqlDbType.Int64).Value = id;
                    queryEtat.Parameters.Add("etat", MySqlDbType.VarChar).Value = "Commande livrée";
                    queryEtat.ExecuteNonQuery();

                    Trace.WriteLine("Fin de cette commande");
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

        private static async Task<Commande> fairePizza(string size,string pizza)
        {
            Trace.WriteLine($"Preparation d'une {pizza} de taille {size}");
            Trace.WriteLine("Rassemblement de tout les ingrédients requis");
            await Task.Delay(5000);
            Trace.WriteLine("Placement de la base");
            await Task.Delay(2000);
            Trace.WriteLine("Rajout des ingrédients");
            await Task.Delay(6000);
            Trace.WriteLine("Cuisson...");
            await Task.Delay(15000);
            Trace.WriteLine("Mise de la pizza en carton");
            await Task.Delay(3000);
            Trace.WriteLine("Pizza Finie !!");
            return new Commande();
        }
        private static async Task<Commande> RajouterBoisson(string boiss)
        {
            Trace.WriteLine($"Rajout du {boiss} dans la commande");
            await Task.Delay(5000);
            Trace.WriteLine("Boisson rajoutée !");
            return new Commande();
        }

        private static async Task<Commande> LivrerCommande(string adresse,double prix)
        {
            Trace.WriteLine($"livraison de la commande au {adresse}");
            await Task.Delay(10000);
            Trace.WriteLine("Arrivé à l'adresse indiquée");
            Trace.WriteLine($"Règlement en espèce de la commande, cout total : {prix}");
            await Task.Delay(2000);
            Trace.WriteLine("Commande livrée avec succès");
            return new Commande();
        }

    }
}