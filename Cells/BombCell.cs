// <copyright file="BombCell.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.Cells
{
    using System;

    /// <summary>
    /// Class that inherits Cell base class and represents bomb cell on the field
    /// </summary>
    public class BombCell : Cell
    {
        /// <summary>
        /// The minimum impact area size a bomb can have
        /// </summary>
        private static const int MinimalBombSize = 1;

        /// <summary>
        /// The maximum impact area size a bomb can have
        /// </summary>
        private static const int MaximumBombSize = 5;

        /// <summary>
        /// Private variable used to set size of a BombCell, when a new BombCell is created
        /// </summary>
        private readonly int bombSize;

        /// <summary>
        /// Initializes a new instance of the BombCell class
        /// </summary>
        /// <param name="bombSize">The size of a bomb by given integer value</param>
        public BombCell(int bombSize) 
            : base(CellType.Bomb)
        {
            if (bombSize < MinimalBombSize || bombSize > MaximumBombSize)
            {
                throw new ArgumentOutOfRangeException("Bomb sie must be between 1 and 5.");
            }

            this.bombSize = bombSize;
        }

        /// <summary>
        /// Gets the current size of a BombCell
        /// </summary>
        public int BombSize
        {
            get { return this.bombSize; }
        }

        // maybe it should be implemented in  the parent class
        public override ICell Clone()
        {
            throw new NotImplementedException();
        }
    }
}
