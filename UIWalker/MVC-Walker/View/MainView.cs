using MVC_Walker.Controller;
using MVC_Walker.Model;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MVC_Walker.View
{
    class MainView
    {
        IController _controller;

        private const string boyRunning =
@"                   >
        \\\\       >
        \o .(      >
         \ _/      >
      ___/(  /(    >
     /--/ \\//     >
 __ )/ /\/ \/      >
`-.\  //\\         >
   \\//  \\        >
    \/    \\       >
           \\      >
           '--`    >
                   >";

        public MainView()
        {
            _controller = new MainController(); // get as Dependency from outside?
            _controller.BoyModel.Changed += (sender, args) => { Draw(); };
        }

        public void Start()
        {
            Draw();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    _controller.MoveLeft();
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    _controller.MoveRight();
                }

            } while (key.KeyChar != 'q');
        }

        private void Draw()
        {
            Console.Clear();
            StringReader reader = new StringReader(boyRunning);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.Write("".PadLeft(this._controller.BoyModel.Position, ' '));
                if (_controller.BoyModel.Direction == Direction.Right)
                {
                    Console.WriteLine(line);
                }
                else
                {
                    Console.WriteLine(CalculateReverseLine(line));
                }
            }
        }

        private string CalculateReverseLine(string line)
        {
            var reversedString = new string(line.Reverse().ToArray());

            StringBuilder builder = new StringBuilder();
            foreach (char c in reversedString)
            {
                switch (c)
                {
                    case '\\':
                        builder.Append('/');
                        break;
                    case '/':
                        builder.Append('\\');
                        break;
                    case '(':
                        builder.Append(')');
                        break;
                    case ')':
                        builder.Append('(');
                        break;
                    case '`':
                        builder.Append('´');
                        break;
                    case '´':
                        builder.Append('`');
                        break;
                    case '<':
                        builder.Append('>');
                        break;
                    case '>':
                        builder.Append('<');
                        break;
                    default:
                        builder.Append(c);
                        break;
                }
            }

            return builder.ToString();
        }
    }
}
