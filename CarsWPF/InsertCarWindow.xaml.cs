using Npgsql;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CarsWPF
{
    /// <summary>
    /// Lógica interna para InsertCarWindow.xaml
    /// </summary>
    public partial class InsertCarWindow : Window
    {
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
            try
            {
                string namecar = txbName.Text;
                string colorcar = txbColor.Text;
                double pricecar = double.Parse(txbPrice.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Currency);
                string yearcar = txbYear.Text;
                string qtpassengers = txbQtde.Text;
                int brand = 1;
                if (cb.SelectedValue != null)
                {
                    brand = (int)cb.SelectedValue;
                }

                string sql = $"SELECT public.insertcar('{namecar}', '{colorcar}', {pricecar}, {yearcar}, {qtpassengers}, {brand})";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Inserido com sucesso!", "Concluído");
                Close();
            }
            catch (Exception)
            {
                CheckFields();
                MessageBox.Show("Não é possível inserir novo carro!\nVerifique o preenchimento dos dados.", "Erro");
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }

            if (e.Key == Key.Enter)
            {
                try
                {
                    string namecar = txbName.Text;
                    string colorcar = txbColor.Text;
                    double pricecar = double.Parse(txbPrice.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Currency);
                    string yearcar = txbYear.Text;
                    string qtpassengers = txbQtde.Text;
                    int brand = 1;
                    if (cb.SelectedValue != null)
                    {
                        brand = (int)cb.SelectedValue;
                    }

                    string sql = $"SELECT public.insertcar('{namecar}', '{colorcar}', {pricecar}, {yearcar}, {qtpassengers}, {brand})";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Inserido com sucesso!", "Concluído");
                    Close();
                }
                catch (Exception)
                {
                    CheckFields();
                    MessageBox.Show("Não é possível inserir novo carro!\nVerifique o preenchimento dos dados.", "Erro");
                }
            }
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

        private void insertBrand_Click(object sender, RoutedEventArgs e)
        {
            InsertBrandWindow insertBrandWindow = new InsertBrandWindow();
            insertBrandWindow.conn = conn;
            insertBrandWindow.ShowDialog();
        }

        private void CheckFields()
        {
            if (string.IsNullOrEmpty(txbName.Text))
            {
                warningLabelName.Content = "Digite um nome!";
            }
            if (string.IsNullOrEmpty(txbColor.Text))
            {
                warningLabelColor.Content = "Digite uma cor!";
            }
            if (string.IsNullOrEmpty(txbPrice.Text))
            {
                warningLabelPrice.Content = "Digite um preço!";
            }
            if (string.IsNullOrEmpty(txbYear.Text))
            {
                warningLabelYear.Content = "Digite um ano!";
            }
            if (string.IsNullOrEmpty(txbQtde.Text))
            {
                warningLabelQtde.Content = "Digite uma quantidade!";
            }
        }

        private void txb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txbName.Text)) warningLabelName.Content = "";
            if (!string.IsNullOrEmpty(txbColor.Text)) warningLabelColor.Content = "";
            if (!string.IsNullOrEmpty(txbPrice.Text)) warningLabelPrice.Content = "";
            if (!string.IsNullOrEmpty(txbYear.Text)) warningLabelYear.Content = "";
            if (!string.IsNullOrEmpty(txbQtde.Text)) warningLabelQtde.Content = "";
        }
    }
}
