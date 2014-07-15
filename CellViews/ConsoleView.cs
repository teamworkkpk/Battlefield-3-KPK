
namespace BattleFiled.CellViews
{
    using System;
    using BattleFiled.Renderer.Context;

    class ConsoleView:ICellView
    {
        private const int ConsolePadding = 5;
        public ConsoleView(int x, int y, ConsoleColor foreground, ConsoleColor background, char symbol)
        {
            this.X = x;
            this.Y = y;
            this.Foreground = foreground;
            this.Background = background;
            this.Symbol = symbol;
        }
        
        public int X { get; private set; }
        public int Y { get; private set; }
        public ConsoleColor Foreground { get; set; }
        public ConsoleColor Background { get; set; }
        public char Symbol { get; set; }
        
        public void Draw(RenderingContext context)
        {
           // Console.Write("front");
            Console.ResetColor();
            Console.SetCursorPosition(this.X + ConsolePadding, this.Y + ConsolePadding);
            Console.ForegroundColor = this.Foreground;
            Console.BackgroundColor = this.Background;
            Console.Write(this.Symbol);
            Console.ResetColor();
        }
    }
}
