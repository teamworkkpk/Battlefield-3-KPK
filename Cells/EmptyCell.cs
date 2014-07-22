// <copyright file="EmptyCell.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.Cells
{
    using System;

    /// <summary>
    /// Class that inherits Cell base class and represents empty cell on the field.
    /// </summary>
    public class EmptyCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the EmptyCell class.
        /// Empty constructor is done for the needs of XML serialization.
        /// </summary>         
        public EmptyCell() : base(CellType.EmptyCell)
        {
        }

        /// <summary>
        /// Makes deep copy of the object.
        /// </summary>
        /// <returns>Returns EmptyCell.</returns>
        public override ICell Clone()
        {            
            return new EmptyCell()
            {
                CellView = this.CellView,
                Color = this.Color,
                X = this.X,
                Y = this.Y
            };
        }
    }
}