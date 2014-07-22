// <copyright file="BombCell.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.Cells
{
    using System;

    /// <summary>
    /// Class that inherits Cell base class and represents bomb cell on the field.
    /// </summary>
    public class BombCell : Cell
    {
        /// <summary>
        /// The minimum impact area size a bomb can have.
        /// </summary>
        private const int MinimalBombSize = 1;

        /// <summary>
        /// The maximum impact area size a bomb can have.
        /// </summary>
        private const int MaximumBombSize = 5;

        /// <summary>
        /// Private variable used to set size of a BombCell, when a new BombCell is created.
        /// </summary>
        private int bombSize;

        /// <summary>
        /// Initializes a new instance of the BombCell class.
        /// Empty constructor is done for the needs of XML serialization.
        /// </summary>            
        public BombCell()
            : base(CellType.Bomb)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BombCell class.
        /// Empty constructor is done for the needs of XML serialization.
        /// </summary>
        /// <param name="bombSize">The size of a bomb by given integer value.</param>        
        public BombCell(int bombSize) 
            : base(CellType.Bomb)
        {
            this.BombSize = bombSize;
        }

        /// <summary>
        /// Gets or sets the current size of a BombCell. 
        /// </summary>        
        /// <value>The bomb size.</value>
        public int BombSize
        {
            get 
            { 
                return this.bombSize;
            }     
       
            set
            {
                if (value < MinimalBombSize || value > MaximumBombSize)
                {
                    throw new ArgumentOutOfRangeException("Bomb sie must be between 1 and 5.");
                }

                this.bombSize = value;
            }
        }

        /// <summary>
        /// Makes deep copy of the object.
        /// </summary>
        /// <returns>Returns BombCell.</returns>
        public override ICell Clone()
        {            
            return new BombCell(this.BombSize)
            {
                CellView = this.CellView,
                Color = this.Color,                
                X = this.X,
                Y = this.Y
            };
        }
    }
}
