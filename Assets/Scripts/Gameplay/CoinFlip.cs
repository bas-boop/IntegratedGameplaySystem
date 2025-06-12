using System;

namespace Gameplay
{
    public static class CoinFlip
    {
        private static readonly Random rng = new Random();

        public static bool Flip() => rng.Next(0, 2) == 0;
    }
}