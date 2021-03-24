using System;

namespace MVVM_Walker.Model
{
    public class BoyModel
    {
        private Direction _direction = Direction.Right;
        public Direction Direction
        {
            get
            {
                return _direction;
            }
        }

        private int _position = 0;
        public int Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (_position != value)
                {
                    _direction = value < _position ? Direction.Left : Direction.Right;
                    _position = value;
                    OnPropertyChanged();
                }
            }
        }

        public event EventHandler<EventArgs> Changed;

        public void OnPropertyChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }
    }
}
