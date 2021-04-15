using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace HelloWorldPostgreSQL.EF
{
    [Table("entityframework_data")]
    public class EFData
    {
        [Key][Column("x")]
        public string X { get; set; }
    }

    public class HelloWorldContext : DbContext
    {
        public DbSet<EFData> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder();
            builder.ApplicationName = "HelloWorldPostgreSQL.EF";
            builder.Database = "postgres";
            builder.Host = "localhost";
            builder.Port = 5432;
            builder.Username = "postgres";
            // Console.WriteLine(builder.ToString());

            optionsBuilder.UseNpgsql(builder.ToString());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // https://docs.microsoft.com/de-de/ef/core/get-started/?tabs=visual-studio
            // PM Console
            // Add-Migration InitialCreate
            // Update-Database

            using HelloWorldContext db = new HelloWorldContext();

            // don't use in combination with migration scripts! 
            // db.Database.EnsureCreated();

            db.Add(new EFData() {X = "Hello EF 2021"});
            db.SaveChanges();

            db.Items.OrderBy(item => item.X).ToList().ForEach(item => Console.WriteLine(item.X));
        }
    }
}
