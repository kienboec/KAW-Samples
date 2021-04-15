using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace KAW1MaintenanceMonitor
{
    // select "current_timestamp", coalesce(message, '<null>') from maintenance_messages;

    [Table("maintenance_messages")]
    public class MainteanceMessages
    {
        public MainteanceMessages(string message)
        {
            this.Message = message;
            CurrentTimeStamp = DateTime.Now;
        }

        [Key]
        [Column("current_timestamp")]
        public DateTime CurrentTimeStamp { get; set; }

        [Column("message")]
        public string Message { get; set; }
    }

    public class MaintenanceDBContext : DbContext
    {
        public DbSet<MainteanceMessages> MainteanceMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder();
            builder.ApplicationName = "HelloWorldPostgreSQL.EF";
            builder.Database = "postgres";
            builder.Host = "localhost";
            builder.Port = 5432;
            builder.Username = "postgres";
            // Console.WriteLine(builder.ToString());

            optionsBuilder.UseNpgsql(builder.ToString());
        }

        public string GetCurrentMessage()
        {
            return
                this.MainteanceMessages
                    .OrderByDescending(x => x.CurrentTimeStamp)
                    .FirstOrDefault()
                    ?.Message;
        }

        public void SetCurrentMessage(string message)
        {
            this.MainteanceMessages.Add(new MainteanceMessages(message));
            this.SaveChanges();
        }
    }
}
