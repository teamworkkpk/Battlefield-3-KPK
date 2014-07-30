// <copyright file="ConsoleView.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled
{
    using System;

    /// <summary>
    /// Provides functionality for registering events when pressing keys
    /// </summary>
    public class KeyEventArgs : EventArgs
    {
        private readonly ConsoleKey pressedKey;

        public KeyEventArgs(ConsoleKey pressedKey)
        {
            this.pressedKey = pressedKey;
        }

        /// <summary>
        /// Gets ConsoleKey PressedKey
        /// </summary>
        public ConsoleKey PressedKey
        {
            get
            {
                return this.pressedKey;
            }
        }
    }
}