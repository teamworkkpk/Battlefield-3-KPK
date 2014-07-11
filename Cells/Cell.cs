namespace BattleFiled.Cells
{
    using System;
    using BattleFiled;

    public abstract class Cell : ICell
    {
        private int positionX;
        private int positionY;
        private readonly CellType cellType;

        protected Cell(CellType celltype)
        {
            this.cellType = celltype;
        }
        
        public CellType CellType
        {
           get { return this.cellType; }
        }

        public CellView CellView
        {
            get;
            set;
        }
        
        public int X
        {
            get
            {
                return this.positionX;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Cell's X position is always a positive number or 0.");
                }
                this.positionX = value;
            }
        }

        public int Y
        {
            get
            {
                return this.positionY;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Cell's Y position is always a positive number or 0.");
                }
                this.positionY = value;
            }
        }

        public Color Color { get;set; }

        public abstract ICell Clone();

        public override string ToString()
        {
            switch (this.Color)
            {
                case Color.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Color.White:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Color.Magenda:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                default:

                    break;
            }
            //edit to cast char instead of int, so the symbol is returned
            return " " + ((char)this.CellView).ToString() + " ";
        }
    }
}
