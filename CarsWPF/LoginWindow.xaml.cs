using Npgsql;
using System;
using System.Windows;
using System.IO;

namespace CarsWPF
{
    /// <summary>
    /// Lógica interna para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        IniFile ini = new();
        public NpgsqlConnection conn = new NpgsqlConnection();

        public LoginWindow()
        {
            InitializeComponent();
            txbUser.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ConnectionDB(txbUser.Text, txbPassword.Password))
                    Close();
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("authentication"))
                    MessageBox.Show("Usuário e/ou senha inválido(s)!");
                else
                    MessageBox.Show("Verifique os dados do ini!");
            }
        }

        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                Close();
            }
            warningLabelUser.Content = "";
            warningLabelPass.Content = "";
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (txbUser.Text == "" && txbPassword.Password != "")
                {
                    txbUser.Focus();
                    warningLabelUser.Content = "Informe um usuário!";
                }
                else if (txbPassword.Password == "" && txbUser.Text != "")
                {
                    txbPassword.Focus();
                    warningLabelPass.Content = "Informe uma senha!";
                }
                else if (txbUser.Text == "" && txbPassword.Password == "")
                {
                    txbUser.Focus();
                    warningLabelUser.Content = "Informe um usuário!";
                    warningLabelPass.Content = "Informe uma senha!";
                }
                else
                {
                    btnLogin_Click(sender, e);
                }
            }
        }

        private bool ConnectionDB(string username, string password)
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

            string con = ($"Server={ip}; Port={port}; Database={db}; User Id={username}; Password={password};");

            conn.ConnectionString = con;
            conn.Open();

            string sql = $"SELECT (password = crypt('{password}', password)) AS password_match FROM users WHERE login = '{username}';";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.GetBoolean(0))
                {
                    reader.Close();
                    return true;
                }
            }
            reader.Close();
            return false;
        }

        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            InsertUserWindow insertUserWindow = new InsertUserWindow();
            insertUserWindow.ShowDialog();
        }
    }
}
