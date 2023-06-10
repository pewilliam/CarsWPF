using Npgsql;
using System.Windows;
using System.Windows.Input;

namespace CarsWPF
{
    /// <summary>
    /// Lógica interna para InsertBrandWindow.xaml
    /// </summary>
    public partial class InsertBrandWindow : Window
    {
        public NpgsqlConnection conn = new();

        public InsertBrandWindow()
        {
            InitializeComponent();
            txbName.Focus();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if(txbName.Text != "")
            {
                string brandName = txbName.Text;
                string sql = $"SELECT public.insertbrand('{brandName}');";

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Inserido com sucesso!", "Marca inserida");
                Close();
            }
            else
            {
                warningLabel.Content = "Insira o nome da marca!";
                txbName.Focus();
            }
        }

        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
            warningLabel.Content = "";
            if (e.Key == Key.Enter)
            {
                if (txbName.Text == "")
                {
                    txbName.Focus();
                    warningLabel.Content = "Informe um usuário!";
                }
                else
                {
                    btnSalvar_Click(sender, e);
                }
            }
        }
    }
}
