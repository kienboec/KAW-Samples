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
        private static Dictionary<string, int> _swearJarBalance = new Dictionary<string, int>();

        // GET: api/<SwearJarController>
        [HttpGet]
        public Dictionary<string, int> Get()
        {
            return _swearJarBalance;
        }

        // GET api/<SwearJarController>/5
        [HttpGet("{username}")]
        public int Get(string username)
        {
            return _swearJarBalance[username];
        }

        // POST api/<SwearJarController>
        [HttpPost("{username}")]
        public void Post(string username)
        {
            if (!_swearJarBalance.ContainsKey(username))
            {
                _swearJarBalance.Add(username, 0);
            }

            _swearJarBalance[username] = _swearJarBalance[username] + 1;
        }

        // DELETE api/<SwearJarController>/5
        [HttpDelete("{username}")]
        public void Delete(string username)
        {
            _swearJarBalance.Remove(username);
        }
    }
}
