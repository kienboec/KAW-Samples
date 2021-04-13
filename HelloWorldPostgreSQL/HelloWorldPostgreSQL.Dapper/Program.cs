using System;
using System.Linq;
using Dapper;
using Npgsql;

namespace HelloWorldPostgreSQL.Dapper
{
    class Program
    {
        class DapperData
        {
            public string X { get; set; }
        }
        static void Main(string[] args)
        {
            // SELECT * FROM pg_stat_activity;
            using NpgsqlConnection connection = new NpgsqlConnection("Application Name=HelloWorldPostgreSQL.Dapper;Database=postgres;Host=localhost;Port=5432;Username=postgres");
            connection.Open();

            connection.Execute("CREATE TABLE IF NOT EXISTS dapper_data (x varchar(100))");
            connection.Execute("INSERT INTO dapper_data VALUES (@p)", new {p = "Hello Dapper"});
            var items = connection.Query<DapperData>("SELECT * FROM dapper_data");
            items.ToList().ForEach(item => Console.WriteLine(item.X));

            connection.Close();

        }
    }
}
