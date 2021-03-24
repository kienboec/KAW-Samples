using MVP_Walker.View;

namespace MVP_Walker.Controller
{
    public interface IPresenter
    {
        IView View { get; }
        void MoveLeft();
        void MoveRight();
        void Start();
    }
}
