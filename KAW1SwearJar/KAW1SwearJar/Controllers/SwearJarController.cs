using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KAW1SwearJar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwearJarController : ControllerBase
    {
        private static Dictionary<string, int> swearJarData = new Dictionary<string, int>();

        [HttpGet]
        public Dictionary<string, int> GetFullSwearJarData()
        {
            return swearJarData;
        }

        [HttpGet("{username}")]
        public int GetSwearJarDataForUser(string username)
        {
            try
            {
                return swearJarData.ContainsKey(username) ? swearJarData[username] : 0;
            }
            catch (Exception exc)
            {
                return 0;
            }
        }

        [HttpPost("{username}")]
        public int IncrementSwearJarBalanceForUser(string username)
        {
            if (!swearJarData.ContainsKey(username))
            {
                swearJarData.Add(username, 0);
            }

            swearJarData[username] = swearJarData[username] + 1;

            return swearJarData[username];
        }

        // DELETE api/<SwearJarController>/5
        [HttpDelete("{username}")]
        public int ResetSwearJarBalanceForUser(string username)
        {
            // first version
            // swearJarData.Remove(username);

            swearJarData[username] = 0;
            return swearJarData[username];
        }
    }
}
