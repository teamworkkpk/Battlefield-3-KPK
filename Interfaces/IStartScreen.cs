// <copyright file="ConsoleView.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

using System;
using System.Linq;

namespace BattleFiled.Interfaces
{
    /// <summary>
    /// Contract handling user choice and checks if a new game is chosen
    /// </summary>
    public interface IStartScreen
    {
        /// <summary>
        /// Sets choice
        /// </summary>
        void SetChoise();

        /// <summary>
        /// Holds information if a new game is chosen
        /// </summary>
        bool IsStartGameChosen { get; set; }
    }
}
