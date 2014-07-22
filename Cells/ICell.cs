// <copyright file="ICell.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.Cells
{
    using System;
    using BattleFiled;

    /// <summary>
    /// Interface that sets the main properties and method used by the cell classes.
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// Gets the cell type.
        /// </summary>
        /// <value>CellType value.</value>
        CellType CellType { get; }

        /// <summary>
        /// Gets or sets the cell view.
        /// </summary>
        /// <value>CellView view.</value>
        CellView CellView { get; set; }

        /// <summary>
        /// Gets or sets cell X position.
        /// </summary>
        /// <value>Cell position.</value>
        int X { get; set; }

        /// <summary>
        /// Gets or sets cell Y position.
        /// </summary>
        /// <value>Cell position.</value>
        int Y { get; set; }

        /// <summary>
        /// Gets or sets cell color.
        /// </summary>
        /// <value>Cell color.</value>
        Color Color { get; set; }

        /// <summary>
        /// Makes deep copy of the object.
        /// </summary>        
        /// <returns>ICell object.</returns>
        ICell Clone();
    }
}
