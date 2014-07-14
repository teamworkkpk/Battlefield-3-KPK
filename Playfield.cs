﻿namespace BattleFiled
{
    using System;
    using System.Text;
    using BattleFiled.Cells;
    using Interfaces;
    using System.Collections;
    using SaveLoad;
    using System.Windows.Forms;
    using System.Drawing;

    /// <summary>
    /// Playfield.Cs implements Singleton design pattern, because the game needs only one 
    /// instance for the playfield.
    /// Playfield.Cs implements Iterator pattern. Using foreach over Playfield.Instance is avaliable
    /// </summary>    
    public sealed class Playfield : IGameObject, IEnumerable
    {
        private static Playfield PlayfieldInstance;

        private ICell[,] cells;

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
                return this.cells.GetLength(0);
            }
        }


        public ICell this[int posX, int posY]
        {
            get
            {
                return this.cells[posX, posY];
            }
            set
            {
                throw new InvalidOperationException("Playfield indexer is read-only.");
            }
        }

        public void SetFieldSize(int size)
        {
            this.cells = new Cell[size, size];
            
        }

        public void InitializeEmptyField()
        {
            
            for (int i = 0; i < this.cells.GetLength(0); i++)
            {
                for (int j = 0; j < this.cells.GetLength(1); j++)
                {
                    ICell cell  = CellFactory.CreateCell(CellType.EmptyCell);
                    cell.X = i*2;
                    cell.Y = j*2;
                    this.cells[i, j] = cell;
                    //Console.WriteLine("                   " +i+" "+j);
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
                    builder.Append(this.cells[i, j]);
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
            
            //CellView cellView;
            int fifteenPercentCellsCount = (int)Math.Floor(totalCellsCount * 0.15);
            int thirtyPercentCellsCount = (int)Math.Floor(totalCellsCount * 0.30);

            int minesCount = RandomGenerator.GetRandomNumber(fifteenPercentCellsCount, thirtyPercentCellsCount + 1);            

            for (int i = 0; i < minesCount; i++)
            {
                int mineRowPosition = RandomGenerator.GetRandomNumber(0, PlayfieldSize)*2;
                int mineColPosition = RandomGenerator.GetRandomNumber(0, PlayfieldSize)*2;

                ICell bombCell = CellFactory.CreateCell(CellType.Bomb);
                bombCell.X = mineRowPosition;
                bombCell.Y = mineColPosition;
                this.cells[mineRowPosition/2, mineColPosition/2] = bombCell;
                //Console.WriteLine(this.playfield[mineRowPosition, mineColPosition].CellView);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.cells.GetEnumerator();
        }

        /// <summary>
        /// Returns MementoField instance that keeps the current state of the Playfield.Instance
        /// </summary>
        /// <returns></returns>
        public MementoField Save()
        { 
            MementoField memento = new MementoField();

            memento.ZeroBasedPlayField = CloneToZeroBasedArray(this.cells as Cell[,]);
            memento.FieldDimension = this.PlayfieldSize;

            return memento;
        }

        /// <summary>
        /// Restore previously state of PlayField.Instance 
        /// </summary>
        /// <param name="memento"></param>
        public void Load(MementoField memento)
        {
            this.cells = this.CloneToMultiDimArray(memento.ZeroBasedPlayField, memento.FieldDimension);
        }

        /// <summary>
        /// Two-dimensional playfield array is cloned as zero-based array for the needs of XML serialization.
        /// XML Serializator cannot serialize multi-dimensional arrays. 
        /// XML Serializator cannot work with interfaces, that's why is used Cell.cs, but not ICell interface
        /// </summary>
        /// <param name="fieldToCopy"></param>
        /// <returns></returns>
        private Cell[] CloneToZeroBasedArray(Cell[,] fieldToCopy)
        {
            int backupArrayLength = fieldToCopy.GetLength(0) * fieldToCopy.GetLength(0);

            Cell[] copy = new Cell[backupArrayLength];           

            int index = 0;
            foreach (Cell item in this)
            {
                copy[index] = item.Clone() as Cell;
                index++;
            }

            return copy;
        }

        /// <summary>
        /// Restores the multidimensional array from the zero-based one comming from the serialization.
        /// </summary>
        /// <param name="zeroBasedArray"></param>
        /// <param name="fieldDimensions"></param>
        /// <returns></returns>

        private Cell[,] CloneToMultiDimArray(Cell[] zeroBasedArray, int fieldDimensions)
        {
            Cell[,] copy = new Cell[fieldDimensions, fieldDimensions];
            int index = 0;

            for (int i = 0; i < fieldDimensions; i++)
            {
                for (int j = 0; j < fieldDimensions; j++)
                {
                    copy[i, j] = zeroBasedArray[index].Clone() as Cell;
                    index++;
                }
            }

            return copy;
        }
    }
}