namespace BattleFiled
{
    using System;
    using SaveLoad;
    public class Player
    {
        private string name;
        private int detonatedMines;
        private int movesCount;

        public Player(string name)
        {
            this.Name = name;
        }

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
                else if(string.IsNullOrWhiteSpace(value.Trim()))
                {
                    throw new ArgumentNullException ("Error: Memento player name cannot be null or whitespace!");
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
                    throw new ArgumentOutOfRangeException("Error: Memento detonated mines count cannot less than zero!");
                }
                if (value > 100)
                {
                    throw new ArgumentOutOfRangeException("Error: Memento detonated mines count cannot greater than 100!");
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
                    throw new ArgumentOutOfRangeException("Error: Memento moves count cannot less than zero!");
                }
                this.movesCount = value;
            }
        }

        public void AddDetonatedMines(int mines)
        {
            this.detonatedMines += mines;
        }

        public void AddMove()
        {
            this.movesCount++;
        }

        public override string ToString()
        {
            return string.Format("Player: {0}, Detonated mines: {1}, Moves: {2}", this.Name, this.DetonatedMines, this.MovesCount);
        }

        public MementoPlayer SaveMemento()
        {
            return new MementoPlayer()
            {
                Name = this.Name,
                DetonatedMines = this.DetonatedMines,
                MovesCount = this.MovesCount
            };
        }

        public void RestoreMemento(MementoPlayer memento)
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
