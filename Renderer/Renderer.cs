﻿// <copyright file="Renderer.cs" company="Team Battlefield 3">
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
    /// Abstract class defining the behavior and interaction with an <see cref="GameEngine" />class.
    /// Renderer-derived classes respond to events fired by the Engine object they are
    /// explicitly attached by changing the state of <see cref="ICellView" /> objects they control.
    /// This class only binds event handlers and some common behavior, concrete classes
    /// should provide implementation or redefine it according to the rendering context 
    /// (Console, Windows Forms, WPF, OpenGL, DirectX and etc.).
    /// </summary>
    public abstract class Renderer
    {
        /// <summary>
        /// A reference to the <see cref="Engine"/> object this Renderer is attached to.
        /// </summary>
        private readonly Engine engine;

        /// <summary>
        /// A container for holding and accessing all controlled ICellView objects.
        /// </summary>
        private ICellView[,] cellViews;

        /// <summary>
        /// Initializes a new instance of the <see cref="Renderer" /> class.
        /// </summary>
        /// <param name="engine">The engine object to be attached to this object instance.</param>
        protected Renderer(Engine engine)
        {
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

            // Register event handlers.
            this.Engine.CurrentCellChanged += this.OnCurrentCellChangedHandler;
            this.Engine.CellsInRegionChanged += this.OnCellsInRegionChangedHandler;
            this.Engine.PlayfieldChanged += this.OnPlayfieldChangedHandler;
            this.Engine.CellsInRegionRedefined += this.OnCellsInRegionRedefinedHandler;
            this.Engine.CellRedefined += this.OnCellRedefinedHandler;
            this.Engine.CellChanged += this.OnCellChangedHandler;
        }

        /// <summary>
        /// Gets the engine reference to the <see cref="Engine"/> object this Renderer is attached to.
        /// </summary>
        /// <value>The engine.</value>
        protected Engine Engine
        {
            get
            {
                return this.engine;
            }
        }

        /// <summary>
        /// Gets or sets all controlled ICellView objects.
        /// </summary>
        /// <value>The cell views.</value>
        protected ICellView[,] CellViews
        {
            get
            {
                return this.cellViews;
            }
            
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Null CellViews provided");
                }

                this.cellViews = value;   
            }
        }

        /// <summary>
        /// Initiates a draw procedure on controlled <see cref="ICellView"/> objects.
        /// </summary>
        public virtual void DrawAll()
        {
            foreach (ICellView view in this.CellViews)
            {
                view.Draw();
            }

            this.DrawPointer();
        }

        /// <summary>
        /// Initiates a procedure defined by concrete classes for drawing a game over screen.
        /// </summary>
        /// <param name="totalMoves">The total moves a player has made up to this moment.</param>
        public abstract void DrawGameOver(int totalMoves);

        /// <summary>
        /// A method for creating new <see cref="ICellView"/> derived classes. Concrete implementations should
        /// implement this method and return the proper type of object for their rendering environment.
        /// This method is used by <see cref="Renderer.CreateCellViews"/> to get the right type of object needed.
        /// </summary>
        /// <param name="cell">The <see cref="ICell"/> object that will get displayed by returned object.</param>
        /// <param name="shouldChangeColor">Color of the should change.</param>
        /// <returns>CellView object to be added to Renderer's list of controlled objects.</returns>
        protected abstract ICellView CreateCellView(ICell cell, bool shouldChangeColor);

        /// <summary>
        /// Concrete implementations should
        /// implement this method according to their rendering environment.
        /// </summary>
        protected abstract void DrawPointer();
        
        /// <summary>
        /// Called when a new <see cref="Playfield"/> object is inserted to the attached GameEngine object.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PlayfieldChangedEventArgs" /> instance containing the event data.</param>
        protected virtual void OnPlayfieldChangedHandler(object sender, PlayfieldChangedEventArgs e)
        {
            this.CellViews = this.CreateCellViews(e.NewPlayField);
            this.DrawAll();
        }

        /// <summary>
        /// Called when a ICell object controlled by the attached GameEngine object changes its state. The default behavior is to redraw the 
        /// corresponding ICellView object maintained by this class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BattleFiled.GameEngine.CellEventArgs" />
        /// instance containing the event data about the changed CellView.</param>
        protected virtual void OnCellChangedHandler(object sender, CellEventArgs e)
        {
            this.CellViews[e.Target.X, e.Target.Y].Draw();
        }
  
        /// <summary>Called when there is a new ICell object being controlled by the attached GameEngine object. The default behavior is to create a new 
        /// corresponding CellView  and redraw it. 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BattleFiled.GameEngine.CellEventArgs" /> instance containing the event data.</param>
        protected virtual void OnCellRedefinedHandler(object sender, CellEventArgs e)
        {
            ICellView view = this.CreateCellView(e.Target, false);
            this.CellViews[e.Target.X, e.Target.Y] = view;
            view.Draw();
        }

        /// <summary>
        /// Called when a there is a group new ICell objects being controlled by the attached GameEngine object. The default behavior is to create new 
        /// corresponding CellView objects and redraw them.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BattleFiled.GameEngine.CellRegionEventArgs" /> instance containing the event data.</param>
        protected virtual void OnCellsInRegionRedefinedHandler(object sender, CellRegionEventArgs e)
        {
            int startX = e.RegionStartX;
            int startY = e.RegionStartY;
            int endX = e.RegionEndX;
            int endY = e.RegionEndY;
            Playfield playfield = this.Engine.PlayField;
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
                    this.CellViews[indexX, indexY] = view;
                    view.Draw();
                    shouldChangeColor = !shouldChangeColor;
                }
            }
        }

        /// <summary>
        /// Called when a group of ICell objects controlled by the attached GameEngine object change their state. The default behavior is to redraw the 
        /// corresponding ICellView objects maintained by this class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BattleFiled.GameEngine.CellRegionEventArgs" /> instance containing the event data.</param>
        protected virtual void OnCellsInRegionChangedHandler(object sender, CellRegionEventArgs e)
        {
            int startX = e.RegionStartX;
            int startY = e.RegionStartY;
            int endX = e.RegionEndX;
            int endY = e.RegionEndY;

            for (int indexX = startX; indexX < endX; indexX++)
            { 
                for (int indexY = startY; indexY < endY; indexY++)
                {
                    this.CellViews[indexX, indexY].Draw();
                }
            }
        }

        /// <summary>
        /// Called when the Cell object on focus gets changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BattleFiled.GameEngine.CellEventArgs" /> instance containing the event data.</param>
        protected virtual void OnCurrentCellChangedHandler(object sender, GameEngine.CellEventArgs e)
        {
        }

        /// <summary>
        /// Creates the ICellView object instances needed in order to visualize a provided <see cref="Playfield"/> object.
        /// </summary>
        /// <param name="playfield">The playfield to be used for generation.</param>
        /// <returns>An array of ICellView objects.</returns>
        private ICellView[,] CreateCellViews(Playfield playfield)
        { 
            int fieldSize = playfield.PlayfieldSize;
            bool shouldChangeColor = false;
            ICellView[,] cellViews = new ICellView[fieldSize, fieldSize];

            for (int i = 0; i < playfield.PlayfieldSize; i++)
            {
                if (playfield.PlayfieldSize % 2 == 0)
                {
                    shouldChangeColor = !shouldChangeColor;
                }

                for (int j = 0; j < playfield.PlayfieldSize; j++)
                {
                    cellViews[i, j] = this.CreateCellView(playfield[i, j], shouldChangeColor);
                    shouldChangeColor = !shouldChangeColor;
                }
            }

            return cellViews;
        }
    }
}