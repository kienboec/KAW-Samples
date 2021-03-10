namespace EventsLister
{
    class Program
    {
        public static void Main(string[] args) =>
            // write to console 
            // Program1.MainAsync(args).GetAwaiter().GetResult();

            // write with date
            // Program2.MainAsync(args).GetAwaiter().GetResult();

            // count headline
            // Program3.MainAsync(args).GetAwaiter().GetResult();

            // filtering
            // Program4.MainAsync(new string[] { "Info" }).GetAwaiter().GetResult();

            // interactive
            Program5.MainAsync(args).GetAwaiter().GetResult();
    }
}