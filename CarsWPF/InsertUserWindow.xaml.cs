using Npgsql;
using System;
using System.IO;
using System.Windows;

namespace CarsWPF
{
    /// <summary>
    /// Lógica interna para InsertUserWindow.xaml
    /// </summary>
    public partial class InsertUserWindow : Window
    {
        IniFile ini = new();
        NpgsqlConnection conn = new();

        public InsertUserWindow()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CarsWPF.ini");
            if (!File.Exists(path))
            {
                ini.Write("ip", "127.0.0.1");
                ini.Write("port", "5433");
                ini.Write("base", "codingbase");
            }
            var ip = ini.Read("ip");
            var port = ini.Read("port");
            var db = ini.Read("base");

            string con = ($"Server={ip}; Port={port}; Database={db}; User Id=postgres; Password=pedrow2001;");

            conn.ConnectionString = con;
            conn.Open();
            string sql;

            if(checkSU.IsChecked == true)
            {
                sql = $"CREATE USER {txbUser.Text} WITH PASSWORD '{txbPass.Password}';" +
                         $"INSERT INTO users (login, password) VALUES ('{txbUser.Text}', crypt('{txbPass.Password}', gen_salt('md5')));" +
                         $"GRANT ALL ON ALL TABLES IN SCHEMA public TO {txbUser.Text};" +
                         $"GRANT EXECUTE ON ALL FUNCTIONS IN SCHEMA public TO {txbUser.Text};";
            }
            else
            {
                sql = $"CREATE USER {txbUser.Text} WITH PASSWORD '{txbPass.Password}';" +
                         $"INSERT INTO users (login, password) VALUES ('{txbUser.Text}', crypt('{txbPass.Password}', gen_salt('md5')));" +
                         $"GRANT SELECT ON ALL TABLES IN SCHEMA public TO {txbUser.Text};" +
                         $"GRANT EXECUTE ON ALL FUNCTIONS IN SCHEMA public TO {txbUser.Text};";
            }

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Salvo com sucesso!");
            conn.Close();
            Close();
        }

        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
