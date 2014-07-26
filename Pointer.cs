namespace BattleFiled
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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
