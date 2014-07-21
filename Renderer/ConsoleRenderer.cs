namespace BattleFiled.Renderer
{
    using System;
    using BattleFiled.CellViews;
    using BattleFiled.Cells;
    using BattleFiled.GameEngine;

    public class ConsoleRenderer : Renderer
    {
        public ConsoleRenderer(Engine engine) : base(engine)
        {
        }
        
        protected override ICellView CreateCellView(ICell cell, bool isBackgroundChanged)
        {
            ICellView view;
            switch (cell.CellType)
            { 
                case CellType.Bomb:
                    view = new ConsoleView(cell.X, cell.Y, ConsoleColor.Red, isBackgroundChanged?ConsoleColor.Green:ConsoleColor.Blue, (char)cell.CellView);
                    break;
                case CellType.BlownCell:
                    view = new ConsoleView(cell.X, cell.Y, ConsoleColor.Red, ConsoleColor.Gray, '*');
                    break;
                case CellType.EmptyCell:
                    view = new ConsoleView(cell.X, cell.Y, ConsoleColor.Cyan, isBackgroundChanged ? ConsoleColor.Green : ConsoleColor.Blue, ' ');
                    break;
                default:
                    throw new NotImplementedException();
            }

            return view;
        }
    }
}