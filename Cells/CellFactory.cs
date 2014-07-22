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
                        int bombSize = RandomGenerator.GetRandomNumber(1, 6);
                        cell = new BombCell(bombSize);
                        cell.CellView = (CellView)bombSize + 48;
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
