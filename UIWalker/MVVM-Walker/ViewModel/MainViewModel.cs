using MVVM_Walker.Model;
using MVVM_Walker.View;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MVVM_Walker.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
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

        private BoyModel _model;

        public ICommand LeftCommand { get; }
        public ICommand RightCommand { get; }

        public string Data
        {
            get
            {
                // consider caching the results

                StringBuilder builder = new StringBuilder();
                StringReader reader = new StringReader(boyRunning);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    builder.Append("".PadLeft(_model.Position, ' '));
                    if (_model.Direction == Direction.Right)
                    {
                        builder.AppendLine(line);
                    }
                    else
                    {
                        builder.AppendLine(CalculateReverseLine(line));
                    }
                }

                return builder.ToString();
            }

        }

        public MainViewModel()
        {
            _model = new BoyModel();
            _model.Changed += (sender, args) => OnPropertyChanged(nameof(Data));

            RightCommand = new RelayCommand(() =>
            {
                _model.Position = Math.Min(10, _model.Position + 1);
            });
            LeftCommand = new RelayCommand(() =>
            {
                _model.Position = Math.Max(0, _model.Position - 1);
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
