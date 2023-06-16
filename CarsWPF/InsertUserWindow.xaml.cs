using Npgsql;
using System;
using System.IO;
using System.Windows;
using System.Xml.Linq;

namespace CarsWPF
{
    /// <summary>
    /// Lógica interna para InsertUserWindow.xaml
    /// </summary>
    public partial class InsertUserWindow : Window
    {
        IniFile ini = new();
        NpgsqlConnection conn = new();
        string sql = "";

        public InsertUserWindow()
        {
            InitializeComponent();
            ConnectionDB();
            txbUser.Focus();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkSU.IsChecked == true)
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
                MessageBox.Show("Usuário inserido com sucesso!", "Concluído");
                conn.Close();
                Close();
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("exists"))
                    MessageBox.Show("Usuário já existente");
                else
                    MessageBox.Show("Não é possível inserir novo usuário!\nVerifique o preenchimento dos dados.", "Erro");
            }
        }

        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckFields()
        {
            if (string.IsNullOrEmpty(txbUser.Text))
            {
                warnginLabelUser.Content = "Digite um usuário!";
            }
            if (string.IsNullOrEmpty(txbPass.Password))
            {
                warnginLabelPass.Content = "Digite uma senha!";
            }
        }
        private void ConnectionDB()
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
        }
    }
}
