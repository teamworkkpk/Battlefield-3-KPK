namespace BattleFiled.Cells
{
    using System;

    public class EmptyCell : Cell
    {
        public EmptyCell() : base(CellType.EmptyCell)
        {
        }

        public override ICell Clone()
        {            
            return new EmptyCell()
            {
                CellView = this.CellView,
                Color = this.Color,
                X = this.X,
                Y = this.Y
            };
        }
    }
}