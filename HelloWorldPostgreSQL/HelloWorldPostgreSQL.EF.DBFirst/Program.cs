using System;
using System.Linq;

namespace HelloWorldPostgreSQL.EF.DBFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/
            // Scaffold-DbContext "Host=localhost;Database=postgres;Username=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -o Models

            /*
            using var db = new Models.postgresContext();
            db.EntityframeworkData.Add(new Models.EntityframeworkData() {X = "Hello EF-DBFirst"});
            db.SaveChanges();

            db.EntityframeworkData.OrderBy(item => item.X).ToList().ForEach(item => Console.WriteLine(item.X));
            */
        }
    }
}
