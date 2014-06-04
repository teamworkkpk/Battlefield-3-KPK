﻿namespace BattleFiled
{
    using System;
    using System.Collections.Generic;

    public class Cell : ICell
    {

        public Cell(CellTypes celltypes)
        {
            switch (celltypes)
            {
                case CellTypes.EmptyCell:
                    this.CellView = BattleFiled.CellView.Empty;
                    this.Color = BattleFiled.Color.White;
                    break;
                case CellTypes.Bomb:
                    Random random = new Random();
                    int number = random.Next(1, 6);
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
                    break;
                case CellTypes.BlownCell:

                    this.CellView = BattleFiled.CellView.Blown;
                    this.Color = BattleFiled.Color.Red;

                    break;
                default:
                    break;
            }
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

            return ((int)this.CellView).ToString();
        }



    }
}
