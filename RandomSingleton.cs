using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    internal static class RandomSingleton
    {
        private static Random _instance;
        public static Random GetInstance() => _instance ?? (_instance = new Random());
    }
}
