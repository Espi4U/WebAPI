using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public static class RandomHelper
    {
        private static readonly Random _random;

        static RandomHelper()
        {
            _random = new Random();
        }

        public static int GetRandomPINCode()
        {
            return _random.Next(0000, 9999);
        }
    }
}
