using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CarsWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoginWindow loginWindow = new LoginWindow();
        private List<DataRowView> originalList;
        NpgsqlConnection conn = new NpgsqlConnection();

        public MainWindow()
        {
            loginWindow.ShowDialog();
            InitializeComponent();
            conn = loginWindow.conn;
            txbSearch.Focus();
            cbSearch.SelectedIndex = 0;
            MostrarCarros();
        }

        private void btnFecha_Click(object sender, RoutedEventArgs e)
        {
            conn.Close();
            Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txb = sender as TextBox;

            if (string.IsNullOrWhiteSpace(txb.Text))
            {
                dg.ItemsSource = originalList; // Restaura a lista original
            }
            else
            {
                List<DataRowView> filteredList = null;

                if (cbSearch.SelectedIndex == 0)
                {
                    filteredList = originalList.Where(row =>
                        row["Name"].ToString().ToLower().Contains(txb.Text.ToLower())).ToList();
                }
                else
                {
                    filteredList = originalList.Where(row =>
                        row["Brand"].ToString().ToLower().Contains(txb.Text.ToLower())).ToList();
                }

                dg.ItemsSource = filteredList;
            }
        }

        private void btnMin_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void MostrarCarros()
        {
            string sql = "SELECT * FROM get_cars();";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            reader.Close();

            originalList = dataTable.DefaultView.OfType<DataRowView>().ToList(); // Armazena a lista original

            dg.ItemsSource = dataTable.DefaultView;
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            InsertCarWindow insertCarWindow = new InsertCarWindow();
            insertCarWindow.conn = conn;
            insertCarWindow.ShowDialog();
            MostrarCarros();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.N))
            {
                btnNovo_Click(sender, e);
            }

            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
