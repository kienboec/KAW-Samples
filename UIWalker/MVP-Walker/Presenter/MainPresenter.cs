using MVP_Walker.Model;
using MVP_Walker.View;
using System;

namespace MVP_Walker.Controller
{
    public class MainPresenter : IPresenter
    {
        public IView View { get; }
        private BoyModel BoyModel { get; }

        public MainPresenter(IView view)
        {
            this.BoyModel = new BoyModel();
            this.View = view;

            this.BoyModel.Changed += (sender, args) => this.CallDraw();
        }

        public void Start()
        {
            CallDraw();
        }

        public void MoveLeft()
        {
            BoyModel.Position = Math.Max(0, BoyModel.Position - 1);
        }

        public void MoveRight()
        {
            BoyModel.Position = Math.Min(10, BoyModel.Position + 1);
        }

        private void CallDraw()
        {
            View.Draw(this.BoyModel.Direction, this.BoyModel.Position);
        }
    }
}
