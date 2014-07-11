namespace BattleFiled.Cells
{
    using System;
    
    class BombCell:Cell
    {
        public static int MinimalBombSize = 1;
        public static int MaximumBombSize = 5;
        
        private readonly int bombSize;

        public int BombSize
        {
            get { return bombSize; }
        }

        
        public BombCell(int bombSize) : base(CellType.Bomb)
        {
            if (bombSize < MinimalBombSize || bombSize > MaximumBombSize)
            {
                throw new ArgumentOutOfRangeException("Bomb sie must be between 1 and 5.");
            }

            this.bombSize = bombSize;
        }

        public override ICell Clone()
        {
            throw new NotImplementedException();
        }
    }
}
