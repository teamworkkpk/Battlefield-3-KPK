// <copyright file="ConsoleRenderer.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.Renderer
{
    using System;
    using BattleFiled.Cells;
    using BattleFiled.CellViews;
    using BattleFiled.GameEngine;

    /// <summary>
    /// A concrete implementation of <see cref="Renderer"/> abstract class that uses the system's console
    /// as a rendering environment.
    /// </summary>
    public class ConsoleRenderer : Renderer
    {
        /// <summary>
        /// The numbers of symbols to be applied as padding on top/left sides of playing field.
        /// </summary>
        private const int ConsolePadding = 5;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleRenderer" /> class.
        /// </summary>
        /// <param name="engine">The engine object to be attached to this object instance.</param>
        public ConsoleRenderer(Engine engine) : base(engine)
        {
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Initiates a procedure defined by concrete classes for drawing a game over screen.
        /// </summary>
        /// <param name="totalMoves">The total moves a player has made up to this moment.</param>
        public override void DrawGameOver(int totalMoves)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Game over, score {0}", totalMoves);
        }

        /// <summary>
        /// Initiates a draw procedure on controlled <see cref="ICellView" /> objects.
        /// </summary>
        public override void DrawAll()
        {
            Console.Clear();
            base.DrawAll();
        }

        /// <summary>
        /// Concrete implementations should
        /// implement this method according to their rendering environment.
        /// </summary>
        protected override void DrawPointer()
        {
            Console.SetCursorPosition(this.Engine.Pointer.X + ConsolePadding, this.Engine.Pointer.Y + ConsolePadding);
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Yellow;

            char symbol = (char)Engine.PlayField[this.Engine.Pointer.X, this.Engine.Pointer.Y].CellView;

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

        /// <summary>
        /// A method for creating new <see cref="ICellView" /> derived classes. Concrete implementations should
        /// implement this method and return the proper type of object for their rendering environment.
        /// This method is used by <see cref="Renderer.CreateCellViews" /> to get the right type of object needed.
        /// </summary>
        /// <param name="cell">The <see cref="ICell" /> object that will get displayed by returned object.</param>
        /// <param name="isBackgroundChanged">A boolean.</param>
        /// <returns>CellView object to be added to Renderer's list of controlled objects.</returns>
        protected override ICellView CreateCellView(ICell cell, bool isBackgroundChanged)
        {
            ICellView view;
            switch (cell.CellType)
            { 
                case CellType.Bomb:
                    view = new ConsoleView(cell.X, cell.Y, ConsoleColor.Red, isBackgroundChanged ? ConsoleColor.Green : ConsoleColor.Blue, (char)cell.CellView);
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