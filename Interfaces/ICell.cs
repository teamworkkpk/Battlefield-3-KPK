namespace BattleFiled
{
    using System;
    using System.Collections.Generic;

    public interface ICell
    {
        CellTypes CellType { get; set; }

        CellView CellView { get; set; }

        int X { get; set; }

        int Y { get; set; }

        Color Color { get; set; }

        ICell Clone();
    }
}
