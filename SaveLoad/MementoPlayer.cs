// <copyright file="ConsoleView.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.SaveLoad
{
    using System;

    /// <summary>
    /// Instance of this class saves the state of Player.cs instance.
    /// The class is used together with SaveLoadInterface.cs.
    /// </summary>    
    public class MementoPlayer
    {
        private string name;
        private int detonatedMines;
        private int movesCount;


        /// <summary>
        /// Gets and sets Name property of a MementoPlayer instance
        /// </summary>    
        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Error: Memento player name cannot be null or emtpy");
                }

                this.name = value;
            }
        }

        /// <summary>
        /// Gets and sets DetonatedMines property of a MementoPlayer instance
        /// </summary>    
        public int DetonatedMines
        {
            get
            {
                return this.detonatedMines;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Error: Memento detonated mines must be positive");
                }
                this.detonatedMines = value;
            }
        }

        /// <summary>
        /// Gets and sets MovesCount property of a MementoPlayer instance
        /// </summary>    
        public int MovesCount
        {
            get
            {
                return this.movesCount;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Error: Memento movesCount mines must be positive");
                }
                this.movesCount = value;
            }
        }
    }
}
