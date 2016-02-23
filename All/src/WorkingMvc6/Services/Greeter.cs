namespace WorkingMvc6.Services
{
    public interface IGreeter
    {
        string GetGreeting();
    }

    public class Greeter : IGreeter
    {
        public string GetGreeting()
        {
            return "Hello, from the greeter!";
        }
    }
}