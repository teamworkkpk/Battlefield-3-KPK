using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleFiled
{
    //use this random generator for every random number generation you need
    //extend the class if needed
   public static class RandomGenerator
    {
        private static Random random = new Random();

       public static int GetRandomNumber(int min, int max)
       {
           return random.Next(min, max);
       }
    }
}
