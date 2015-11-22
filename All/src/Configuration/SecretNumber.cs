using System.Threading;
using Microsoft.Extensions.Configuration;

namespace Configuration
{
    public interface ISecretNumber
    {
        int ComputeNumber();
    }

    public class SecretNumber : ISecretNumber
    {
        public SecretNumber(IConfiguration configuration)
        {
            _number = int.Parse(configuration["secret"]);
        }

        public int ComputeNumber()
        {
            return Interlocked.Increment(ref _number);
        }

        static int _number;
    }
}
