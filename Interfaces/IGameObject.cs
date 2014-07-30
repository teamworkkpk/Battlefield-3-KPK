// <copyright file="ConsoleView.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.Interfaces
{
    using System;
    using System.Linq;

    /// <summary>
    /// Contract for initializing empty field and placing mines
    /// </summary>
    public interface IGameObject
    {
        /// <summary>
        /// Sets a way to initialize empty field
        /// </summary>
        void InitializeEmptyField();

        /// <summary>
        /// Sets a way to place mines
        /// </summary>
        void PlaceMines();
    }
}
