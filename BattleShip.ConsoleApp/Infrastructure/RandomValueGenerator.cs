using Battleship.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship.Infrastructure
{
    public class RandomValueGenerator : IRandomValueGenerator
    {
        private Random _random;

        public RandomValueGenerator()
        {
            _random = new Random();
        }

        public bool GetRandomBool()
        {
            return _random.Next(0, 2) == 0;
        }

        public int GetRandomIntValue(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue + 1);
        }
    }
}
