using MVP_Walker.Controller;
using MVP_Walker.Model;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MVP_Walker.View
{
    class MainView : IView
    {
        IPresenter _presenter;

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
            _presenter = new MainPresenter(this); // rethink abstraction if instance-creation happens in View
        }

        public void Start()
        {
            _presenter.Start();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    _presenter.MoveLeft();
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    _presenter.MoveRight();
                }

            } while (key.KeyChar != 'q');
        }

        public void Draw(Direction direction, int position)
        {
            Console.Clear();
            StringReader reader = new StringReader(boyRunning);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.Write("".PadLeft(position, ' '));
                if (direction == Direction.Right)
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
