using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KAW2MaintenanceMonitor
{
    // select "CurrentTimeStamp", coalesce("Message", '<null>') from "MaintenanceMessages";
    public class MaintenanceMessage
    {
        [Key]
        public DateTime CurrentTimeStamp { get; set; }
        public string Message { get; set; }

        public MaintenanceMessage(string message)
        {
            this.CurrentTimeStamp = DateTime.Now;
            this.Message = message;
        }
    }

    public class MaintenanceContext : DbContext
    {
        public DbSet<MaintenanceMessage> MaintenanceMessages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseNpgsql("Application Name=HelloWorldPostgreSQL.Dapper;Database=postgres;Host=localhost;Port=5432;Username=postgres");
        }

        public void AddNewMessage(string message)
        {
            this.MaintenanceMessages.Add(new MaintenanceMessage(message));
            this.SaveChanges();
        }

        public string GetCurrentMessage()
        {
            return this.MaintenanceMessages.OrderByDescending(x => x.CurrentTimeStamp).FirstOrDefault().Message;
        }
    }
}
