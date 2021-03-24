using MVC_Walker.View;

namespace MVC_Walker
{
    class Program
    {
        public static MainView View { get; set; }
        static void Main(string[] args)
        {
            View = new MainView();
            View.Start();
        }
    }
}
