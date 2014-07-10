﻿namespace BattleFiled
{
    using System;
    using System.Text;
    using Interfaces;
    using System.Collections;
    
    public sealed class Playfield : IGameObject, IEnumerable
    {
        private static Playfield PlayfieldInstance;

        private ICell[,] playfield;
        
        private Playfield()
        {
        }

        public static Playfield Instance
        {
            get
            {
                if (Playfield.PlayfieldInstance == null)
                {
                    Playfield.PlayfieldInstance = new Playfield();
                }
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
                    this.playfield[i, j] = new Cell(CellTypes.EmptyCell);
                   // Console.WriteLine(this.playfield[i, j]);
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < this.PlayfieldSize; i++)
            {
                for (int j = 0; j < this.PlayfieldSize; j++)
                {
                    builder.Append(this.playfield[i,j]);
                }

                builder.AppendLine();
                builder.AppendLine();
            }

            return builder.ToString();
        }

        public void PlaceMines()
        {
            //TODO find why all mines are displayed with white color, but not magenda and fix it

            int totalCellsCount = this.PlayfieldSize * this.PlayfieldSize;

            int fifteenPercentCellsCount = (int)Math.Floor(totalCellsCount * 0.15);
            int thirtyPercentCellsCount = (int)Math.Floor(totalCellsCount * 0.30);

            int minesCount = RandomGenerator.GetRandomNumber(fifteenPercentCellsCount, thirtyPercentCellsCount + 1);            

            for (int i = 0; i < minesCount; i++)
            {
                int mineRowPosition = RandomGenerator.GetRandomNumber(0, PlayfieldSize);
                int mineColPosition = RandomGenerator.GetRandomNumber(0, PlayfieldSize);

                Cell bombCell = new Cell(CellTypes.Bomb);
                //bombCell.CellType = CellTypes.Bomb;

               this.playfield[mineRowPosition, mineColPosition] = bombCell;
                
            }           
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < this.playfield.GetLength(0); i++)
            {
                for (int j = 0; j < this.playfield.GetLength(1); j++)
                {
                    yield return this.playfield[i, j];
                }
            }            
        }
    }
}
