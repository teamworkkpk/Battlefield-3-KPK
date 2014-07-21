using System;
using BattleFiled;
using BattleFiled.Cells;
using BattleFiled.GameEngine;
using BattleFiled.CellViews;
using BattleFiled.Renderer.Context;

namespace BattleFiled.Renderer
{
    abstract class Renderer : RenderingContext
    {
        protected ICellView[,] cellViews;
        private Engine engine;
        private const int ConsolePadding = 5;

        public Renderer(Engine engine)
        {
            Console.CursorVisible = false;

            if (engine == null)
            {
                throw new ArgumentNullException("Null Engine provided.");
            }

            if (engine.PlayField != null)
            {
                this.engine = engine;
                this.cellViews = this.CreateCellViews(engine.PlayField);
                
                this.DrawAll();

            }

            //Register event handlers.
            this.engine.CurrentCellChanged += this.OnCurrentCellChangedHandler;
            this.engine.CellsInRegionChanged += this.OnCellsInRegionChangedHandler;
            this.engine.PlayfieldChanged += this.OnPlayfieldChangedHandler;
            this.engine.CellsInRegionRedefined += this.OnCellsInRegionRedefinedHandler;
            this.engine.CellRedefined += this.OnCellRedefinedHandler;
            this.engine.CellChanged += this.OnCellChangedHandler;
        }
  
        public void DrawAll()
        {
            Console.Clear();
            
            foreach (ICellView view in this.cellViews)
            {
                view.Draw(this);
            }

            DrawPointer();
        }

        public void DrawGameOver(int totalMoves)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Game over, score {0}", totalMoves);
        }

        protected abstract ICellView CreateCellView(ICell cell, bool shouldChangeColor);

        private void DrawPointer()
        {
            Console.SetCursorPosition(this.engine.Pointer.X + ConsolePadding, this.engine.Pointer.Y + ConsolePadding);
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Yellow;

            char symbol = (char)engine.PlayField[this.engine.Pointer.X, this.engine.Pointer.Y].CellView;

            if(symbol != '0')
            {
                Console.Write(symbol);
            }
            else
            {
                Console.Write(" ");
            }

            Console.ResetColor();
        }

        private void OnPlayfieldChangedHandler(object sender, PlayfieldChangedEventArgs e)
        {
            this.cellViews = this.CreateCellViews(e.NewPlayField);
            this.DrawAll();
        }

        private ICellView[,] CreateCellViews(Playfield playfield)
        { 
            int fieldSize = playfield.PlayfieldSize;
            bool shouldChangeColor = false;
            ICellView[,] cellViews = new ICellView[fieldSize, fieldSize];

            //foreach (ICell cell in playfield)
            //{
            //    Console.WriteLine("                   " + cell.X + " " + cell.Y);
            //    cellViews[cell.X-1, cell.Y] = CreateCellView(cell);
            //}

            for (int i = 0; i < playfield.PlayfieldSize; i++)
            {
                if (playfield.PlayfieldSize % 2 == 0)
                {
                    shouldChangeColor = !shouldChangeColor;
                }

                for (int j = 0; j < playfield.PlayfieldSize; j++)
                {
                    cellViews[i, j] = CreateCellView(playfield[i, j], shouldChangeColor);
                    shouldChangeColor = !shouldChangeColor;
                }
                
            }
            return cellViews;
        }

        private void OnCellChangedHandler(object sender, CellEventArgs e)
        {
            this.cellViews[e.Target.X, e.Target.Y].Draw(this);
        }
  
        private void OnCellRedefinedHandler(object sender, CellEventArgs e)
        {
            ICellView view = this.CreateCellView(e.Target,false);
            this.cellViews[e.Target.X, e.Target.Y] = view;
            view.Draw(this);
        }

        private void OnCellsInRegionRedefinedHandler(object sender, CellRegionEventArgs e)
        {
            int startX = e.RegionStartX;
            int startY = e.RegionStartY;
            int endX = e.RegionEndX;
            int endY = e.RegionEndY;
            Playfield playfield = this.engine.PlayField;
            bool shouldChangeColor = false;

            for (int indexX = startX; indexX < endX; indexX++)
            {
                if (endX % 2 == 0)
                {
                    shouldChangeColor = !shouldChangeColor;
                }

                for (int indexY = startY; indexY < endY; indexY++)
                {
                    ICellView view = this.CreateCellView(playfield[indexX, indexY], shouldChangeColor);
                    this.cellViews[indexX, indexY] = view;
                    view.Draw(this);
                    shouldChangeColor = !shouldChangeColor;
                }
            }
        }

        private void OnCellsInRegionChangedHandler(object sender, CellRegionEventArgs e)
        {
            int startX = e.RegionStartX;
            int startY = e.RegionStartY;
            int endX = e.RegionEndX;
            int endY = e.RegionEndY;

            for (int indexX = startX; indexX < endX; indexX++)
            { 
                for (int indexY = startY; indexY < endY; indexY++)
                {
                    this.cellViews[indexX, indexY].Draw(this);
                }
            }
        }

        private void OnCurrentCellChangedHandler(object sender, GameEngine.CellEventArgs e)
        {
           
        }
    }
}