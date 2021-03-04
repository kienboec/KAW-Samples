namespace EventsLister.CBP
{
    public interface IIOHandler
    {
        string ReadLine();
        void Write(string text);
        void WriteLine(string text);
    }
}