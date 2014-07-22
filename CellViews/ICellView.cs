// <copyright file="ICellView.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.CellViews
{
    using System;
    using BattleFiled.Renderer.Context;    

    /// <summary>
    /// Interface that sets the methods for cell rendering.
    /// </summary>
    public interface ICellView
    {
        /// <summary>
        /// Sets the way of cell rendering.
        /// </summary>                
        void Draw();
    }
}