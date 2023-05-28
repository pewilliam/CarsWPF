using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CarsWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IniFile ini = new IniFile();
        NpgsqlConnection conn = new NpgsqlConnection();
        List<Cars> listCars = new List<Cars>();

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                ConnectionDb();
                MostrarCarros();
            }
            catch (Exception)
            {
                MessageBox.Show("Verifique os parâmetros do ini!");
                IniConfigWindow iniConfigWindow = new IniConfigWindow();
                iniConfigWindow.ShowDialog();
                ConnectionDb();
                MostrarCarros();
            }
        }

        public void ConnectionDb()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CarsWPF.ini");
            if (!File.Exists(path))
            {
                ini.Write("ip", "127.0.0.1");
                ini.Write("port", "5432");
                ini.Write("base", "base_mm");
            }
            var ip = ini.Read("ip");
            var porta = ini.Read("porta");
            var db = ini.Read("base");

            string con = ($"Server={ip}; Port={porta}; Database={db}; User Id=postgres; Password=pedrow2001;");
            conn.ConnectionString = con;
            conn.Open();
        }

        private void btnFecha_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txb = sender as TextBox;
            if (txb.Text == null)
            {
                MostrarCarros();
            }
            else
            {
                var filteredList = listCars.Where(x => x.Name.ToLower().Contains(txb.Text.ToLower()));
                dg.ItemsSource = null;
                MostrarCarros();
                dg.ItemsSource = filteredList;
            }
        }

        private void btnMin_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void MostrarCarros()
        {
            //string sql = "SELECT p.codigo, nome, unidade, qtdefiscal AS matriz, dep.qtdefiscaldeposito, fil.qtdefiscalfilial, p.precovenda FROM produtos p\r\nLEFT JOIN (\r\nSELECT * FROM(SELECT * FROM dblink('host=127.0.0.1 dbname=base_mmdeposito user=postgres password=postzeus2011','SELECT codigo, qtdefiscal FROM produtos') db(codigo integer, qtdefiscaldeposito numeric)) deposito) dep ON dep.codigo = p.codigo\r\nLEFT JOIN (\r\nSELECT * FROM(SELECT * FROM dblink('host=127.0.0.1 dbname=base_mmfilial user=postgres password=postzeus2011','SELECT codigo, qtdefiscal FROM produtos') db(codigo integer, qtdefiscalfilial numeric)) filial) fil ON fil.codigo = p.codigo\r\n";
            string sql = "SELECT id, name, color, price, year, qtpassengers FROM cars ORDER BY id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            listCars.Clear();
            if (!reader.HasRows)
            {
                dg.Items.Refresh();
                MessageBox.Show("Sem produtos para atualizar!");
            }
            while (reader.Read())
            {
                Cars car = new(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetDouble(3),
                    reader.GetInt32(4),
                    reader.GetInt32(5)
                    );

                listCars.Add(car);
            }
            reader.Close();
            dg.ItemsSource = listCars;
            dg.Items.Refresh();
        }

        private void btnNovo_Click(object sender, RoutedEventArgs e)
        {
            InsertCarWindow insertCarWindow = new InsertCarWindow();
            insertCarWindow.ShowDialog();
            MostrarCarros();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
