using Npgsql;
using System;
using System.Windows;

namespace CarsWPF
{
    /// <summary>
    /// Lógica interna para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        DatabaseConnection dbCon = new DatabaseConnection();
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
                conn = dbCon.ConnectionDb(txbUser.Text, txbPassword.Password);
                conn.Open();
                Close();
            }
            catch (Exception ex)
            {
                if(ex.ToString().ToLower().Contains("authentication"))
                {
                    MessageBox.Show("Usuário e/ou senha incorreto(s)!");
                    txbPassword.Focus();
                    txbPassword.SelectAll();
                }
                else
                {
                    MessageBox.Show("Verifique os parâmetros do ini!");
                    IniConfigWindow iniConfigWindow = new IniConfigWindow();
                    iniConfigWindow.ShowDialog();
                }
            }
        }

        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Escape)
            {
                Close();
            }
            warningLabel.Content = "";
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (txbPassword.Password == "")
                {
                    txbPassword.Focus();
                    warningLabel.Content = "Informe uma senha!";
                }
                else
                {
                    btnLogin_Click(sender, e);
                }
            }
        }
    }
}
