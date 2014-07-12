// <copyright file="Cell.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.Cells
{
    using System;
    using BattleFiled;

    /// <summary>
    /// Base abstract class for all Cell object
    /// </summary>
    public abstract class Cell : ICell
    {
        /// <summary>
        /// Private variable used to set the cell type of a new Cell, when a new Cell is created
        /// </summary>
        private readonly CellType cellType;

        /// <summary>
        /// Private variable used to set X coordinate of a new Cell, when a new Cell is created
        /// </summary>
        private int positionX;

        /// <summary>
        /// Private variable used to set Y coordinate of a new Cell, when a new Cell is created
        /// </summary>
        private int positionY;

        /// <summary>
        /// Initializes a new instance of the Cell class
        /// </summary>
        /// <param name="celltype">The type of the cell by given CellType</param>
        protected Cell(CellType celltype)
        {
            this.cellType = celltype;
        }
        
        /// <summary>
        /// Gets the CellType of a Cell
        /// </summary>
        public CellType CellType
        {
           get { return this.cellType; }
        }

        /// <summary>
        /// Gets or sets the CellView of a Cell
        /// </summary>
        public CellView CellView
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the X coordinate of a Cell
        /// </summary>
        public int X
        {
            get
            {
                return this.positionX;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Cell's X position is always a positive number or 0.");
                }

                this.positionX = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y coordinate of a Cell
        /// </summary>
        public int Y
        {
            get
            {
                return this.positionY;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Cell's Y position is always a positive number or 0.");
                }

                this.positionY = value;
            }
        }

        /// <summary>
        /// Gets or sets the Color of a Cell
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Creates a deep copy of a Cell
        /// </summary>
        /// <returns>Returns cloned ICell</returns>
        public abstract ICell Clone();

        /// <summary>
        /// Creates a string of the Cell object
        /// </summary>
        /// <returns>Returns the string of the Cell object</returns>
        public override string ToString()
        {
            switch (this.Color)
            {
                case Color.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Color.White:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Color.Magenda:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                default:
                    throw new ArgumentException("Ivalid color");
            }
            //// edit to cast char instead of int, so the symbol is returned
            return " " + ((char)this.CellView).ToString() + " ";
        }
    }
}
