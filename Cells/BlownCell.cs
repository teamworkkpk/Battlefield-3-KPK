namespace BattleFiled.Cells
{
    using System;

    internal class BlownCell : Cell
    {
        public BlownCell() : base(CellType.BlownCell)
        {
        }

        public override ICell Clone()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}