using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KAW2SwearJar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwearJarController : ControllerBase
    {
        private static readonly Dictionary<string, int> SwearJarBalance = new Dictionary<string, int>();

        // GET: api/<SwearJarController>
        [HttpGet]
        public Dictionary<string, int> Get()
        {
            return SwearJarBalance;
        }

        // GET api/<SwearJarController>/5
        [HttpGet("{username}")]
        public int Get(string username)
        {
            return SwearJarBalance.ContainsKey(username) 
                ? SwearJarBalance[username] 
                : 0;
        }

        // POST api/<SwearJarController>
        [HttpPost("{username}")]
        public int Post(string username)
        {
            if (!SwearJarBalance.ContainsKey(username))
            {
                SwearJarBalance.Add(username, 0);
            }

            SwearJarBalance[username] = SwearJarBalance[username] + 1;
            return SwearJarBalance[username];
        }

        // DELETE api/<SwearJarController>/5
        [HttpDelete("{username}")]
        public void Delete(string username)
        {
            SwearJarBalance.Remove(username);
        }
    }
}
