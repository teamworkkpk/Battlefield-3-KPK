using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleFiled
{
    internal class CellFactory
    {
        //the dictionary will hold key-values for every cell type
        //if cell type is already created it will just return a reference to the already created cell
        //this will work only for empy or blown cells, because with flyweight is not possible to have different bombs with different bom values
        private readonly Dictionary<CellTypes, ICell> cellTypesCollection = new Dictionary<CellTypes, ICell>();

        /// <summary>
        /// Checks if cell of CellType is already created and returns it's reference. If not created, 
        /// creates new cell of type CellType and adds it to the dictionary
        /// </summary>
        /// <param name="cellType">CellTypes cellType</param>
        /// <returns>ICell cell</returns>
        public ICell GetCell(CellTypes cellType)
        {
            // Uses "lazy initialization"
            ICell cell = null;

            //Check if cell of CellType is already created and if it's not of type CellType.Bomb
            //if already created makes it point to the reference of the already created cell
            //if not created new Cell of type CellType
            if (this.cellTypesCollection.ContainsKey(cellType) && cellType != CellTypes.Bomb)
            {
                cell = this.cellTypesCollection[cellType];
            }
            else
            {
                switch (cellType)
                {
                    case CellTypes.EmptyCell:
                        cell = new Cell(CellTypes.EmptyCell);
                        this.cellTypesCollection.Add(cellType, cell);
                        break;
                    case CellTypes.Bomb:
                        cell = new Cell(CellTypes.Bomb);
                        break;

                    case CellTypes.BlownCell:
                        cell = new Cell(CellTypes.BlownCell);
                        this.cellTypesCollection.Add(cellType, cell);
                        break;
                }

            }

            return cell;
        }
    }
}
