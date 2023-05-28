using Npgsql;
using System;
using System.Collections.Generic;
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

namespace CarsWPF
{
    /// <summary>
    /// Lógica interna para InsertCarWindow.xaml
    /// </summary>
    public partial class InsertCarWindow : Window
    {
        IniFile ini = new IniFile();
        NpgsqlConnection conn = new NpgsqlConnection();

        public InsertCarWindow()
        {
            InitializeComponent();
            ConnectionDb();
        }

        public void ConnectionDb()
        {
            var ip = ini.Read("ip");
            var porta = ini.Read("porta");
            var db = ini.Read("base");

            string con = ($"Server={ip}; Port={porta}; Database={db}; User Id=postgres; Password=pedrow2001;");
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

            string sql = $"SELECT public.insertcar('{namecar}', '{colorcar}', {pricecar}, {yearcar}, {qtpassengers})";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Inserido com sucesso!");
            Close();
        }
    }
}
