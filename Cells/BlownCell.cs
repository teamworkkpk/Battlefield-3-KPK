// <copyright file="BlownCell.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.Cells
{
    using System;

    /// <summary>
    /// Class that inherits Cell base class and represents destroyed cell on the field
    /// </summary>
    internal class BlownCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the BlownCell class
        /// </summary>
        public BlownCell() : base(CellType.BlownCell)
        {
        }

        // maybe should be implemented in the parent class
        public override ICell Clone()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}