namespace BattleFiled.Cells
{
    using System;

    internal class EmptyCell : Cell
    {
        public EmptyCell() : base(CellType.EmptyCell)
        {
        }

        public override ICell Clone()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}