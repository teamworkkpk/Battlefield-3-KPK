namespace BattleFiled
{
    public class Player
    {
        private int detonatedMines;
        private int movesCount;

        public Player(string name)
        {
            this.Name = name;
            this.detonatedMines = 0;
            this.movesCount = 0;
        }

        public string Name { get; protected set; }

        public int DetonatedMines
        {
            get
            {
                return this.detonatedMines;
            }
        }

        public int MovesCount
        {
            get
            {
                return this.movesCount;
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
    }
}
