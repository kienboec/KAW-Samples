using MVC_Walker.Model;

namespace MVC_Walker.Controller
{
    public interface IController
    {
        BoyModel BoyModel { get; }
        void MoveLeft();
        void MoveRight();
    }
}
