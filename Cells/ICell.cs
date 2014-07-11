namespace BattleFiled.Cells
{
    using System;
    using BattleFiled;

    public interface ICell
    {
        CellType CellType { get; }

        CellView CellView { get; set; }

        int X { get; set; }

        int Y { get; set; }

        Color Color { get; set; }

        ICell Clone();
    }
}
