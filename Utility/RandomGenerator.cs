using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    static class RandomGenerator
    {
        private static Random random;

        static RandomGenerator()
        {
            random = new Random();
        }

        public static int GetRandomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public static float GetRandomFloat()
        {
            return (float)random.NextDouble();
        }
    }
}
