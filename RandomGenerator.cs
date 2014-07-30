// <copyright file="ConsoleView.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled
{
    using System;
    using System.Linq;

    /// <summary>
    /// Static class used to generate random numbers
    /// </summary>    
   public static class RandomGenerator
    {
        private static Random random = new Random();

        /// <summary>
        /// Returns random number
        /// </summary>    
       public static int GetRandomNumber(int min, int max)
       {
           return random.Next(min, max);
       }
    }
}
