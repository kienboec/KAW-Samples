using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAW1MaintenanceMonitor
{
    public class MaintenanceMessageManager
    {
        private static MaintenanceDBContext _db = new MaintenanceDBContext();
        
        public MaintenanceMessageManager()
        {
            _db.Database.EnsureCreated();
        }

        public string GetMaintenanceMessage()
        {
            var maintenanceMessage = _db.GetCurrentMessage();
            if (string.IsNullOrWhiteSpace(maintenanceMessage))
            {
                return "OK: not in maintenance mode currently";
            }

            return $"ERROR: {maintenanceMessage}";
        }

        public string SetMaintenanceMessage(string maintenanceMessage)
        {
            _db.SetCurrentMessage(maintenanceMessage);
            return GetMaintenanceMessage();
        }

        public string ResetMaintenanceMessage()
        {
            return SetMaintenanceMessage(null);
        }
    }
}
