// <copyright file="PlayfieldChangedEventArgs.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled.GameEngine
{
    using System;
    using BattleFiled;
    
    /// <summary>
    /// PlayfieldChangedEventArgs will handle the event for changes in the playfield.
    /// </summary>
    public class PlayfieldChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The playfield in the game,.
        /// </summary>
        private Playfield newPlayField;

        /// <summary>
        /// The playfield size.
        /// </summary>
        private int playfieldSize;
        
        /// <summary>
        /// Initializes a new instance of the PlayfieldChangedEventArgs.
        /// </summary>
        /// <param name="newPlayfield">Playfield newPlayfield.</param>
        public PlayfieldChangedEventArgs(Playfield newPlayfield)
        {
            this.NewPlayField = newPlayfield;
            this.PlayfieldSize = newPlayfield.PlayfieldSize;
        }

        /// <summary>
        /// Gets the playfield.
        /// </summary>
        /// <value>Playfield newPlayField</value>
        public Playfield NewPlayField
        {
            get
            {
                return this.newPlayField;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Null Playfield provided.");
                }

                this.newPlayField = value; 
            }
        }

        /// <summary>
        /// Gets the size of the playfield.
        /// </summary>
        /// <value>Integer playfieldSize.</value>
        public int PlayfieldSize
        {
            get
            {
                return playfieldSize;
            }
            private set
            {
                playfieldSize = value;
            }
        }
    }
}