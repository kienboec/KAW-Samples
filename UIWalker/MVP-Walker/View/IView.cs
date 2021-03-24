using MVP_Walker.Model;

namespace MVP_Walker.View
{
    public interface IView
    {
        void Start();
        void Draw(Direction direction, int position);

    }
}
