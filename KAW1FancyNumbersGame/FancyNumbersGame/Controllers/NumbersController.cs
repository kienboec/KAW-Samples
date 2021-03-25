using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FancyNumbersGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumbersController : ControllerBase
    {
        private static int _secretNumber = 0;
        private static Dictionary<string, int> _guesses = new Dictionary<string, int>();

        // POST api/<NumbersController>
        [HttpPost]
        public void GenerateNewNumber()
        {
            var random = new Random(DateTime.Now.Millisecond);
            _secretNumber = random.Next(0, 100);
            _guesses.Clear();
        }

        // POST api/<NumbersController>
        [HttpPost("{username}")]
        public void AddGuess(string username, [FromBody] int guess)
        {
            _guesses[username] = guess;
        }

        [HttpGet]
        public string DisplayWinner() // TODO: rename
        {
            KeyValuePair<string, int>? winner = null;
            foreach (var guess in _guesses)
            {
                if (winner == null || Math.Abs(guess.Value - _secretNumber) < Math.Abs(winner.Value.Value - _secretNumber))
                {
                    winner = guess;
                }
            }

            // return winner; // writes object as json
            return $"{(winner?.Key) ?? "-"}: {(winner?.Value ?? 0).ToString()} ({_secretNumber})";
        }
    }
}
