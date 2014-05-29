namespace BattleFiled
{
    using System;

    public sealed class Playfield
    {
        private static readonly Playfield PlayfieldInstance = new Playfield();

        private ICell[,] playfield;

        private Playfield()
        {
        }

        public static Playfield Instance
        {
            get
            {
                return PlayfieldInstance;
            }
        }

        public int PlayfieldSize
        {
            get
            {
                return this.playfield.GetLength(0);
            }
        }

        public void SetFieldSize(int size)
        {
            this.playfield = new Cell[size, size];
        }

        public void InitializeEmptyField()
        {
            for (int i = 0; i < this.playfield.GetLength(0); i++)
            {
                for (int j = 0; j < this.playfield.GetLength(1); j++)
                {
                    this.playfield[i, j] = new Cell();
                }
            }
        }

        public void PlaceMines()
        {
            Random randomGenerator = new Random();

            int totalCellsCount = this.PlayfieldSize * this.PlayfieldSize;

            int fifteenPercentCellsCount = (int)Math.Floor(totalCellsCount * 0.15);
            int thirtyPercentCellsCount = (int)Math.Floor(totalCellsCount * 0.30);

            int minesCount = randomGenerator.Next(fifteenPercentCellsCount, thirtyPercentCellsCount + 1);            

            for (int i = 0; i < minesCount; i++)
            {
                int mineRowPosition = randomGenerator.Next(0, PlayfieldSize);
                int mineColPosition = randomGenerator.Next(0, PlayfieldSize);

                Cell bombCell = new Cell();
                bombCell.CellType = CellTypes.Bomb;

                this.playfield[mineRowPosition, mineColPosition] = bombCell;
            }           
        }
    }
}
