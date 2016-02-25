using System.Threading;

namespace WorkingMvc6.Services
{
    public interface IGreeter
    {
        string GetGreeting();
    }

    public class Greeter : IGreeter
    {
        public Greeter()
        {
            Interlocked.Increment(ref InstanceCount);
        }

        public string GetGreeting()
        {
            return "Hello, from the greeter!";
        }

        public static int InstanceCount = 0;
    }
}