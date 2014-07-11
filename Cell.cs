namespace BattleFiled
{
    using System;
    using System.Collections.Generic;
    using Interfaces;

    public class Cell : ICell
    {

        public Cell(CellTypes celltypes)
        {
            switch (celltypes)
            {
                case CellTypes.EmptyCell:
                    CreateEmptyCell();
                    break;
                case CellTypes.Bomb:
                    CreateBombCell();
                    //Console.WriteLine(this.Color);
                    break;
                case CellTypes.BlownCell:
                    CreateBlownCell();
                    break;
                default:
                    break;
            }
        }

        public ICell Clone()
        {
            ICell cellCopy = new Cell(this.CellType);
            
            cellCopy.CellView = this.CellView;
            cellCopy.Color = this.Color;

            return cellCopy;
        }

        private void CreateEmptyCell()
        {
            this.CellView = BattleFiled.CellView.Empty;
            this.Color = BattleFiled.Color.White;
        }

        private void CreateBombCell()
        {
            int number = RandomGenerator.GetRandomNumber(1, 6);
            switch (number)
            {
                case 1:
                    this.CellView = BattleFiled.CellView.Bomb1;
                    break;
                case 2:
                    this.CellView = BattleFiled.CellView.Bomb2;
                    break;
                case 3:
                    this.CellView = BattleFiled.CellView.Bomb3;
                    break;
                case 4:
                    this.CellView = BattleFiled.CellView.Bomb4;
                    break;
                case 5:
                    this.CellView = BattleFiled.CellView.Bomb5;
                    break;
                default:
                    break;
            }
            this.Color = BattleFiled.Color.Magenda;
        }

        private void CreateBlownCell()
        {
            this.CellView = BattleFiled.CellView.Blown;
            this.Color = BattleFiled.Color.Red;
        }
        public CellTypes CellType
        {
            get;
            set;
        }

        public CellView CellView
        {
            get;
            set;
        }
        public Color Color
        {
            get;
            set;
        }

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
