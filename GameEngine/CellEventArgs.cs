// <copyright file="CellEventArgs.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.GameEngine
{
    using System;
    using BattleFiled.Cells;

    /// <summary>
    /// CellEventArgs will handle the event, when a cell needs to change it's type.
    /// </summary>
    public class CellEventArgs : EventArgs
    {
        /// <summary>
        /// The cell target.
        /// </summary>
        private ICell target;

        /// <summary>
        /// Initializes a new instance of the CellEventArgs class with a target of type ICell.
        /// </summary>
        /// <param name="target">ICell object, which will be the target of the CellEventArgs.</param>
        public CellEventArgs(ICell target)
        {
            this.Target = target;
        }

        /// <summary>
        /// Gets the CellEventArgs target.
        /// </summary>
        /// /// <value>ICell target.</value>
        public ICell Target
        {
            get 
            {
                return this.target;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Cannot set null Target.");
                }

                this.target = value;
            }
        }
    }
}