// <copyright file="ConsoleView.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled
{
    using System;
    using System.Linq;

    /// <summary>
    /// Pointer object that will be used by the player for navigating around the field.
    /// </summary>
    public class Pointer
    {
        private int x;
        private int y;

        public Pointer(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Gets and sets coordinate alone X axis
        /// </summary>
        public int X
        {
            get
            {
                return this.x;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Pointer x coordinate cannot be less than 0");
                }

                this.x = value;
            }
        }

        /// <summary>
        /// Gets and sets along Y axis
        /// </summary>
        public int Y
        {
            get
            {
                return this.y;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Pointer y coordinate cannot be less than 0");
                }

                this.y = value;
            }
        }
    }
}
