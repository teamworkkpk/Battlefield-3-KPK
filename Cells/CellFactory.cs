// <copyright file="CellFactory.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.Cells
{
    using System;

    /// <summary>
    /// Creates and returns new Cell depending on CellType.
    /// </summary>
    internal static class CellFactory
    {
        /// <summary>
        /// Checks if cell of CellType is already created and returns it's reference, If not created,
        /// creates new cell of type CellType and adds it to the dictionary.
        /// </summary>
        /// <param name="cellType">CellTypes cellType.</param>
        /// <returns>ICell cell.</returns>
        /// <summary>
        /// Constant offset for ASCII code that is used for cell rendering.
        /// </summary>
        private const int ASCII_VIEW_OFFSET = 48;

        /// <summary>
        /// Constant for min bomb size.
        /// </summary>
        private const int MIN_BOMB_SIZE = 1;

        /// <summary>
        /// Constant for min bomb size.
        /// </summary>
        private const int MAX_BOMB_SIZE = 6;

        /// <summary>
        /// Cell factory implementation.
        /// </summary>
        /// <param name="cellType">Cell type.</param>
        /// <returns>Required cell.</returns>
        public static ICell CreateCell(CellType cellType)
        {
            //// Uses "lazy initialization".
            ICell cell;

                switch (cellType)
                {
                    case CellType.EmptyCell:
                        cell = new EmptyCell();
                        cell.CellView = CellView.Empty;
                        break;

                    case CellType.Bomb:
                        int bombSize = RandomGenerator.GetRandomNumber(MIN_BOMB_SIZE, MAX_BOMB_SIZE);
                        cell = new BombCell(bombSize);
                        cell.CellView = (CellView)bombSize + ASCII_VIEW_OFFSET;
                        break;

                    case CellType.BlownCell:
                        cell = new BlownCell(); 
                        cell.CellView = CellView.Blown;
                        break;

                    default:
                        throw new ArgumentException("Invalid cell type give to the cell factory");
                }

            return cell;
        }
    }
}
