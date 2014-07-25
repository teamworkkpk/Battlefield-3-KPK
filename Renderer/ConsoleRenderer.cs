namespace BattleFiled.Renderer
{
    using System;
    using BattleFiled.CellViews;
    using BattleFiled.Cells;
    using BattleFiled.GameEngine;

    public class ConsoleRenderer : Renderer
    {
        private const int ConsolePadding = 5;
        
        public ConsoleRenderer(Engine engine) : base(engine)
        {
            Console.CursorVisible = false;
        }

        public override void DrawGameOver(int totalMoves)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Game over, score {0}", totalMoves);
        }

        public override void DrawAll()
        {
            Console.Clear();
            base.DrawAll();
        }

        protected override void DrawPointer()
        {
            Console.SetCursorPosition(this.engine.Pointer.X + ConsolePadding, this.engine.Pointer.Y + ConsolePadding);
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Yellow;

            char symbol = (char)engine.PlayField[this.engine.Pointer.X, this.engine.Pointer.Y].CellView;

            if (symbol != '0')
            {
                Console.Write(symbol);
            }
            else
            {
                Console.Write(" ");
            }

            Console.ResetColor();
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