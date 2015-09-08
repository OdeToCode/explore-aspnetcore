using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Framework.Configuration;

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
            _number = int.Parse(configuration.Get("secret"));
        }

        public int ComputeNumber()
        {
            return Interlocked.Increment(ref _number);
        }

        static int _number;
    }
}
