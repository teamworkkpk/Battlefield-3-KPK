// <copyright file="ConsoleView.cs" company="Team Battlefield 3">
// All rights reserved.
// </copyright>
// <author>Team Battlefield 3</author>

namespace BattleFiled
{
    using System;
    using System.Text;
    using System.Collections;
    using SaveLoad;
    using BattleFiled.Cells;
    using Interfaces;

    /// <summary>
    /// Playfield.Cs implements Singleton design pattern, because the game needs only one 
    /// instance for the playfield.
    /// Playfield.Cs implements Iterator pattern. Using foreach over Playfield.Instance is avaliable
    /// </summary>    
    public sealed class Playfield : IGameObject, IEnumerable
    {
        /// <summary>
        /// Constant for min field size.
        /// </summary>
        private const int MIN_FIELD_SIZE = 2;

        /// <summary>
        /// Constant for max field size.
        /// </summary>
        private const int MAX_FIELD_SIZE = 10;

        /// <summary>
        /// Singleton instance for playfield.
        /// </summary>
        private static Playfield PlayfieldInstance;

        /// <summary>
        /// Two dimensional array, contains all cells.
        /// </summary>
        private ICell[,] cells;

        private Playfield()
        {
        }

        /// <summary>
        /// Gets Singleton instance of the class
        /// </summary>
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

        /// <summary>
        /// Gets Playfield.Playfield size
        /// </summary>
        public int PlayfieldSize
        {
            get
            {
                return this.cells.GetLength(0);
            }
        }

        /// <summary>
        /// Item property used to get and sets the type of a particular cell in the playfield
        /// </summary>
        public ICell this[int posX, int posY]
        {
            get
            {
                return this.cells[posX, posY];
            }
            set
            {
                this.cells[posX, posY] = value;
            }
        }

        /// <summary>
        /// Sets Playfield size
        /// </summary>
        /// <param name="size"></param>
        public void SetFieldSize(int size)
        {
            if (size < MIN_FIELD_SIZE || size > MAX_FIELD_SIZE)
            {
                throw new ArgumentException(string.Format("Error: field size must be between {0} and {1}", MIN_FIELD_SIZE, MAX_FIELD_SIZE));
            }
            this.cells = new Cell[size, size];
        }

        /// <summary>
        /// Initializes new empty field
        /// </summary>
        public void InitializeEmptyField()
        {
            if (cells == null)
            {
                throw new ArgumentNullException("Error: playfiled array cannot be null (not initialized)");
            }

            for (int i = 0; i < this.cells.GetLength(0); i++)
            {
                for (int j = 0; j < this.cells.GetLength(1); j++)
                {
                    ICell cell = CellFactory.CreateCell(CellType.EmptyCell);
                    cell.X = i;
                    cell.Y = j;
                    this.cells[i, j] = cell;
                }
            }
        }

        /// <summary>
        /// String representation of the Playfield object
        /// </summary>
        public override string ToString()
        {
            if (cells == null)
            {
                throw new ArgumentNullException("Error: playfiled array cannot be null (not initialized)");
            }

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < this.PlayfieldSize; i++)
            {
                for (int j = 0; j < this.PlayfieldSize; j++)
                {
                    builder.Append(this.cells[i, j]);
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        /// <summary>
        /// Places mines on the field
        /// </summary>
        public void PlaceMines()
        {
            if (cells == null)
            {
                throw new ArgumentNullException("Error: playfiled array cannot be null (not initialized)");
            }

            int totalCellsCount = this.PlayfieldSize * this.PlayfieldSize;

            //CellView cellView;
            int fifteenPercentCellsCount = (int)Math.Floor(totalCellsCount * 0.15);
            int thirtyPercentCellsCount = (int)Math.Floor(totalCellsCount * 0.30);

            int minesCount = RandomGenerator.GetRandomNumber(fifteenPercentCellsCount, thirtyPercentCellsCount + 1);

            for (int i = 0; i < minesCount; i++)
            {
                int mineRowPosition = RandomGenerator.GetRandomNumber(0, PlayfieldSize);
                int mineColPosition = RandomGenerator.GetRandomNumber(0, PlayfieldSize);

                ICell bombCell = CellFactory.CreateCell(CellType.Bomb);
                bombCell.X = mineRowPosition;
                bombCell.Y = mineColPosition;
                this.cells[mineRowPosition, mineColPosition] = bombCell;
                //Console.WriteLine(this.playfield[mineRowPosition, mineColPosition].CellView);
            }
        }

        /// <summary>
        /// Iterates over the cells of the field
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < this.PlayfieldSize; i++)
            {
                for (int j = 0; j < this.PlayfieldSize; j++)
                {
                    yield return this[i, j];
                }
            }            
        }

        /// <summary>
        /// Returns MementoField instance that keeps the current state of the Playfield.Instance
        /// </summary>
        /// <returns></returns>
        public MementoField SaveMemento()
        {
            if (cells == null)
            {
                throw new ArgumentNullException("Error: playfiled array cannot be null (not initialized)");
            }

            MementoField memento = new MementoField();

            memento.ZeroBasedPlayField = CloneToZeroBasedArray(this.cells as Cell[,]);

            memento.FieldDimension = this.PlayfieldSize;

            return memento;
        }

        /// <summary>
        /// Restore previously state of PlayField.Instance 
        /// </summary>
        /// <param name="memento"></param>
        public void LoadMemento(MementoField memento)
        {
            if (memento == null)
            {
                throw new ArgumentNullException("Error: loaded memento object cannot be null!");
            }

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