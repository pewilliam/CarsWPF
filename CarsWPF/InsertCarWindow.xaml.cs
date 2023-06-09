using Npgsql;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace CarsWPF
{
    /// <summary>
    /// Lógica interna para InsertCarWindow.xaml
    /// </summary>
    public partial class InsertCarWindow : Window
    {
        IniFile ini = new IniFile();
        public NpgsqlConnection conn = new NpgsqlConnection();
        readonly List<Brand> listBrands = new List<Brand>();

        public InsertCarWindow()
        {
            InitializeComponent();
            txbName.Focus();
        }

        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            string namecar = txbName.Text;
            string colorcar = txbColor.Text;
            double pricecar = double.Parse(txbPrice.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Currency);
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

        private void PreviewCharInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void PreviewNumberInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txbPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            double amount;
            txbPrice.Text = (double.TryParse(txbPrice.Text, out amount)) ? amount.ToString("C") : string.Empty;
        }

        private void txbPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txbPrice.Text != string.Empty)
            {
                double amount = double.Parse(txbPrice.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Currency);
                txbPrice.Text = amount.ToString();
                txbPrice.CaretIndex = txbPrice.Text.Length;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}
