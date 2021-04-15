using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KAW2MaintenanceMonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private static readonly MaintenanceContext _db = new MaintenanceContext();

        public MaintenanceController()
        {
            _db.Database.EnsureCreated();
        }

        [HttpGet]
        public string GetMaintenanceMessage()
        {
            var maintenanceMessage = _db.GetCurrentMessage();
            if (string.IsNullOrWhiteSpace(maintenanceMessage))
            {
                return "OK : everything is awesome...";
            }

            return $"ERR: {maintenanceMessage}";
        }

        [HttpPost]
        public void Post([FromBody] string maintenanceMessage)
        {
            _db.AddNewMessage(maintenanceMessage);
        }

        [HttpDelete]
        public void Delete()
        {
            _db.AddNewMessage(null);
        }
    }
}
