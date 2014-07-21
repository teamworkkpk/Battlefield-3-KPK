namespace BattleFiled.GameEngine
{
    using System;
    using BattleFiled;
    
    public class PlayfieldChangedEventArgs : EventArgs
    {
        private Playfield newPlayField;
        private int playfieldSize;
        
        public PlayfieldChangedEventArgs(Playfield newPlayfield)
        {
            this.NewPlayField = newPlayfield;
            this.PlayfieldSize = newPlayfield.PlayfieldSize;
        }

        public Playfield NewPlayField
        {
            get
            {
                return newPlayField;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Null Playfield provided.");
                }
                newPlayField = value; 
            }
        }

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