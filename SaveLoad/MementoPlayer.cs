// <copyright file="MementoPlayer.cs" company="Team Battlefield 3">
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
        /// <summary>
        /// Keeps name of the player.
        /// </summary>
        private string name;

        /// <summary>
        /// Keeps detonated mines.
        /// </summary>
        private int detonatedMines;

        /// <summary>
        /// Keeps moves count.
        /// </summary>
        private int movesCount;

        /// <summary>
        /// Gets or sets Name property of a MementoPlayer instance.
        /// </summary>    
        /// <value>Keeps name.</value>
        public string Name
        {
            get
            { 
                return this.name; 
            }

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
        /// Gets or sets DetonatedMines property of a MementoPlayer instance.
        /// </summary>    
        /// <value>Keep detonated mines count.</value>
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
        /// Gets or sets MovesCount property of a MementoPlayer instance.
        /// </summary>    
        /// <value>Keep moves count.</value>
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
