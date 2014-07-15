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
        }

        private void OnPlayfieldChangedHandler(object sender, PlayfieldChangedEventArgs e)
        {
            this.cellViews = this.CreateCellViews(e.NewPlayField);
            this.DrawAll();
        }

        private ICellView[,] CreateCellViews(Playfield playfield)
        { 
            int fieldSize = playfield.PlayfieldSize;
            ICellView[,] cellViews = new ICellView[fieldSize, fieldSize];

            //foreach (ICell cell in playfield)
            //{
            //    Console.WriteLine("                   " + cell.X + " " + cell.Y);
            //    cellViews[cell.X-1, cell.Y] = CreateCellView(cell);
            //}

            for (int i = 0; i < playfield.PlayfieldSize; i++)
            {
                for (int j = 0; j < playfield.PlayfieldSize; j++)
                {
                    cellViews[i, j] = CreateCellView(playfield[i,j]);
                }
                
            }
            return cellViews;
        }

        public void ChangeCellView(Playfield playfieldToCreateCellView)
        {
            int fieldSize = playfieldToCreateCellView.PlayfieldSize;
           // cellViews = new ICellView[fieldSize, fieldSize];

            for (int i = 0; i < playfieldToCreateCellView.PlayfieldSize; i++)
            {
                for (int j = 0; j < playfieldToCreateCellView.PlayfieldSize; j++)
                {
                    cellViews[i, j] = CreateCellView(playfieldToCreateCellView[i, j]);
                }

            }
        }

        protected abstract ICellView CreateCellView(ICell cell);

        public void DrawAll()
        {
            Console.Clear();
            
            foreach (ICellView view in this.cellViews)
            {
                view.Draw(this);
            }

            DrawPointer();
        }

        private void DrawPointer()
        {
            Console.SetCursorPosition(this.engine.Pointer.X + ConsolePadding, this.engine.Pointer.Y + ConsolePadding);
            Console.BackgroundColor = ConsoleColor.Green;
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
            //TODO: add functionality to draw cursor
        }
    }
}