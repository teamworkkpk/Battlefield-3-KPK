using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleFiled
{
    internal class CellFactory
    {

        private readonly Dictionary<CellTypes, ICell> cellTypesCollection = new Dictionary<CellTypes, ICell>();

        public ICell GetCell(CellTypes cellType)
        {
            // Uses "lazy initialization"
            ICell cell = null;
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
