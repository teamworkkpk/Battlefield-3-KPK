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
