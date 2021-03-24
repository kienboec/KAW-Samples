using MVP_Walker.View;

namespace MVP_Walker
{
    class Program
    {
        public static IView View { get; set; }
        static void Main(string[] args)
        {
            View = new MainView();
            View.Start();
        }
    }
}
