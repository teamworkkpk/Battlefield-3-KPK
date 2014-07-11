namespace BattleFiled.Cells
{
    using System;

    internal static class CellFactory
    {
        /// <summary>
        /// Checks if cell of CellType is already created and returns it's reference. If not created, 
        /// creates new cell of type CellType and adds it to the dictionary
        /// </summary>
        /// <param name="cellType">CellTypes cellType</param>
        /// <returns>ICell cell</returns>
        public static ICell CreateCell(CellType cellType)
        {
            // Uses "lazy initialization"
            ICell cell;

            //Check if cell of CellType is already created and if it's not of type CellType.Bomb
            //if already created makes it point to the reference of the already created cell
            //if not created new Cell of type CellType
                switch (cellType)
                {
                    case CellType.EmptyCell:
                        cell = new EmptyCell();
                        break;
                    case CellType.Bomb:
                        int bombSize = RandomGenerator.GetRandomNumber(1, 6);
                        cell = new BombCell(bombSize);
                        break;

                    case CellType.BlownCell:
                        cell = new BlownCell();
                        break;
                    default:
                        throw new NotImplementedException();
                }

            return cell;
        }
    }
}
