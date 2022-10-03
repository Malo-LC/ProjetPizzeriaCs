﻿using MySql.Data.MySqlClient;
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
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    MySqlCommand query = new MySqlCommand();
                    query.Connection = sqlCon;
                    query.CommandText = "SELECT * FROM client";
                    var reader = query.ExecuteReader();
                    while (reader.Read())
                    {
                        EntityData data = new EntityData();
                        data.Cumul = (int)reader["ID"];
                        data.Type = "Client";
                        data.Ville = (string)reader["Ville"];
                        data.Prenom = (string)reader["Prenom"];
                        data.Nom = (string)reader["Nom"];
                        EntityList.Items.Add(data);
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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
