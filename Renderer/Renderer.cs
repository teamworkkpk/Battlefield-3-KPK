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

        public Renderer(Engine engine)
        {
            if (engine == null)
            {
                throw new ArgumentNullException("Null Engine provided.");
            }

            if (engine.PlayField != null)
            {
                this.cellViews = this.CreateCellViews(engine.PlayField);
                
                this.DrawAll();

            }

            //Register event handlers.
            engine.CurrentCellChanged += this.OnCurrentCellChangedHandler;
            engine.CellsInRegionChanged += this.OnCellsInRegionChangedHandler;
            engine.PlayfieldChanged += this.OnPlayfieldChangedHandler;
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

        protected abstract ICellView CreateCellView(ICell cell);

        private void DrawAll()
        {
            Console.Clear();
            foreach (ICellView view in this.cellViews)
            {
                view.Draw(this);
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
            //TODO: add functionality to draw cursor
        }
    }
}