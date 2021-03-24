using MVC_Walker.Model;
using System;

namespace MVC_Walker.Controller
{
    public class MainController : IController
    {
        public BoyModel BoyModel { get; }

        public MainController()
        {
            this.BoyModel = new BoyModel();
        }

        public void MoveLeft()
        {
            BoyModel.Position = Math.Max(0, BoyModel.Position - 1);
        }

        public void MoveRight()
        {
            BoyModel.Position = Math.Min(10, BoyModel.Position + 1);
        }
    }
}
