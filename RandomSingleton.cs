using System;

namespace Battleships
{
    internal static class RandomSingleton
    {
        private static Random _instance;
        public static Random GetInstance() => _instance ?? (_instance = new Random());
    }
}
