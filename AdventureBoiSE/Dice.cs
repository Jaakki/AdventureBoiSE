using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureBoiSE
{
    public class Dice
    {
        public static int RandomInt(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static int RandomDice()
        {
            Random random = new Random();
            return random.Next(1,7);
        }
    }
}
