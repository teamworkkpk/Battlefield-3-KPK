// <copyright file="ConsoleView.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.CellViews
{
    using System;
    using BattleFiled.Renderer.Context;

    /// <summary>
    /// Class instance provides draw method for drawing all cell types on the console.
    /// </summary>
    public class ConsoleView : ICellView
    {
        /// <summary>
        /// Sets the console padding.
        /// </summary>
        private const int CONSOLE_PADDING = 5;

        /// <summary>
        /// Initializes a new instance of the ConsoleView class.
        /// </summary>
        /// <param name="x">Requests X cell position.</param>
        /// <param name="y">Requests Y cell position.</param>
        /// <param name="foreground">Requests cell foreground color.</param>
        /// <param name="background">Requests cell background color.</param>
        /// <param name="symbol">Requests cell draw symbol.</param>
        public ConsoleView(int x, int y, ConsoleColor foreground, ConsoleColor background, char symbol)
        {
            this.X = x;
            this.Y = y;
            this.Foreground = foreground;
            this.Background = background;
            this.Symbol = symbol;
        }

        /// <summary>
        /// Gets cell X position.
        /// </summary>
        /// <value>Cell position.</value>
        public int X { get; private set; }

        /// <summary>
        /// Gets cell Y position.
        /// </summary>
        /// <value>Cell position.</value>
        public int Y { get; private set; }

        /// <summary>
        /// Gets or sets console foreground color.
        /// </summary>
        /// <value>Console foreground color.</value>
        public ConsoleColor Foreground { get; set; }

        /// <summary>
        /// Gets or sets console background color.
        /// </summary>
        /// <value>Console background color.</value>
        public ConsoleColor Background { get; set; }

        /// <summary>
        /// Gets or sets cell symbol.
        /// </summary>
        /// <value>Cell char symbol.</value>
        public char Symbol { get; set; }

        /// <summary>
        /// Draws on the current cell on the console.
        /// </summary>
        public void Draw()
        {
            Console.ResetColor();
            Console.SetCursorPosition(this.X + CONSOLE_PADDING, this.Y + CONSOLE_PADDING);
            Console.ForegroundColor = this.Foreground;
            Console.BackgroundColor = this.Background;
            Console.Write(this.Symbol);
            Console.ResetColor();
        }
    }
}
