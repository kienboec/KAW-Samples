using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAW1MaintenanceMonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private static MaintenanceMessageManager _manager = new MaintenanceMessageManager();
       

        [HttpGet]
        public string GetMaintenanceMessage() => _manager.GetMaintenanceMessage();

        [HttpPost]
        public string SetMaintenanceMessage([FromBody] string maintenanceMessage) => _manager.SetMaintenanceMessage(maintenanceMessage);


        [HttpDelete]
        public string ResetMaintenanceMessage() => _manager.ResetMaintenanceMessage();

    }
}
