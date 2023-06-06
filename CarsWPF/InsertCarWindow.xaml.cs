using Npgsql;
using System.Collections.Generic;
using System.Windows;

namespace CarsWPF
{
    /// <summary>
    /// Lógica interna para InsertCarWindow.xaml
    /// </summary>
    public partial class InsertCarWindow : Window
    {
        IniFile ini = new IniFile();
        NpgsqlConnection conn = new NpgsqlConnection();
        List<Brand> listBrands = new List<Brand>();

        public InsertCarWindow()
        {
            InitializeComponent();
            ConnectionDb();
        }

        public void ConnectionDb()
        {
            var ip = ini.Read("ip");
            var port = ini.Read("port");
            var db = ini.Read("base");

            string con = ($"Server={ip}; Port={port}; Database={db}; User Id=postgres; Password=pedrow2001;");
            conn.ConnectionString = con;
            conn.Open();
        }

        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            string namecar = txbName.Text;
            string colorcar = txbColor.Text;
            string pricecar = txbPrice.Text;
            string yearcar = txbYear.Text;
            string qtpassengers = txbQtde.Text;
            int brand = (int)cb.SelectedValue;

            string sql = $"SELECT public.insertcar('{namecar}', '{colorcar}', {pricecar}, {yearcar}, {qtpassengers}, {brand})";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Inserido com sucesso!");
            Close();
        }

        private void PopulateCB(object sender, System.EventArgs e)
        {
            string sql = "SELECT * FROM brands;";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            listBrands.Clear();

            while (reader.Read())
            {
                Brand brand = new(
                    reader.GetInt32(0),
                    reader.GetString(1)
                    );

                listBrands.Add(brand);
            }
            reader.Close();
            cb.ItemsSource = listBrands;
            cb.Items.Refresh();
        }
    }
}
