﻿using Npgsql;
using System.Collections.Generic;
using System.Data;
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
        LoginWindow loginWindow = new LoginWindow();
        List<Cars> listCars = new List<Cars>();
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
            if (txb.Text == null)
            {
                MostrarCarros();
            }
            else
            {
                if(cbSearch.SelectedIndex == 0)
                {
                    var filteredList = listCars.Where(x => x.Name.ToLower().Contains(txb.Text.ToLower()));
                    dg.ItemsSource = null;
                    MostrarCarros();
                    dg.ItemsSource = filteredList;
                }
                else
                {
                    var filteredList = listCars.Where(x => x.Brand.ToLower().Contains(txb.Text.ToLower()));
                    dg.ItemsSource = null;
                    MostrarCarros();
                    dg.ItemsSource = filteredList;
                }
            }
        }

        private void btnMin_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void MostrarCarros()
        {
            string sql = "SELECT * FROM get_cars();";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            listCars.Clear();

            while (reader.Read())
            {
                Cars car = new(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetDouble(3),
                    reader.GetInt32(4),
                    reader.GetInt32(5),
                    reader.GetString(6)
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
