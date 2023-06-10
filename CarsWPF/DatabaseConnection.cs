using Npgsql;
using System;
using System.IO;

namespace CarsWPF
{
    public class DatabaseConnection
    {
        IniFile ini = new IniFile();
        NpgsqlConnection conn = new NpgsqlConnection();

        public NpgsqlConnection ConnectionDb(string username, string password)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CarsWPF.ini");
            if (!File.Exists(path))
            {
                ini.Write("ip", "127.0.0.1");
                ini.Write("port", "5432");
                ini.Write("base", "codingbase");
            }
            var ip = ini.Read("ip");
            var port = ini.Read("port");
            var db = ini.Read("base");
            var login = username;
            var pass = password;

            string con = ($"Server={ip}; Port={port}; Database={db}; User Id={login}; Password={pass};");
            conn.ConnectionString = con;

            return conn;
        }
    }
}
