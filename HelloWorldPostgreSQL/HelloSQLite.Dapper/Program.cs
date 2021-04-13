using System;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace HelloSQLite.Dapper
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=c:\\repos\\KAW-Samples\\HelloWorldPostgreSQL\\HelloSQLite\\bin\\Debug\\netcoreapp3.1\\testdb.db");
            connection.Open();

            var products = connection.Query<Product>("select * from Products");
            products.ToList().ForEach(product => Console.WriteLine(product.Name));
        }
    }
}
