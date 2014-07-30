// <copyright file="ConsoleView.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled
{
    using System;
    using SaveLoad;

    /// <summary>
    /// Class Player hold statistics about the player.
    /// </summary>
    public class Player
    {
        private string name;
        private int detonatedMines;
        private int movesCount;

        public Player(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets and sets Player.Name property
        /// </summary>
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
                    throw new ArgumentNullException("Error: Memento player name cannot be null or empty!");
                }
                else if (string.IsNullOrWhiteSpace(value.Trim()))
                {
                    throw new ArgumentNullException("Error: Memento player name cannot be null or whitespace!");
                }

                this.name = value;
            }
        }

        /// <summary>
        /// Gets and sets Player.DetonatedMines property
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
                    throw new ArgumentOutOfRangeException("Error: Memento detonated mines count cannot less than zero!");
                }

                if (value > 100)
                {
                    throw new ArgumentOutOfRangeException("Error: Memento detonated mines count cannot greater than 100!");
                }

                this.detonatedMines = value;
            }
        }

        /// <summary>
        /// Gets and sets Player.MovesCount property
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
                    throw new ArgumentOutOfRangeException("Error: Memento moves count cannot less than zero!");
                }

                this.movesCount = value;
            }
        }

        /// <summary>
        /// Adds detonated mines
        /// </summary>
        /// <param name="mines"></param>
        public void AddDetonatedMines(int mines)
        {
            this.detonatedMines += mines;
        }

        /// <summary>
        /// Increase the move counter
        /// </summary>
        public void AddMove()
        {
            this.movesCount++;
        }

        /// <summary>
        /// Creates a string of the Player object.
        /// </summary>
        public override string ToString()
        {
            return string.Format("Player: {0}, Detonated mines: {1}, Moves: {2}", this.Name, this.DetonatedMines, this.MovesCount);
        }

        /// <summary>
        /// Saves the players statistics
        /// </summary>
        /// <returns></returns>
        public MementoPlayer SaveMemento()
        {
            return new MementoPlayer()
            {
                Name = this.Name,
                DetonatedMines = this.DetonatedMines,
                MovesCount = this.MovesCount
            };
        }

        /// <summary>
        /// Loads player statistics
        /// </summary>
        /// <param name="memento"></param>
        public void LoadMemento(MementoPlayer memento)
        {
            if (memento == null)
            {
                throw new ArgumentNullException("Error: Loaded memento player cannot be null!");
            }

            this.Name = memento.Name;
            this.DetonatedMines = memento.DetonatedMines;
            this.MovesCount = memento.MovesCount;
        }
    }
}
