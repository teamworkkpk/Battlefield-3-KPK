namespace BattleFiled.Renderer
{
    using System;
    using BattleFiled.CellViews;
    using BattleFiled.Cells;
    using BattleFiled.GameEngine;

    class ConsoleRenderer : Renderer
    {
        public ConsoleRenderer(Engine engine) : base(engine)
        {
        }
        
        protected override ICellView CreateCellView(ICell cell)
        {
            ICellView view;
            switch (cell.CellType)
            { 
                case CellType.Bomb:
                    view = new ConsoleView(cell.X, cell.Y, ConsoleColor.Red, ConsoleColor.Cyan, '@');
                    break;
                case CellType.BlownCell:
                    view = new ConsoleView(cell.X, cell.Y, ConsoleColor.DarkGray, ConsoleColor.Cyan, '*');
                    break;
                case CellType.EmptyCell:
                    view = new ConsoleView(cell.X, cell.Y, ConsoleColor.Cyan, ConsoleColor.Cyan, ' ');
                    break;
                default:
                    throw new NotImplementedException();
            }

            return view;
        }
    }
}